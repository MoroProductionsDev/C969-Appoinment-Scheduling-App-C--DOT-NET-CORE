using MySql.Data.MySqlClient;
using Scheduling_Logic.Model.Config;
using System.Data.Common;

namespace Scheduling_Logic.Model.Factory
{
    internal static class DbFactory
    {
        internal static DbProviderFactory? CreateDbProviderFactory(IDbConfig? config)
        {
            DbProviderFactory? dbProviderFactory = null;

            if (config?.GetType() == typeof(MySqlConfig))
            {
                dbProviderFactory = MySqlClientFactory.Instance;
            }

            return dbProviderFactory;
        }
    }
}
