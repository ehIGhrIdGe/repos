using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using NLog;


namespace DataGridViewSample
{
    public partial class Form1 : Form
    {
        private DataTable Dt { get; set; }

        public Form1()
        {
            InitializeComponent();

            var log = LogManager.GetCurrentClassLogger();

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataGridViewSample.Properties.Settings.接続文字列1"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter())
                        {
                            con.Open();

                            cmd.Connection = con;
                            var tra = con.BeginTransaction();
                            try
                            {
                                using (DataTable dt = new DataTable())
                                {


                                    cmd.CommandText = @"SELECT * FROM [testdb].[dbo].[Employee] E INNER JOIN [testdb].[dbo].[Department] D ON E.部門番号 = D.部門番号;";
                                    cmd.Transaction = tra;
                                    adapter.SelectCommand = cmd;

                                    adapter.Fill(dt);
                                    con.Close();

                                    Dt = dt;
                                }
                            }
                            catch (SqlException)
                            {
                                tra.Rollback();
                                log.Info(" ============================= ロールバックしました。=============================");
                                throw;
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }


                    }
                }
            }
            catch (SqlException ex)
            {
                log.Error($" ************************ ログの出力開始 ************************");
                log.Error($" GetType().Name => {ex.GetType().Name}");
                log.Error($" Server         => {ex.Server}");
                log.Error($" Message        => {ex.Message}");
                log.Error($" Errors         => {ex.Errors}");
                log.Error($" Source         => {ex.Source}");
                log.Error($" InnerException => {ex.InnerException}");
                log.Error($" StackTrace     => {ex.StackTrace}");
                log.Error($" ************************ ログの出力終了 ************************");
            }
            catch (Exception ex)
            {
                log.Error($" ************************ ログの出力開始 ************************");
                log.Error($" GetType().Name => {ex.GetType().Name}");
                log.Error($" Message        => {ex.Message}");
                log.Error($" StackTrace     => {ex.StackTrace}");
                log.Error($" Source         => {ex.Source}");
                log.Error($" InnerException => {ex.InnerException}");
                log.Error($" TargetSite     => {ex.TargetSite}");
                log.Error($" ************************ ログの出力終了 ************************");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = Dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var column = e.ColumnIndex;

            if (e.ColumnIndex == 3)
            {
                MessageBox.Show($"社員番号 => {Dt.Rows[e.RowIndex]["社員番号"].ToString()}\n" +
                                $"氏名     => {Dt.Rows[e.RowIndex]["氏名"].ToString()}\n" +
                                $"部門     => {Dt.Rows[e.RowIndex]["部門名"].ToString()}",
                                "編集", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
