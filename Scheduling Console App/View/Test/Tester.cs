using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_API.Controller;
using Scheduling_API.Controller.State;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_Console_App.Test
{
    internal static class Tester
    {

        internal static void AuthenticationTest(AppState appState)
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

        internal static void MappingDatabaseTest(AppState appState)
        {
            AppController.MapDataBaseToDataSet(appState);
        }

        internal static void InsertDatabaseTest(AppState appState)
        {
            AppController.AddCustomerRecord(appState);
        }

        internal static void UpdateDatabaseTest(AppState appState) 
        {
            /*            string columnName = "customerName";
                        DataTable table = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Customer];
                        DataRow resultRow = table.Select($"{columnName} = 'Raul Rivero'").FirstOrDefault();
                        resultRow[columnName] = "Pedro Navaja";

                        ConsoleOutput.ShowTable(table, resultRow);*/

            AppController.UpdateCustomerRecord(appState);
        }

        internal static void DeleteDatabaseTest(AppState appState)
        {
            AppController.DeleteCustomerRecord(appState);
        }


/*        internal static void OutputAllDatasetTablesTest(in AppState appState)
        {
            foreach (DataTable dtTable in appState.DbDataSet.DataSet.Tables)
            {
                ConsoleOutput.ShowTable(dtTable);
            } 
        }*/

        internal static void OpeningDatabaseConnectionTest(AppState appState)
        {
            appState.DbConnector?.OpenConnection();

            // Output
            Console.WriteLine($" Connection State: {appState.DbConnector?.ConnState}");
            // out-end
        }

        internal static void CloseAndDisposeDatabaseObject(AppState appState)
        {
            appState.DbConnector?.Dispose();

            // Output
            Console.WriteLine($"Disposed object: {appState.DbConnector}");
            // out-end
        }
    }
}
