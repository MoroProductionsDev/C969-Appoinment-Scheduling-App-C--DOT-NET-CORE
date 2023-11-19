using Scheduling_Console_App.Controller.State;
using Scheduling_Library.Model.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Scheduling_Library.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_Console_App.Controller.Validate
{
    internal static class Validator
    {
        public static bool CheckCredentials(in AppState appState)
        {
            DataTable userTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.User];
            String userName = appState.AppData.UserRecord.UserName;
            String password = appState.AppData.UserRecord.Password;

            IEnumerable<DataRow> userQuery =
            from userRow in userTable.AsEnumerable()
            select userRow;

            IEnumerable<DataRow> userNameAndPw =
            userQuery.Where((userRow) => {
                return
                userRow.Field<string>(UserColumnName.UserName) == userName &&
                userRow.Field<string>(UserColumnName.Password) == password;
            });
/*
            Console.WriteLine(userNameAndPw.ToList().Count);

            foreach (DataRow user in userNameAndPw)
            {
                Console.WriteLine(user.Field<string>(UserColumnName.UserName));
                Console.WriteLine(user.Field<string>(UserColumnName.Password));
            }*/

            return userNameAndPw.ToList().Count == 1;
        }
    }
}
