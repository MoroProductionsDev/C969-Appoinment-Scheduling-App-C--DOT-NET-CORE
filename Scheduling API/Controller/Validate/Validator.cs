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
using static Scheduling_Logic.Model.Data.ClientScheduleRecord;

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
                appState.AppData.AddUser(userDataRow.ToImmutableArray<DataRow>()); // mutation of the UserRecord

                return true;
            }  
            else
            {
                return false;
            }
        }

        internal static bool CheckTimeIsOnBusinnesHours(AppointmentRecord appointmentRecord)
        {
            DateTime newAppointmentStartDateTime = appointmentRecord.Start;
            DateTime newAppointmentEndDateTime = appointmentRecord.End;

            if (newAppointmentStartDateTime.Hour < AppData.BusinessOpeningHour || newAppointmentStartDateTime.Hour >= AppData.BusinessClosingHour
                && newAppointmentEndDateTime.Hour < AppData.BusinessOpeningHour || newAppointmentEndDateTime.Hour >= AppData.BusinessClosingHour)
            {
                return false;
            } else
            {
                return true;
            }
        }

        internal static bool CheckForOverlappingAppointment(AppState appState, AppointmentRecord appointmentRecord)
        {
            DateTime newAppointmentStartDateTime = appointmentRecord.Start;
            DateTime newAppointmentEndDateTime = appointmentRecord.End;

            DataTable appointmentTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Appointment]!;

            foreach (DataRow row in appointmentTable.Rows)
            {
                if (newAppointmentStartDateTime >= (DateTime) row[AppointmentColumnName.Start] &&
                    newAppointmentStartDateTime <= (DateTime) row[AppointmentColumnName.End] ||
                    newAppointmentEndDateTime >= (DateTime)row[AppointmentColumnName.Start] &&
                    newAppointmentEndDateTime <= (DateTime)row[AppointmentColumnName.End])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
