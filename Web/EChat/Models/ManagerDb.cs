using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EChat.Models
{
    public class ManagerDb
    {
        public enum TABLETYPE
        {
            ChatLogs,Users
        }

        private enum QUERYTYPE
        {
            ChatLogs, Users, ChatLogsUsers
        }

        public static IConfiguration Configuration { get; set; }

        private static string ExcuteQuery { get; set; }

        private static SqlConnection SqlConnection { get; set; }

        private static SqlCommand SqlCommand { get; set; }

        private static SqlDataAdapter SqlDataAdapter { get; set; }

        private static DataTable DataTable { get; set; }

        private static SqlTransaction SqlTransaction { get; set; }              

        public static bool InsertMessage(string msg)
        {
            LoadQuery(QUERYTYPE.ChatLogs);

            var tempDt = DataTable.Clone();
            var nr = tempDt.NewRow();
            nr[1] = DateTime.Now;
            nr[2] = msg;
            nr[3] = Environment.UserName;
            tempDt.Rows.Add(nr);

            return TryInsertData(nr);
        }

        /// <summary>
        /// メッセージを読み込む
        /// </summary>
        /// <returns></returns>
        public static List<Message> GetMessages()
        {
            LoadQuery(QUERYTYPE.ChatLogs);
            GetData();

            List<Message> messagesList = new List<Message>();

            foreach(DataRow message in DataTable.Rows)
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
                        using (DataTable = new DataTable())
                        {
                            SqlDataAdapter.SelectCommand = SqlCommand;
                            SqlDataAdapter.Fill(DataTable);

                            DataTable.ImportRow(inputRow);

                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
                            sqlBld.DataAdapter = SqlDataAdapter;
                            SqlDataAdapter.InsertCommand = sqlBld.GetInsertCommand();

                            SqlDataAdapter.Update(DataTable);
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

        private static void UpdateData()
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
                        using (DataTable = new DataTable())
                        {
                            SqlDataAdapter.SelectCommand = SqlCommand;
                            SqlDataAdapter.Fill(DataTable);

                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
                            sqlBld.DataAdapter = SqlDataAdapter;
                            SqlDataAdapter.UpdateCommand = sqlBld.GetUpdateCommand();

                            SqlTransaction.Commit();
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
                throw;
            }
        }

        /// <summary>
        /// ManagerDb.DataTableにデータをセットする
        /// </summary>
        /// <param name="excuteQuery">実行したいクエリ</param>
        private static void GetData()
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

                    try
                    {
                        using (DataTable = new DataTable())
                        {
                            SqlDataAdapter.SelectCommand = SqlCommand;
                            SqlDataAdapter.Fill(DataTable);
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
        private static void LoadQuery(QUERYTYPE queryType)
        {
            try
            {
                using (var reader = new StreamReader(Configuration.GetSection("ExcuteQueryPath").GetValue<string>(queryType.ToString())))
                {
                    ExcuteQuery = reader.ReadToEnd();
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
