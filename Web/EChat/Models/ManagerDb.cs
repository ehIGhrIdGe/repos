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
            ChatLogs, Users, ChatLogsUsers, GetMessages, GetUserInfo
        }

        public static IConfiguration Configuration { get; set; }

        private static string ExcuteQuery { get; set; }

        private static SqlConnection SqlConnection { get; set; }

        private static SqlCommand SqlCommand { get; set; }

        private static SqlDataAdapter SqlDataAdapter { get; set; }

        private static DataTable MsgDataTable { get; set; }

        private static DataTable UserDataTable { get; set; }

        private static SqlTransaction SqlTransaction { get; set; }              

        public static bool UpdateUserInfo(User userInfo)
        {
            UserDataTable.Rows[0]["UserId"] = userInfo.UserId;
            UserDataTable.Rows[0]["UserName"] = userInfo.UserName;
            UserDataTable.Rows[0]["PasswordType"] = userInfo.PasswordType;
            UserDataTable.Rows[0]["PasswordSalt"] = userInfo.PasswordSalt;
            UserDataTable.Rows[0]["Password"] = userInfo.Password;
            UserDataTable.Rows[0]["IsAdministrator"] = userInfo.IsAdministrator;

            return TryUpdateData(QUERYTYPE.Users, UserDataTable);
        }

        public static bool InsertMessage(string msg, string userId)
        {
            var tempDt = MsgDataTable.Clone();
            string[] strValues = new string[] { msg, userId };
            var nr = tempDt.NewRow();
            nr[1] = DateTime.Now;
            nr[2] = msg;
            nr[3] = Environment.UserName;
            nr[3] = userId;
            tempDt.Rows.Add(nr);
            //DataTable.ImportRow(inputRow);

            return TryInsertData(QUERYTYPE.ChatLogs, nr);
        }

        /// <summary>
        /// ユーザーデータを読み込む
        /// </summary>
        /// <returns></returns>
        public static List<User> GetUserInfo(string inputUserId)
        {
            UserDataTable = GetData(QUERYTYPE.GetUserInfo, "@userid", inputUserId);
            List<User> userInfoList = null;
            
            if (UserDataTable.Rows.Count != 0)
            {
                userInfoList = new List<User>();
                var tempUser = new User(UserDataTable.Rows[0][0].ToString(), UserDataTable.Rows[0][1].ToString(), (byte)UserDataTable.Rows[0][2], UserDataTable.Rows[0][3].ToString(), UserDataTable.Rows[0][4].ToString(), (bool)UserDataTable.Rows[0][5]);
                userInfoList.Add(tempUser);
            }

            return userInfoList;
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
                var temp = new Message((int)message[0], (DateTime)message[1], message[2].ToString(), message[3].ToString());
                messagesList.Add(temp);
            }

            return messagesList;
        }

        private static bool TryInsertData(DataRow inputRow)
        {
            try
            {
                using (SqlConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                using (SqlCommand = new SqlCommand())
                using (SqlDataAdapter = new SqlDataAdapter())
                {
                    SqlConnection.Open();
                    SqlCommand.Connection = SqlConnection;
                    SqlCommand.CommandText = ExcuteQuery;

                    SqlTransaction = SqlConnection.BeginTransaction();
                    SqlCommand.Transaction = SqlTransaction;

                    try
                    {
                        using (MsgDataTable = new DataTable())
                        {
                            SqlDataAdapter.SelectCommand = SqlCommand;
                            SqlDataAdapter.Fill(MsgDataTable);

                            MsgDataTable.ImportRow(inputRow);

                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
                            sqlBld.DataAdapter = SqlDataAdapter;
                            SqlDataAdapter.InsertCommand = sqlBld.GetInsertCommand();

                            SqlDataAdapter.Update(MsgDataTable);
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
                            SqlDataAdapter.SelectCommand = SqlCommand;
                            //SqlDataAdapter.Fill(dt);

                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
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
