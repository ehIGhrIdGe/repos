using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace sampleDBConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            var csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "DESKTOP-LO22HNR";
            csBuilder.InitialCatalog = "testdb";
            csBuilder.IntegratedSecurity = true;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(csBuilder.ConnectionString))
                {
                    sqlConnection.Open();

                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        Console.WriteLine("DB Connection is open.");
                    }
                    else
                    {
                        Console.WriteLine("DB Connection is not open.");
                    }

                    sqlConnection.Close();

                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        Console.WriteLine("DB Connection is open even.");
                    }
                    else
                    {
                        Console.WriteLine("DB Connection is close.");
                    }
                }

                //Selectコマンドを実行してみる
                Console.WriteLine();
                using (SqlConnection sqlConnection = new SqlConnection(csBuilder.ConnectionString))
                {
                    sqlConnection.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConnection;
                        cmd.CommandText = "SELECT * FROM Employee;";

                        SqlDataReader reader = cmd.ExecuteReader();

                        Console.WriteLine("適当に全件表示");
                        while (reader.Read())
                        {
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write($"{reader[i].ToString().Trim()}-");
                            }
                            Console.WriteLine();
                        }
                        reader.Close();
                        reader = cmd.ExecuteReader();

                        Console.WriteLine("\n社員番号、氏名をハイフン区切りで表示。取得件数も表示。");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]}-{reader[1]}");
                        }
                        reader.Close();
                        cmd.CommandText = "SELECT COUNT(*) FROM Employee;";
                        Console.WriteLine($"取得件数は{cmd.ExecuteScalar()}件です。");
                    }
                    sqlConnection.Close();

                    Console.ReadKey();
                }

                //Update,delete等を実行してみる
                Console.WriteLine();
                using (SqlConnection sqlConnection = new SqlConnection(csBuilder.ConnectionString))
                {
                    sqlConnection.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConnection;
                        cmd.CommandText = "DELETE FROM Employee;";
                        Console.WriteLine($"削除件数は{cmd.ExecuteNonQuery()}件です。");
                        Console.ReadKey();

                        cmd.CommandText = "insert into [testdb].[dbo].[Employee] select * from testdb.dbo.社員;";
                        Console.WriteLine($"追加件数は{cmd.ExecuteNonQuery()}件です。");
                        Console.ReadKey();

                        cmd.CommandText = "SELECT * FROM Employee;";
                        SqlDataReader reader = cmd.ExecuteReader();
                        Console.WriteLine("\n更新前の状態です。");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]}-{reader[1].ToString().Trim()}-{reader[2]}");
                        }
                        reader.Close();
                        Console.ReadKey();

                        cmd.CommandText = "UPDATE [testdb].[dbo].[Employee] SET 給与 = 給与*10;";
                        Console.WriteLine($"\n更新件数は{cmd.ExecuteNonQuery()}件です。");
                        Console.ReadKey();

                        cmd.CommandText = "SELECT * FROM Employee;";
                        reader = cmd.ExecuteReader();
                        Console.WriteLine("\n更新後の状態です。");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]}-{reader[1].ToString().Trim()}-{reader[2]}");
                        }
                        reader.Close();
                        Console.ReadKey();
                    }                                           
                    sqlConnection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.GetType().Name.ToString());
                Console.WriteLine(e.Message);
                Console.WriteLine(e.ToString());
            }
        }
    }
}
