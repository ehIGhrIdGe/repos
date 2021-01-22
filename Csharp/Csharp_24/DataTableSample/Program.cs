using System;
using System.Data;
using System.Data.SqlClient;

namespace DataTableSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var csBuilder = new SqlConnectionStringBuilder();
                csBuilder.DataSource = "DESKTOP-LO22HNR";
                csBuilder.InitialCatalog = "testdb";
                csBuilder.IntegratedSecurity = true;

                using (var sqlConnection = new SqlConnection(csBuilder.ConnectionString))
                {
                    sqlConnection.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConnection;
                        cmd.CommandText = "SELECT * FROM Employee;";

                        //ExecuteReader()での結果をまず確認
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"名前：{reader[1]}");
                            }
                        }

                        using (var ada = new SqlDataAdapter())
                        {
                            ada.SelectCommand = cmd;

                            using (var dt = new DataTable())
                            {
                                ada.Fill(dt);
                                sqlConnection.Close();

                                Console.WriteLine($"行数は{dt.Rows.Count}です。");
                                Console.WriteLine($"列数は{dt.Columns.Count}です。");
                                Console.WriteLine($"インデックス指定：1行目第2項目は{dt.Rows[0][1].ToString().Trim()}です。");
                                Console.WriteLine($"列名を指定：1行目第2項目は{dt.Rows[0]["氏名"].ToString().Trim()}です。");
                            }
                        }
                    }
                }

                Console.ReadKey();
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.GetType().Name.ToString());
                Console.WriteLine(e.Server.ToString());
                Console.WriteLine(e.Message.ToString());
                Console.WriteLine(e.Errors.ToString());
                Console.WriteLine(e.Source.ToString());
            }
        }
    }
}
