using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Library.Model.Structure;
using Scheduling_Library.Model.Factory;
using Scheduling_Library.Model.Data;
using Scheduling_Library.Model.Database;
using Scheduling_Library.Model.Config;
using Scheduling_Console_App.Controller.State;
using System.Xml.Linq;

namespace Scheduling_Console_App.Controller.Factory
{
    internal static class AppFactory
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
        internal static IDbConfig BuildDatabaseConfiguration(string providerName)
        {
            IDbConfig dbConfig = null;

            switch (providerName)
            {
                case DbProvider.MySqlClient:
                    dbConfig = new MySqlConfig();
                    break;
            }

            return dbConfig;
        }

        internal static DbSchema BuildDbSchema(string dbName)
        {
            DbSchema dbSchema = null;

            switch (dbName)
            {
                case DbName.ClientScheduleDbName:
                    dbSchema = new ClientScheduleDbSchema();
                    break;
            }

            return dbSchema;
        }

        internal static AppState BuildAppState(IDbConfig dbConfig, string dbName)
        {
            return new AppState(dbConfig, dbName);
        }

        internal static AppData BuildAppData(string dbName) {
            AppData appData = null;

            switch (dbName)
            {
                case DbName.ClientScheduleDbName:
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
