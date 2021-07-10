using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace eChat.Models
{
    public class DbUtil
    {
        public static string ConnectionStr { get; set; }

        public static SqlConnection NewConnection()
        {
            SqlConnection con = null;
            try
            {
                if (string.IsNullOrWhiteSpace(ConnectionStr))
                {
                    throw new ArgumentException();
                }
                con = new SqlConnection(ConnectionStr);
                con.Open();
                
            }
            catch
            {
                con?.Dispose();
            }
            return con;
        }

        public static IDisposable GetConnection(out SqlConnection connection)
        {
            connection = NewConnection();
            return connection;
        }

        public void GetMessages()
        {
            SqlConnection conn;
            using (GetConnection(out conn))
            {
                using (IDbTransaction trn = conn.BeginTransaction())
                {
                    try
                    {
                        var cmd = new SqlCommand("SELECT * FROM [ChatLogs]", conn);
                        var ada = new SqlDataAdapter(cmd);
                        var chatLogsDt = new DataTable();
                        ada.Fill(chatLogsDt);
                    }
                    finally
                    {
                        if (trn.Connection != null)
                        {
                            trn.Rollback();
                        }
                    }
                }
            }
        }
    }
}
