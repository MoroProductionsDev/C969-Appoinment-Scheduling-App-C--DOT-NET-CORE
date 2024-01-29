using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Config;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Database;
using Scheduling_Logic.Model.Factory;
using Scheduling_Logic.Model.Structure;

namespace Scheduling_API.Controller.Factory
{
    // Factory class for the app's API (MVC) model.
    public static class AppFactory
    {
        public static AppState BuildAppState(IDbConfig? dbConfig, string dbName)
        {
            return new AppState(dbConfig, dbName);
        }

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
                case AppInfo.MySqlClientNameSpace:
                    dbConfig = new MySqlConfig();
                    break;
            }

            return dbConfig;
        }

        internal static DbSchema? BuildDbSchema(string dbName)
        {
            DbSchema? dbSchema = null;

            switch (dbName)
            {
                case AppInfo.ClientScheduleDbName:
                    dbSchema = new ClientScheduleDbSchema();
                    break;
            }

            return dbSchema;
        }

        internal static AppData? BuildAppData(string dbName)
        {
            AppData? appData = null;

            switch (dbName)
            {
                case AppInfo.ClientScheduleDbName:
                    appData = new AppData();
                    break;
            }

            return appData;
        }

        internal static AppDataView? BuildAppDataView(AppState appState, string dbName)
        {
            AppDataView? appDataView = null;

            switch (dbName)
            {
                case AppInfo.ClientScheduleDbName:
                    appDataView = new AppDataView(appState);
                    break;
            }

            return appDataView;
        }

        internal static DbConnector? BuildDatabaseConnector(IDbConfig dbConfig)
        {
            return DbInstance.CreateDatabaseConnector(dbConfig);
        }

        // Must have checked for nulls
        internal static DbDataSet? BuildDatabaseDataSet(AppState appState)
        {
            return DataInstance.CreateDbDataTable(appState.DbConnector!, appState.DbSchema!);
        }
    }
}
