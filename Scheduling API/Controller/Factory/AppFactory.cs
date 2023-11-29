using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Logic.Model.Structure;
using Scheduling_Logic.Model.Factory;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Config;
using Scheduling_Logic.Model.Database;
using Scheduling_API.Controller.State;
using Scheduling_API.Model.State;

namespace Scheduling_API.Controller.Factory
{
    public static class AppFactory
    {
        /*
         * Description: Identify the required fields to create the database connection and database connector properties.
         * 
         * @param:      [String]    providerName        identify which providername will be use for the database connection
         *                                              stored in the configuration setting.
         * 
         * mutations: [Type]    this.dbConnType         stores the current connection type in this object
         *            [String]  this.connectionString   stores the current connection string in this object
         *            
         * @return:   [Boolean]                         if the this.dbConntype is not null and this.connectionString is not
         *                                              null, empty string or white space.
         */
        public static IDbConfig? BuildDatabaseConfiguration(string providerName)
        {
            IDbConfig? dbConfig = null;

            switch (providerName)
            {
                case AppDbInfo.MySqlClient:
                    dbConfig = new MySqlConfig();
                    break;
            }

            return dbConfig;
        }

        public static AppState BuildAppState(IDbConfig dbConfig, string dbName)
        {
            return new AppState(dbConfig, dbName);
        }

        internal static DbSchema? BuildDbSchema(string dbName)
        {
            DbSchema? dbSchema = null;

            switch (dbName)
            {
                case AppDbInfo.ClientScheduleDbName:
                    dbSchema = new ClientScheduleDbSchema();
                    break;
            }

            return dbSchema;
        }

        internal static AppData? BuildAppData(string dbName) {
            AppData? appData = null;

            switch (dbName)
            {
                case AppDbInfo.ClientScheduleDbName:
                    appData = new AppData();
                    break;
            }
            
            return appData;
        }

        internal static DbConnector BuildDatabaseConnector(IDbConfig dbConfig)
        {
            return DbInstance.CreateDatabaseConnector(dbConfig);
        }

        internal static DbDataSet BuildDatabaseDataSet(AppState appState)
        {
            return DataInstance.CreateDbDataTable(appState.DbConnector, appState.DbSchema);
        }
    }
}
