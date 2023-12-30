using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Scheduling_Logic.Model.Config;

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
