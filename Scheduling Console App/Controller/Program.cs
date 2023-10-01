using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;
using static Scheduling_Console_App.Program;

namespace Scheduling_Console_App
{
    internal class Program
    {
        const string dbConnStrngKeyNm = "MySqlDBConn";

        static void Main(string[] args)
        {

            string connectionString = ConfigurationManager.ConnectionStrings[dbConnStrngKeyNm].ConnectionString;

            Console.WriteLine(connectionString);
        }
    }
}
