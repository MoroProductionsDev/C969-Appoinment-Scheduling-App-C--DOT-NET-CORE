using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Data;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_API.Controller.Validate
{
    internal static class Validator
    {
        internal static bool CheckCredentials(AppState appState)
        {
            DataTable userTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.User]!;
            string userName = appState.AppData.UserRecord.UserName;
            string password = appState.AppData.UserRecord.Password;

            IEnumerable<DataRow> userQuery =
            from userRow in userTable.AsEnumerable()
            select userRow;

            IEnumerable<DataRow> userDataRow =
            userQuery.Where((userRow) => {
                return
                userRow.Field<string>(UserColumnName.UserName) == userName &&
                userRow.Field<string>(UserColumnName.Password) == password;
            });

            if (userDataRow.ToList().Count == 1)
            {
                // fetch the user's data to the application
                appState.AppData.AddUser(userDataRow.ToImmutableArray<DataRow>());
                return true;
            }  
            else
            {
                return false;
            }
        }

        internal static bool CheckTimeIsOnBusinnesHours(AppState appState)
        {
            return false;
        }
    }
}
