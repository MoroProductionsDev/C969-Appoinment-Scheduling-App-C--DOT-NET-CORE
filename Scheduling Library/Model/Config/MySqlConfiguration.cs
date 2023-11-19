using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Library.Model.Structure;
using MySql.Data.MySqlClient;

namespace Scheduling_Console_App.Controller.Config
{
    public sealed class MySqlConfiguration : IDbConfiguration
    {
        private const string connectionStringName = "MySqlDBConn";
        private readonly String connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        private readonly Type connectionType = typeof(MySqlConnection);

        public string ConnectionString => connectionString;
        public Type ConnectionType => connectionType;
    }
}
