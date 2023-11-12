using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Console_App.Controller.State;
using static Scheduling_Library.Model.Structure.DbStructure;

namespace Scheduling_Console_App.Controller.Test
{
    internal static class Tester
    {
        internal static void MappingDatabaseTest(in AppState appState)
        {
            appState.DbDataSet.Populate();
        }

        internal static void UpdateDatabaseTest(in AppState appState) 
        {
            string tableName = appState.DbSchema.TableNames[ClientSchedule.Customer];
            string columnName = "customerName";
            DataTable table = appState.DbDataSet.DataSet.Tables[tableName];
            DataRow resultRow = table.Select($"{columnName} = 'Raul Rivero'").FirstOrDefault();
            resultRow[columnName] = "Pedro Navaja";

            ConsoleOutput.ShowTable(table, resultRow);
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
