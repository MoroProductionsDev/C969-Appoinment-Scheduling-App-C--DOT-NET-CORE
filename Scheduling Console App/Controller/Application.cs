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
using Scheduling_Library.Model.Structure;
using Scheduling_Library.Model.Factory;
using Scheduling_Library.Model.Data;
using Scheduling_Library.Model.Database;
using Scheduling_Console_App.Controller.Config;
using Scheduling_Console_App.Controller.State;
using Scheduling_Console_App.Controller.Factory;
using Scheduling_Console_App.Controller.Test;

namespace Scheduling_Console_App
{
    internal sealed class Application
    {
        private readonly AppState appState;
        private readonly IDbConfiguration dbConfig;

        // The "connectionString" element's name int the "App.config" file that will be use to connect
        // to the correct database data.

        public Application(in String providerName, in String dbName)
        {
            this.dbConfig = AppFactory.BuildDatabaseConfiguration(in providerName);
            this.appState = AppFactory.BuildAppState(in this.dbConfig, dbName);
        }

        /*
             * Description: Runs the application.
             * 
             * @param:      [String]    providerName        identify which providername will be use for the database connection
             *                                              stored in the configuration setting.
        */
        public void Run()
        {            
            Tester.OpeningDatabaseConnectionTest(in this.appState);

            Tester.MappingDatabaseTest(in this.appState);

            ConsoleOutput.ShowTable(appState.DbDataSet.DataSet.Tables["customer"]);

            //Tester.InsertDatabaseTest(in this.appState);

            Tester.UpdateDatabaseTest(in this.appState);

            ConsoleOutput.ShowTable(appState.DbDataSet.DataSet.Tables["customer"]);

/*            Tester.DeleteDatabaseTest(in this.appState);

            ConsoleOutput.ShowTable(appState.DbDataSet.DataSet.Tables["customer"]);

            Tester.InsertDatabaseTest(in this.appState);

            ConsoleOutput.ShowTable(appState.DbDataSet.DataSet.Tables["customer"]);*/

            //ConsoleOutput.ShowTable(this.appState.DbDataSet.DataSet.Tables);

            // Tester.AuthenticationTest(in this.appState);

            //Tester.UpdateDatabaseTest(in this.appState);

            Tester.CloseAndDisposeDatabaseObject(in this.appState);
        }
    }
}
