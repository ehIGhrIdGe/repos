using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;

namespace DataTableSample2
{
    class Program
    {
        static void Main(string[] args)
        {            
            try
            {
                Lesson10();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.GetType().Name.ToString());
                Console.WriteLine(e.Server.ToString());
                Console.WriteLine(e.Message.ToString());
                Console.WriteLine(e.Errors.ToString());
                Console.WriteLine(e.Source.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetType().Name => {e.GetType().Name}");
                Console.WriteLine($"Message        => {e.Message}");
                Console.WriteLine($"StackTrace     => {e.StackTrace}");
                Console.WriteLine($"Source         => {e.Source}");
                Console.WriteLine($"InnerException => {e.InnerException}");
                Console.WriteLine($"TargetSite     => {e.TargetSite}");
            }

            Console.ReadKey();
        }

        //アプリケーション構成ファイルのあれこれ
        static void Lesson10()
        {
            //アプリケーション構成ファイルの読み込み
            Console.WriteLine("アプリケーション構成ファイルの読み込み");
            Console.WriteLine(Properties.Settings.Default["設定値1"].ToString());
            Console.WriteLine(Properties.Settings.Default["設定値2"].ToString());
            Console.WriteLine(Properties.Settings.Default["接続文字列1"].ToString());

            //ConfigurationManager での読み込み
            Console.WriteLine("\nConfigurationManager での読み込み");
            var conStr = ConfigurationManager.ConnectionStrings["DataTableSample2.Properties.Settings.接続文字列1"].ConnectionString;
            Console.WriteLine(conStr);            
        }

        //トランザクション処理
        static void Lesson9()
        {
            var csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "DESKTOP-LO22HNR";
            csBuilder.InitialCatalog = "testdb";
            csBuilder.IntegratedSecurity = true;

            try
            {
                using (var sqlConnection = new SqlConnection(csBuilder.ConnectionString))
                {
                    using (var cmd = new SqlCommand())
                    {
                        //接続を開始（一度閉じるとトランザクションが無効になってしまうので、最後の処理まで開きっぱなしにする）
                        Console.WriteLine("open");
                        sqlConnection.Open();
                        cmd.Connection = sqlConnection;

                        //トランザクションの開始
                        var tra = sqlConnection.BeginTransaction();
                        try
                        {
                            using (var ada = new SqlDataAdapter())
                            {
                                cmd.Transaction = tra;
                                cmd.CommandText = @"SELECT * FROM Employee";
                                ada.SelectCommand = cmd;

                                using (var dt = new DataTable())
                                {
                                    //こことかで一回閉じて、ada.Update(dt); の直前に再度接続を開くと、tra.Commit(); の部分でエラーになる。
                                    ada.Fill(dt);

                                    //インサートするものを設定（キー重複などでエラーを起こすことで、ロールバックできる）
                                    var nRow = dt.NewRow();
                                    nRow[0] = 1;
                                    nRow["氏名"] = "yamayama";
                                    nRow["給与"] = 9999999;
                                    dt.Rows.Add(nRow);

                                    var nRow2 = dt.NewRow();
                                    nRow2[0] = 21;
                                    nRow2["氏名"] = "momoguchi";
                                    nRow2["給与"] = 222222222;
                                    dt.Rows.Add(nRow2);

                                    //更新するものを設定
                                    dt.Rows[0][2] = 99999999;

                                    //コマンドを自動的に発行
                                    var sqlBuilder = new SqlCommandBuilder();
                                    sqlBuilder.DataAdapter = ada;
                                    ada.InsertCommand = sqlBuilder.GetInsertCommand();
                                    ada.UpdateCommand = sqlBuilder.GetUpdateCommand();

                                    //DBに反映
                                    ada.Update(dt);
                                    tra.Commit();
                                    sqlConnection.Close();
                                    Console.WriteLine("コミットしたよ。");
                                    Console.WriteLine("close…");
                                }
                            }
                        }
                        catch (SqlException)
                        {
                            Console.WriteLine("ロールバックしちゃった。");
                            tra.Rollback();
                            throw;
                        }
                        catch (DirectoryNotFoundException)
                        {
                            throw;
                        }
                        catch (FileNotFoundException)
                        {
                            throw;
                        }
                        catch (IOException)
                        {
                            throw;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("ロールバックしちゃった。");
                            tra.Rollback();
                            throw;
                        }
                    }
                }
                Console.ReadKey();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //DataTableの操作
        static void Lesson8()
        {
            var csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "DESKTOP-LO22HNR";
            csBuilder.InitialCatalog = "testdb";
            csBuilder.IntegratedSecurity = true;

            try
            {
                using (var sqlConnection = new SqlConnection(csBuilder.ConnectionString))
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConnection;
                        cmd.CommandText = @"SELECT * FROM Employee";

                        using (var ada = new SqlDataAdapter())
                        {
                            ada.SelectCommand = cmd;

                            using (var dt = new DataTable())
                            {
                                Console.WriteLine("open");
                                sqlConnection.Open();
                                ada.Fill(dt);
                                sqlConnection.Close();
                                Console.WriteLine("close");

                                //インサートするものを設定?
                                var nRow = dt.NewRow();
                                nRow[0] = 14;
                                nRow["氏名"] = "gorge";
                                nRow["給与"] = 999;
                                dt.Rows.Add(nRow);

                                var nRow2 = dt.NewRow();
                                nRow2[0] = 15;
                                nRow2["氏名"] = "kono";
                                nRow2["給与"] = 999;
                                dt.Rows.Add(nRow2);

                                //更新するものを設定?
                                dt.Rows[0][2] = 11111111;

                                //コマンドを自動的に発行
                                var sqlBuilder = new SqlCommandBuilder();
                                sqlBuilder.DataAdapter = ada;
                                ada.InsertCommand = sqlBuilder.GetInsertCommand();
                                ada.UpdateCommand = sqlBuilder.GetUpdateCommand();

                                //発行したコマンドの中身を確認
                                Console.WriteLine("\n発行したコマンドの中身を確認");
                                Console.WriteLine(ada.InsertCommand.CommandText.ToString());
                                Console.WriteLine(ada.UpdateCommand.CommandText.ToString());

                                //再接続し、DBに反映
                                Console.WriteLine("\n再接続し、DBに反映");
                                Console.WriteLine("re:opne");
                                sqlConnection.Open();
                                ada.Update(dt);
                                sqlConnection.Close();
                                Console.WriteLine("close");
                            }
                        }
                    }
                }
                Console.ReadKey();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static void Lesson7()
        {
            var csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "DESKTOP-LO22HNR";
            csBuilder.InitialCatalog = "testdb";
            csBuilder.IntegratedSecurity = true;

            Console.Write("名前は？ => ");
            var name = Console.ReadLine();

            try
            {
                using (var sqlConnection = new SqlConnection(csBuilder.ConnectionString))
                {
                    sqlConnection.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConnection;
                        cmd.CommandText = @"SELECT * FROM Employee WHERE 氏名 = @param";

                        var param = new SqlParameter();
                        param.ParameterName = "@param";
                        param.Value = name;
                        cmd.Parameters.Add(param);

                        using (var ada = new SqlDataAdapter())
                        {
                            ada.SelectCommand = cmd;

                            using (var dt = new DataTable())
                            {
                                ada.Fill(dt);
                                sqlConnection.Close();

                                Console.WriteLine($"該当する社員は{dt.Rows.Count}名です。"); ;
                            }
                        }
                    }
                }

                Console.ReadKey();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static void Lesson6()
        {
            var csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "DESKTOP-LO22HNR";
            csBuilder.InitialCatalog = "testdb";
            csBuilder.IntegratedSecurity = true;

            Console.Write("名前は？ => ");
            var name = Console.ReadLine();

            try
            {
                using (var sqlConnection = new SqlConnection(csBuilder.ConnectionString))
                {
                    sqlConnection.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConnection;
                        cmd.CommandText = $"SELECT * FROM Employee WHERE 氏名 = '{name}';";

                        using (var ada = new SqlDataAdapter())
                        {
                            ada.SelectCommand = cmd;

                            using (var dt = new DataTable())
                            {
                                ada.Fill(dt);
                                sqlConnection.Close();

                                Console.WriteLine($"該当する社員は{dt.Rows.Count}名です。"); ;
                            }
                        }
                    }
                }

                Console.ReadKey();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static void Lesson5()
        {
            try
            {
                var csBuilder = new SqlConnectionStringBuilder();
                csBuilder.DataSource = "DESKTOP-LO22HNR";
                csBuilder.InitialCatalog = "testdb";
                csBuilder.IntegratedSecurity = true;

                try
                {
                    using (var sqlConnection = new SqlConnection(csBuilder.ConnectionString))
                    {
                        Console.WriteLine("DBへの接続します。");
                        sqlConnection.Open();

                        using (var cmd = new SqlCommand())
                        {
                            cmd.Connection = sqlConnection;
                            cmd.CommandText = "SELECT * FROM Employee;";

                            using (var ada = new SqlDataAdapter())
                            {
                                ada.SelectCommand = cmd;

                                using (var dt = new DataTable())
                                {
                                    ada.Fill(dt);
                                    sqlConnection.Close();
                                    Console.WriteLine("DBへの接続を切断しました。\n");

                                    using (var writer = new StreamWriter(@"C:\Users\eij\source\repos\Csharp_24\DataTableSample2\text\test.txt"))
                                    {
                                        var writeText = string.Empty;

                                        for (var c = 0; c < dt.Columns.Count; c++)
                                        {
                                            writeText = writeText + dt.Columns[c].ColumnName.ToString().Trim() + "\t";
                                            writeText.Trim();
                                        }
                                        writeText = writeText + "\n";

                                        for (var r = 0; r < dt.Rows.Count; r++)
                                        {
                                            for (var c = 0; c < dt.Columns.Count; c++)
                                            {
                                                writeText = writeText + dt.Rows[r][c].ToString().Trim() + "\t";
                                                writeText.Trim();
                                            }

                                            writeText = writeText + "\n";
                                        }
                                        writer.Write(writeText);
                                        Console.WriteLine(@"C:\Users\eij\source\repos\Csharp_24\DataTableSample2\text\test.txt へ保存しました。");
                                    }

                                }
                            }
                        }
                    }

                    Console.ReadKey();
                }
                catch (DirectoryNotFoundException)
                {
                    throw;
                }
                catch (FileNotFoundException)
                {
                    throw;
                }
                catch (IOException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
