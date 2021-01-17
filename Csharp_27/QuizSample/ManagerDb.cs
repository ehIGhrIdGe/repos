using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace QuizSample
{
    static class ManagerDb
    {
        /// <summary>
        /// 取得したいテーブル
        /// </summary>
        public enum TABLETYPE
        {
            QuizTable, CategoryTable, QuizCategoryTable, QuizCategoryChoicesTable,QuizCategoryTableCaId
        }

        /// <summary>
        /// DBから結果を取得してデータテーブルにセットします。戻り値は、値のセットが成功したかどうか示します。
        /// </summary>
        /// <param name="tableType">取得したいテーブルのタイプ</param>
        /// /// <param name="dataTable">値をセットするデータテーブル</param>
        public static bool TryGetData(TABLETYPE tableType, out DataTable dataTable)
        {        
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuizSample.Properties.Settings.Quiz_Db"].ConnectionString))
                using (SqlCommand cmd = new SqlCommand())
                using (SqlDataAdapter ada = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = ConfigurationManager.AppSettings[tableType.ToString()];

                    SqlTransaction tra = con.BeginTransaction();
                    cmd.Transaction = tra;

                    try
                    {
                        using (var dt = new DataTable())
                        {
                            ada.SelectCommand = cmd;
                            ada.Fill(dt);
                            dataTable = dt;
                            return true;
                        }
                    }
                    catch (SqlException)
                    {
                        tra.Rollback();
                        ManagerLog.Logging(ManagerLog.LOGTYPE.Rollback);
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                ManagerLog.Logging(ex);
                dataTable = null;
                return false;
            }
        }


        /// <summary>
        /// 指定されたTABLETYPEのテーブルから、指定された行が削除されたデータテーブルをセットします。戻り値は、削除が成功したかどうか示します。
        /// </summary>
        /// <param name="tableType">取得したいテーブルのタイプ</param>
        /// <param name="rowNumber">削除したい行</param>
        /// <param name="dataTable">値をセットするデータテーブル</param>
        public static bool TryDelteData(TABLETYPE tableType, int rowNumber, out DataTable dataTable)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuizSample.Properties.Settings.Quiz_Db"].ConnectionString))
                using (SqlCommand cmd = new SqlCommand())
                using (SqlDataAdapter ada = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = ConfigurationManager.AppSettings[tableType.ToString()];

                    SqlTransaction tra = con.BeginTransaction();
                    cmd.Transaction = tra;

                    try
                    {
                        using (var dt = new DataTable())
                        {
                            ada.SelectCommand = cmd;

                            ada.Fill(dt);
                            dt.Rows[rowNumber].Delete();

                            //コマンドを自動設定
                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
                            sqlBld.DataAdapter = ada;
                            ada.DeleteCommand = sqlBld.GetDeleteCommand();

                            ManagerLog.Logging(ManagerLog.LOGTYPE.Delete, sqlBld.GetDeleteCommand().ToString());

                            //DBに反映
                            ada.Update(dt);
                            tra.Commit();

                            dataTable = dt;
                            return true;
                        }
                    }
                    catch (SqlException)
                    {
                        tra.Rollback();
                        ManagerLog.Logging(ManagerLog.LOGTYPE.Rollback);
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                dataTable = null;
                ManagerLog.Logging(ex);                
                return false;
            }
            catch (IndexOutOfRangeException ex)
            {
                dataTable = null;
                ManagerLog.Logging(ex);
                return false;
            }
            catch (InvalidOperationException ex)
            {
                dataTable = null;
                ManagerLog.Logging(ex);
                return false;
            }
        }

        /// <summary>
        /// DBからWhereで絞った結果をデータテーブルにセットします。戻り値は、値のセットが成功したかどうか示します。
        /// </summary>
        /// <param name="tableType">取得したいテーブルのタイプ</param>
        /// /// <param name="dataTable">値をセットするデータテーブル</param>
        public static bool TryGetWhereCategory(TABLETYPE tableType, int categoryId, out DataTable dataTable)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuizSample.Properties.Settings.Quiz_Db"].ConnectionString))
                using (SqlCommand cmd = new SqlCommand())
                using (SqlDataAdapter ada = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = ConfigurationManager.AppSettings[tableType.ToString()] + categoryId;

                    SqlTransaction tra = con.BeginTransaction();
                    cmd.Transaction = tra;

                    try
                    {
                        using (var dt = new DataTable())
                        {
                            ada.SelectCommand = cmd;
                            ada.Fill(dt);
                            dataTable = dt;
                            return true;
                        }
                    }
                    catch (SqlException)
                    {
                        tra.Rollback();
                        ManagerLog.Logging(ManagerLog.LOGTYPE.Rollback);
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                ManagerLog.Logging(ex);
                dataTable = null;
                return false;
            }
        }
    }
}
