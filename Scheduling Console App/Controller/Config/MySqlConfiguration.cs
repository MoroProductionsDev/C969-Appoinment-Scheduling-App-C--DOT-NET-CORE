using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Library.Model.Structure;

namespace Scheduling_Console_App.Controller.Config
{
    internal sealed class MySqlConfiguration : IDbConfiguration
    {
        private const string connectionStringName = "MySqlDBConn";
        private readonly String connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        private readonly Type connectionType = DatabaseType.MySqlConn;
        public string ConnectionString => connectionString;
        public Type ConnectionType => connectionType;
    }
}
