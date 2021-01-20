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
            QuizTable, ChoicesTable, CategoryTable, QuizCategoryTable, QuizCategoryChoicesTable, QuizCategoryTableCaId
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
        /// 指定されたTABLETYPEのテーブルへ、指定された行インサートします。戻り値は、インサートが成功したかどうか示します。
        /// </summary>
        /// <param name="tableType">インサートしたいテーブルのタイプ</param>
        /// <param name="newRow">インサートしたい行データ</param>
        /// <param name="dataTable">結果をセットするデータテーブル</param>
        public static bool TryUpdateData(TABLETYPE tableType, DataTable inputDt)
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


                            //コマンドを自動設定
                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
                            sqlBld.DataAdapter = ada;
                            ada.UpdateCommand = sqlBld.GetUpdateCommand();

                            ManagerLog.Logging(ManagerLog.LOGTYPE.Update, sqlBld.GetUpdateCommand().CommandText);

                            //DBに反映
                            ada.Update(inputDt);
                            tra.Commit();

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
                return false;
            }
            catch (IndexOutOfRangeException ex)
            {
                ManagerLog.Logging(ex);
                return false;
            }
            catch (InvalidOperationException ex)
            {
                ManagerLog.Logging(ex);
                return false;
            }
        }

        /// <summary>
        /// 指定されたTABLETYPEのテーブルへ、指定された行インサートします。戻り値は、インサートが成功したかどうか示します。
        /// </summary>
        /// <param name="tableType">インサートしたいテーブルのタイプ</param>
        /// <param name="newRow">インサートしたい行データ</param>
        /// <param name="dataTable">結果をセットするデータテーブル</param>
        public static bool TryInsertData(TABLETYPE tableType, DataRow inputRow)
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

                            //配列のコピーで行を追加する方法。この場合、FQuizDetail.btnUpdate_Clickの"dtQuiz.Rows.Add(nRowQuiz);"の一文はいらない。
                            //var nRow = dt.NewRow();
                            //nRow.ItemArray = inputRow.ItemArray;
                            //dt.Rows.Add(nRow);

                            //DataTable.ImportRow(DataRow)で行を追加する方法。この場合、FQuizDetail.btnUpdate_Clickの"dtQuiz.Rows.Add(nRowQuiz);"の一文が必要。（ImportRow は同一の構造を持つ DataTable に対して、既に DataTable 内で使用されている DataRow のみコピーすることができるため。）
                            dt.ImportRow(inputRow);

                            //コマンドを自動設定
                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
                            sqlBld.DataAdapter = ada;
                            ada.InsertCommand = sqlBld.GetInsertCommand();

                            ManagerLog.Logging(ManagerLog.LOGTYPE.Insert, sqlBld.GetInsertCommand().CommandText);

                            //DBに反映
                            ada.Update(dt);
                            tra.Commit();

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
                return false;
            }
            catch (IndexOutOfRangeException ex)
            {
                ManagerLog.Logging(ex);
                return false;
            }
            catch (InvalidOperationException ex)
            {
                ManagerLog.Logging(ex);
                return false;
            }
        }

        /// <summary>
        /// 指定されたTABLETYPEのテーブルから、指定された行が削除されたデータテーブルをセットします。戻り値は、削除が成功したかどうか示します。
        /// </summary>
        /// <param name="tableType">取得したいテーブルのタイプ</param>
        /// <param name="inputQuizId">削除したいクイズの quiz_id</param>
        public static bool TryDelteData(TABLETYPE tableType, int inputQuizId)
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

                            //deleteの対照をセット
                            var rowCount = dt.Select($"quiz_id = {inputQuizId}").Count();
                            for (var i = 0; i < rowCount; i++)
                            {
                                dt.Select($"quiz_id = {inputQuizId}")[0].Delete();
                            }

                            //コマンドを自動設定                            
                            SqlCommandBuilder sqlBld = new SqlCommandBuilder();
                            sqlBld.DataAdapter = ada;
                            ada.DeleteCommand = sqlBld.GetDeleteCommand();

                            ManagerLog.Logging(ManagerLog.LOGTYPE.Delete, sqlBld.GetDeleteCommand().CommandText);

                            //DBに反映
                            ada.Update(dt);                        
                            tra.Commit();                            

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
                return false;
            }
            catch (IndexOutOfRangeException ex)
            {
                ManagerLog.Logging(ex);
                return false;
            }
            catch (InvalidOperationException ex)
            {
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
