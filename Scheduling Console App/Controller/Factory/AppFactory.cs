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
using Scheduling_Console_App.Controller.Config;
using Scheduling_Console_App.Controller.State;

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
        internal static IDbConfiguration BuildDatabaseConfiguration(in String providerName)
        {
            IDbConfiguration dbConfig = null;

            switch (providerName)
            {
                case Provider.MySql:
                    dbConfig = new MySqlConfiguration();
                    break;
            }

            return dbConfig;
        }

        internal static IDbConnector BuildDatabaseConnector(in IDbConfiguration dbConfig)
        {
            return DbInstance.CreateDatabaseConnector(dbConfig.ConnectionType, dbConfig.ConnectionString);
        }

        internal static DbDataSet BuildDatabaseDataSet(AppState appState)
        {
            return new DbDataSet(appState.DbConnector, appState.DbSchema);
        }
    }
}
