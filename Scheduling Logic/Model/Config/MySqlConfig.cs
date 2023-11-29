using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Logic.Model.Config
{
    public sealed class MySqlConfig : IDbConfig
    {
        private const string connectionStringName = "MySqlDBConn";
        private readonly string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

        public string ConnectionString => connectionString;
    }
}
