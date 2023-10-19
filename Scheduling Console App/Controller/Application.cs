using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchedulingLibrary;

namespace Scheduling_Console_App
{
    internal class Application
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
            ConsoleOutput output = new ConsoleOutput();

            if (!IdentifyRequiredFields(providerName))
            {
                return;
            }

            IDatabaseConnector connector = BuildConnector();

            connector?.OpenConnection();

            var commandText = "SELECT * FROM client_schedule.customer;";

            connector?.CreateCommand(commandText).Execute();

            DatabaseDataTable databaseDataTable = BuildDatabaseDataTable(connector?.DbDataReader);

            databaseDataTable?.Create();

            output.ShowTable(databaseDataTable.DbDataTable);
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

        private IDatabaseConnector BuildConnector()
        {
            return DatabaseInstance.CreateDatabaseConnector(this.dbConnType, this.connectionString);
        }


        /* Description: It builds a instance of the [DatabaseDataTable] base on reference data reader provided.
         * 
         * @param       [IDataReader] reader        It carries a reference to database data reader returned from it.
         * 
         * access:      It creates a new DataTable instance and pases it internally.
         * 
         * @return      Returns a newly created instace of [DatabaseDataTable].
         */
        private DatabaseDataTable BuildDatabaseDataTable(IDataReader reader)
        {
            return DataInstance.CreateDbDataTable(reader, DataInstance.createDataTable());
        }
    }
}
