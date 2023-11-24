using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Library.Model.Structure;
using MySql.Data.MySqlClient;

namespace Scheduling_Library.Model.Config
{
    public sealed class MySqlConfig : IDbConfig
    {
        private const string connectionStringName = "MySqlDBConn";
        private readonly String connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

        public string ConnectionString => connectionString;
    }
}
