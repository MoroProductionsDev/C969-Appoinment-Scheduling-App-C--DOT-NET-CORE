using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Scheduling_Library;
using System.ComponentModel;
using Scheduling_Library.Model.structure;
using Scheduling_Library.Model.factory;
using Scheduling_Library.Model.data;
using Scheduling_Library.Model.database;

namespace Scheduling_Console_App
{
    internal sealed class Application
    {

        private Type dbConnType;
        private string connectionString;

        // The "connectionString" element's name int the "App.config" file that will be use to connect
        // to the correct database data.
        private const string dbConnStrngKeyNm = "MySqlDBConn";

        /*
             * Description: Runs the application.
             * 
             * @param:      [String]    providerName        identify which providername will be use for the database connection
             *                                              stored in the configuration setting.
        */
        public void Run(string providerName)
        {
            if (!IdentifyRequiredFields(providerName))
            {
                return;
            }

            DbSchema clientSchema = new DbStructure.ClientDatabaseSchema();

            IDbConnector connector = BuildConnector();

 
            OpeningDatabaseConnectionTest(connector);

            /*ReadingDatabaseTest(connector, commandText);*/

            MappingDatabaseTest(in connector, clientSchema);

            CloseAndDisposeDatabaseObj(connector);
        }

        private void MappingDatabaseTest(in IDbConnector connector, in DbSchema dbSchema)
        {
            DbDataSet databaseDataTable = new DbDataSet(in connector, in dbSchema);

            databaseDataTable.Populate();
        }

        private void writingDatabaseTest(IDbConnector connector, String commandText)
        {
            connector?.CreateDbCommand(commandText);
        }


        private void OpeningDatabaseConnectionTest(IDbConnector connector)
        {
            connector?.OpenConnection();

            // Output
            ConsoleOutput.WriteLine($" Connection State: {connector.ConnectionState}");
            // out-end
        }

        private void CloseAndDisposeDatabaseObj(IDbConnector connector)
        {
            connector.Dispose();

            // Output
            ConsoleOutput.WriteLine($"Disposing object: {connector}");
            // out-end
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
        private bool IdentifyRequiredFields(string providerName)
        {
            switch (providerName)
            {
                case Provider.MySql:
                    this.dbConnType = DatabaseType.MySqlConn;
                    this.connectionString = ConfigurationManager.ConnectionStrings[dbConnStrngKeyNm].ConnectionString;
                    break;
            }

            return (this.dbConnType != null && !string.IsNullOrWhiteSpace(this.connectionString));
        }

        /* Description: It builds ainstance of the [IDatabaseConnector] base on the connection type (MySql, SQl Server, Postgress SQL, etc...)
         * 
         * access:      It uses the private fields [this.dbConnType, this.connectionString]
         * 
         * @return      Returns a newly created instace of [IDatabaseConnector].
         */

        private IDbConnector BuildConnector()
        {
            return DbInstance.CreateDatabaseConnector(this.dbConnType, this.connectionString);
        }
    }
}
