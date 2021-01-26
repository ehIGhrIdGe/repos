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

        private static string ExcuteQuery { get; set; }

        private static SqlConnection SqlConnection { get; set; }

        private static SqlCommand SqlCommand { get; set; }

        private static SqlDataAdapter SqlDataAdapter { get; set; }

        private static DataTable DataTable { get; set; }

        private static SqlTransaction SqlTransaction { get; set; }

        public static IConfiguration Configuration { get; set; }        

        public static List<Messages> GetMessages()
        {
            LoadQuery();
            GetData();

            List<Messages> messagesList = new List<Messages>();

            foreach(DataRow message in DataTable.Rows)
            {
                var temp = new Messages((int)message[0], (DateTime)message[1], message[2].ToString(), message[3].ToString());
                messagesList.Add(temp);
            }

            return messagesList;
        }

        /// <summary>
        /// ManagerDb.DataTableにデータをセットする
        /// </summary>
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

                    SqlTransaction = SqlConnection.BeginTransaction();
                    SqlCommand.Transaction = SqlTransaction;

                    try
                    {
                        using (DataTable = new DataTable())
                        {
                            SqlDataAdapter.SelectCommand = SqlCommand;
                            SqlDataAdapter.Fill(DataTable);
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

        private static void BeginTr()
        {
            SqlTransaction = SqlConnection.BeginTransaction();
        }

        /// <summary>
        /// ManagerDb.ExcuteQueryに実行したいクエリをセットする
        /// </summary>
        private static void LoadQuery()
        {
            try
            {
                using (var reader = new StreamReader(Configuration.GetSection("ExcuteQueryPath").GetValue<string>("ChatLogs")))
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
