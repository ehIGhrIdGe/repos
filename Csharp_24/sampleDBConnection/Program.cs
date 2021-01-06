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

            using (SqlConnection sqlConnection = new SqlConnection(csBuilder.ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    if(sqlConnection.State == ConnectionState.Open)
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
                catch(SqlException e)
                {
                    Console.WriteLine(e.GetType().Name.ToString());
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.ToString());
                }
                

            }

            Console.WriteLine("Hello World!");
        }
    }
}
