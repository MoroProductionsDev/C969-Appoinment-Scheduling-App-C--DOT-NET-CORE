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
using System.ComponentModel;
using Scheduling_Logic.Model.Config;
using Scheduling_API.Controller;
using Scheduling_API.Controller.State;
using Scheduling_API.Controller.Factory;
using Scheduling_Console_App.Test;



namespace Scheduling_Console_App
{
    internal sealed class Application
    {
        private readonly AppState appState;
        private readonly IDbConfig dbConfig;

        // The "connectionString" element's name int the "App.config" file that will be use to connect
        // to the correct database data.

        public Application(string providerName, string dbName)
        {
            this.dbConfig = AppFactory.BuildDatabaseConfiguration(providerName);
            this.appState = AppFactory.BuildAppState(this.dbConfig, dbName);
        }

        /*
             * Description: Runs the application.
             * 
             * @param:      [String]    providerName        identify which providername will be use for the database connection
             *                                              stored in the configuration setting.
        */
        public void Run()
        {            
            Tester.OpeningDatabaseConnectionTest(this.appState);

            Tester.MappingDatabaseTest(this.appState);

            ConsoleOutput.ShowTable(appState.DbDataSet.DataSet.Tables["customer"]);

            //Tester.InsertDatabaseTest(this.appState);

            Tester.UpdateDatabaseTest(this.appState);

            ConsoleOutput.ShowTable(appState.DbDataSet.DataSet.Tables["customer"]);

/*            Tester.DeleteDatabaseTest(this.appState);

            ConsoleOutput.ShowTable(appState.DbDataSet.DataSet.Tables["customer"]);

            Tester.InsertDatabaseTest(this.appState);

            ConsoleOutput.ShowTable(appState.DbDataSet.DataSet.Tables["customer"]);*/

            //ConsoleOutput.ShowTable(this.appState.DbDataSet.DataSet.Tables);

            // Tester.AuthenticationTest(this.appState);

            //Tester.UpdateDatabaseTest(this.appState);

            Tester.CloseAndDisposeDatabaseObject(this.appState);
        }
    }
}