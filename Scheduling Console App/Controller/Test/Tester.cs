using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Console_App.Controller.State;
using Scheduling_Library.Model.Data;
using Scheduling_Library.Model.Structure;
using static Scheduling_Library.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_Console_App.Controller.Test
{
    internal static class Tester
    {

        internal static void AuthenticationTest(in AppState appState)
        {
            string[] userNames = new string[] { "test", "raul", "test" };
            string[] passwords = new string[] {"pasword", "test", "test" };
            int testSize = userNames.Length;

            for (var index = 0; index < testSize; ++index)
            {
                appState.AppData.UserRecord.UserName = userNames[index];
                appState.AppData.UserRecord.Password = passwords[index];

                AppController.AuthenticateLogIn(appState);
            }
        }

        internal static void MappingDatabaseTest(in AppState appState)
        {
            AppController.MapDataBaseToDataSet(appState);
        }

        internal static void UpdateDatabaseTest(in AppState appState) 
        {
            string columnName = "customerName";
            DataTable table = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Customer];
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
