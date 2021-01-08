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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                                    

                                    cmd.CommandText = @"SELECT* FROM Employee";
                                    cmd.Transaction = tra;
                                    adapter.SelectCommand = cmd;

                                    adapter.Fill(dt);

                                    dataGridView1.AutoGenerateColumns = false;
                                    dataGridView1.DataSource = dt;
                                }
                            }
                            catch (SqlException)
                            {
                                tra.Rollback();
                                log.Info("-------------------ロールバックしました。-------------------");
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
                log.Error($"GetType().Name => {ex.GetType().Name}");
                log.Error($"Server         => {ex.Server}");
                log.Error($"Message        => {ex.Message}");
                log.Error($"Errors         => {ex.Errors}");
                log.Error($"Source         => {ex.Source}");
                log.Error($"InnerException => {ex.InnerException}");
                log.Error($"StackTrace     => {ex.StackTrace}");
            }
            catch (Exception ex)
            {
                log.Error($"GetType().Name => {ex.GetType().Name}");
                log.Error($"Message        => {ex.Message}");
                log.Error($"StackTrace     => {ex.StackTrace}");
                log.Error($"Source         => {ex.Source}");
                log.Error($"InnerException => {ex.InnerException}");
                log.Error($"TargetSite     => {ex.TargetSite}");
            }
        }
    }
}
