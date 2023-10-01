using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Console_App.Data
{
    internal class DatabaseConnector
    {
        private MySqlConnection conn;
        public DatabaseConnector(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection Connection
        {
            get
            {
                return conn;
            }
            set // Validate if the current connection is not null and if it is open.
            {
                if (conn?.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn = value;
            }
        }

        public void Open()
        {
            try
            {
                conn?.Open();
            } catch (MySqlException mySqlExcp) {
                Console.WriteLine($"{mySqlExcp}\n{mySqlExcp.Message}");
            }
        }
    }
}
