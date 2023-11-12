using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Console_App.Controller.State;

namespace Scheduling_Console_App.Controller.Test
{
    internal static class Tester
    {
        internal static void MappingDatabaseTest(in AppState appState)
        {
            appState.DbDataSet.Populate();
        }

/*        internal static void writingDatabaseTest(in State appState)
        {
            appState.Connector?.CreateDbCommand(commandText);
        }*/


        internal static void OpeningDatabaseConnectionTest(in AppState appState)
        {
            appState.DbConnector?.OpenConnection();

            // Output
            Console.WriteLine($" Connection State: {appState.DbConnector?.ConnState}");
            // out-end
        }

        internal static void CloseAndDisposeDatabaseObject(in AppState appState)
        {
            appState.DbConnector?.Dispose();

            // Output
            Console.WriteLine($"Disposed object: {appState.DbConnector}");
            // out-end
        }
    }
}
