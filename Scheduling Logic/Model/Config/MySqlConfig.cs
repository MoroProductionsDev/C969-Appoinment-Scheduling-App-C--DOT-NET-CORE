using System.Configuration;

namespace Scheduling_Logic.Model.Config
{
    // MySQl database class configuration
    public sealed class MySqlConfig : IDbConfig
    {
        private const string connectionStringName = "MySqlDBConn";
        private readonly string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

        public string ConnectionString => connectionString;
    }
}
