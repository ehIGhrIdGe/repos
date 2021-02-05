using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace EChat.Models
{
    public class ManagerDb
    {
        private enum QUERYTYPE
        {
            ChatLogs, Users, ChatLogsUsers, GetMessages, GetUserInfo, DeleteUserInfo
        }

        public static IConfiguration Configuration { get; set; }

        private static string ExcuteQuery { get; set; }

        private static SqlConnection SqlConnection { get; set; }

        private static SqlCommand SqlCommand { get; set; }

        private static SqlDataAdapter SqlDataAdapter { get; set; }

        private static DataTable MsgDataTable { get; set; }

        private static DataTable UserDataTable { get; set; }

        private static SqlTransaction SqlTransaction { get; set; }

        public static bool DeleteUserData(string inputUserId)
        {
            UserDataTable = GetData(QUERYTYPE.GetUserInfo, "@userid", inputUserId);
            UserDataTable.Rows[0].Delete();            

            return TryDeleteData(QUERYTYPE.Users, UserDataTable);
        }

        /// <summary>
        /// ユーザーを新規登録する
        /// </summary>
        /// <param name="inputUser">登録したい User型 のデータ</param>
        /// <returns></returns>
        public static bool InsertUser(User inputUser)
        {
            var tempDt = UserDataTable.Clone();
            var nr = tempDt.NewRow();
            nr[0] = inputUser.UserId;
            nr[1] = inputUser.UserName;
            nr[2] = inputUser.PasswordType;
            nr[3] = inputUser.PasswordSalt;
            nr[4] = inputUser.Password;
            nr[5] = inputUser.IsAdministrator;
            tempDt.Rows.Add(nr);

            return TryInsertData(QUERYTYPE.Users, nr);
        }

        /// <summary>
        /// 特定のユーザーデータを更新
        /// </summary>
        /// <param name="inputUserInfo"></param>
        /// <returns></returns>
        public static bool UpdateUser(User inputUserInfo)
        {
            UserDataTable = GetData(QUERYTYPE.GetUserInfo, "@userid", inputUserInfo.UserId);
            UserDataTable.Rows[0]["UserId"] = inputUserInfo.UserId;
            UserDataTable.Rows[0]["UserName"] = inputUserInfo.UserName;
            UserDataTable.Rows[0]["PasswordType"] = inputUserInfo.PasswordType;
            UserDataTable.Rows[0]["PasswordSalt"] = inputUserInfo.PasswordSalt;
            UserDataTable.Rows[0]["Password"] = inputUserInfo.Password;
            UserDataTable.Rows[0]["IsAdministrator"] = inputUserInfo.IsAdministrator;

            return TryUpdateData(QUERYTYPE.Users, UserDataTable);
        }

        /// <summary>
        /// 特定のユーザーデータを読み込む
        /// </summary>
        /// <param name="inputUserId"></param>
        /// <returns></returns>
        public static User GetUserData(string inputUserId)
        {
            UserDataTable = GetData(QUERYTYPE.GetUserInfo, "@userid", inputUserId);
            User user = null;

            if (UserDataTable.Rows.Count != 0)
            {
                user = new User(UserDataTable.Rows[0][0].ToString(), UserDataTable.Rows[0][1].ToString(), (byte)UserDataTable.Rows[0][2], UserDataTable.Rows[0][3].ToString(), UserDataTable.Rows[0][4].ToString(), (bool)UserDataTable.Rows[0][5]);
            }

            return user;
        }

        /// <summary>
        /// ユーザー一覧を List<User> で取得する
        /// </summary>
        /// <param name="inputUserId"></param>
        /// <returns></returns>
        public static List<User> GetUsers()
        {
            UserDataTable = GetData(QUERYTYPE.Users);
            List<User> userList = null;

            if (UserDataTable.Rows.Count != 0)
            {
                userList = new List<User>();

                foreach (DataRow user in UserDataTable.Rows)
                {
                    var temp = new User((string)user[0], (string)user[1], (byte)user[2], (string)user[3], (string)user[4], (bool)user[5]);
                    userList.Add(temp);
                }
            }

            return userList;
        }

        public static bool InsertMessage(string msg, string userId)
        {
            var tempDt = MsgDataTable.Clone();
            var nr = tempDt.NewRow();
            nr[1] = DateTime.Now;
            nr[2] = msg;
            nr[3] = Environment.UserName;
            nr[3] = userId;
            tempDt.Rows.Add(nr);

            return TryInsertData(QUERYTYPE.ChatLogs, nr);
        }

        /// <summary>
        /// メッセージを読み込む
        /// </summary>
        /// <returns></returns>
        public static List<Message> GetMessages()
        {
            MsgDataTable = GetData(QUERYTYPE.GetMessages, "@loadnum", Configuration.GetSection("LoadNumber").GetValue<int>("Message"));

            List<Message> messagesList = new List<Message>();

            foreach(DataRow message in MsgDataTable.Rows)
            {
                var temp = new Message((int)message[0], (DateTime)message[1], (string)message[2], (string)message[3]);
                messagesList.Add(temp);
            }

            return messagesList;
        }

        private static bool TryDeleteData(QUERYTYPE inputType, DataTable inputDt)
        {
            try
            {
                using (SqlConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                using (SqlCommand = new SqlCommand())
                using (SqlDataAdapter = new SqlDataAdapter())
                {
                    SqlConnection.Open();
                    SqlCommand.Connection = SqlConnection;
                    SqlCommand.CommandText = LoadQuery(inputType);
                    SqlTransaction = SqlConnection.BeginTransaction();
                    SqlCommand.Transaction = SqlTransaction;

                    try
                    {
                        using (inputDt)
                        {
                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
                            SqlDataAdapter.SelectCommand = SqlCommand;
                            sqlBld.DataAdapter = SqlDataAdapter;
                            SqlDataAdapter.DeleteCommand = sqlBld.GetDeleteCommand();

                            SqlDataAdapter.Update(inputDt);
                            SqlTransaction.Commit();
                            return true;
                        }
                    }
                    catch (SqlException)
                    {
                        SqlTransaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        SqlConnection.Close();
                    }
                }
            }
            catch (SqlException)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 新しいデータを登録する。（主にメッセージ用）
        /// </summary>
        /// <param name="inputType">実行クエリタイプ</param>
        /// <param name="inputDt">データを追加する対象のデータテーブル</param>
        /// <param name="inputStrValues"></param>
        /// <returns></returns>
        private static bool TryInsertData(QUERYTYPE inputType, DataRow inputRow)
        {
            try
            {
                using (SqlConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                using (SqlCommand = new SqlCommand())
                using (SqlDataAdapter = new SqlDataAdapter())
                {
                    SqlConnection.Open();
                    SqlCommand.Connection = SqlConnection;
                    SqlCommand.CommandText = LoadQuery(inputType);
                    SqlTransaction = SqlConnection.BeginTransaction();
                    SqlCommand.Transaction = SqlTransaction;

                    try
                    {
                        using (var dt = new DataTable())
                        {
                            SqlDataAdapter.SelectCommand = SqlCommand;
                            SqlDataAdapter.Fill(dt);

                            dt.ImportRow(inputRow);

                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
                            sqlBld.DataAdapter = SqlDataAdapter;
                            SqlDataAdapter.InsertCommand = sqlBld.GetInsertCommand();

                            SqlDataAdapter.Update(dt);
                            SqlTransaction.Commit();
                            return true;
                        }
                    }
                    catch (SqlException)
                    {
                        SqlTransaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        SqlConnection.Close();
                    }
                }
            }
            catch (SqlException)
            {
                return false;
                throw;
            }
        }

        private static bool TryUpdateData(QUERYTYPE inputType, DataTable inputDt)
        {
            try
            {
                using (SqlConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                using (SqlCommand = new SqlCommand())
                using (SqlDataAdapter = new SqlDataAdapter())
                {
                    SqlConnection.Open();
                    SqlCommand.Connection = SqlConnection;
                    SqlCommand.CommandText = LoadQuery(inputType);
                    SqlTransaction = SqlConnection.BeginTransaction();
                    SqlCommand.Transaction = SqlTransaction;

                    try
                    {
                        using (inputDt)
                        {
                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
                            SqlDataAdapter.SelectCommand = SqlCommand;                            
                            sqlBld.DataAdapter = SqlDataAdapter;
                            SqlDataAdapter.UpdateCommand = sqlBld.GetUpdateCommand();

                            SqlDataAdapter.Update(inputDt);
                            SqlTransaction.Commit();
                            return true;
                        }
                    }
                    catch (SqlException)
                    {
                        SqlTransaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        SqlConnection.Close();
                    }
                }
            }
            catch (SqlException)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// ManagerDb.DataTableにデータをセットする
        /// </summary>
        private static DataTable GetData(QUERYTYPE inputType)
        {
            try
            {
                using (SqlConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                using (SqlCommand = new SqlCommand())
                using (SqlDataAdapter = new SqlDataAdapter())
                {
                    SqlConnection.Open();
                    SqlCommand.Connection = SqlConnection;
                    SqlCommand.CommandText = LoadQuery(inputType);

                    try
                    {
                        using (var dt = new DataTable())
                        {
                            SqlDataAdapter.SelectCommand = SqlCommand;
                            SqlDataAdapter.Fill(dt);
                            return dt;
                        }
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                    finally
                    {
                        SqlConnection.Close();
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// ManagerDb.DataTableにデータをセットする。inputParam で指定されたパラメーターのデータを取得する。
        /// </summary>
        /// <param name="inputType">実行クエリ</param>
        /// /// <param name="inputParam">パラメーターの名前</param>
        /// <param name="inputParam">パラメーターの値</param>
        /// <returns></returns>
        private static DataTable GetData(QUERYTYPE inputType, string paramName, object inputParam)
        {
            var param = new SqlParameter();
            param.ParameterName = paramName;
            param.Value = inputParam;

            try
            {
                using (SqlConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                using (SqlCommand = new SqlCommand())
                using (SqlDataAdapter = new SqlDataAdapter())
                {
                    SqlConnection.Open();
                    SqlCommand.Connection = SqlConnection;
                    SqlCommand.CommandText = LoadQuery(inputType);
                    SqlCommand.Parameters.Add(param);
                    
                    try
                    {
                        using (var dt = new DataTable())
                        {
                            SqlDataAdapter.SelectCommand = SqlCommand;
                            SqlDataAdapter.Fill(dt);
                            return dt;
                        }
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                    finally
                    {
                        SqlConnection.Close();
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// QUERYTYPE で指定された実行したいクエリを読み込む
        /// </summary>
        private static string LoadQuery(QUERYTYPE queryType)
        {
            try
            {
                using (var reader = new StreamReader(Configuration.GetSection("ExcuteQueryPath").GetValue<string>(queryType.ToString())))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                // ファイルまたはディレクトリの一部が見つからない場合にスローされる例外。
                throw;
            }
            catch (System.IO.DriveNotFoundException)
            {
                // 使用できないドライブまたは共有にアクセスしようとするとスローされる例外。
                throw;
            }
            catch (System.IO.EndOfStreamException)
            {
                // ストリームの末尾を越えて読み込もうとしたときにスローされる例外。
                throw;
            }
            catch (System.IO.FileLoadException)
            {
                // マネージ アセンブリが見つかったが、読み込むことができない場合にスローされる例外。
                throw;
            }
            catch (System.IO.FileNotFoundException)
            {
                // ディスク上に存在しないファイルにアクセスしようとして失敗したときにスローされる例外。
                throw;
            }
            catch (System.IO.IOException)
            {
                // I/O エラーが発生したときにスローされる例外。
                throw;
            }            
        }
    }
}
