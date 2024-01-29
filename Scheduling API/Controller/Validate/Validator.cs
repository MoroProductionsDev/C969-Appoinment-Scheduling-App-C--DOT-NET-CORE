using Scheduling_API.Controller.State;
using System.Collections.Immutable;
using System.Data;
using static Scheduling_Logic.Model.Data.ClientScheduleData;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_API.Controller.Validate
{
    // Validates specific application needed action based on the user's input.
    public static class Validator
    {
        internal static bool CheckCredentials(AppState appState)
        {
            DataTable userTable = appState.DbDataSet.DataSet.Tables[TableName.User]!;
            string userName = appState.AppData.UserRecord.UserName;
            string password = appState.AppData.UserRecord.Password;

            IEnumerable<DataRow> userQuery =
            from userRow in userTable.AsEnumerable()
            select userRow;

            IEnumerable<DataRow> userDataRow =
            userQuery.Where((userRow) =>
            {
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

        public static bool ValidateOutsideBusinessHours(AppointmentRecord appointmentRecord)
        {
            DateTime newAppointmentStartDateTime = appointmentRecord.Start;
            DateTime newAppointmentEndDateTime = appointmentRecord.End;

            if (newAppointmentStartDateTime.Hour < AppData.BusinessOpeningHour || newAppointmentStartDateTime.Hour >= AppData.BusinessClosingHour
                && newAppointmentEndDateTime.Hour < AppData.BusinessOpeningHour || newAppointmentEndDateTime.Hour >= AppData.BusinessClosingHour)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ValidateOutsideBusinessHours(DateTime newAppointmentDateTime)
        {

            if (newAppointmentDateTime.Hour < AppData.BusinessOpeningHour || newAppointmentDateTime.Hour >= AppData.BusinessClosingHour)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidateOverlappingAppointment(AppState appState, AppointmentRecord appointmentRecord)
        {
            DateTime newAppointmentStartDateTime = appointmentRecord.Start;
            DateTime newAppointmentEndDateTime = appointmentRecord.End;

            DataTable appointmentTable = appState.DbDataSet.DataSet.Tables[TableName.Appointment]!;

            foreach (DataRow row in appointmentTable.Rows)
            {
                if (newAppointmentStartDateTime >= ((DateTime)row[AppointmentColumnName.Start]).ToLocalTime() &&
                    newAppointmentStartDateTime <= ((DateTime)row[AppointmentColumnName.End]).ToLocalTime() ||
                    newAppointmentEndDateTime >= ((DateTime)row[AppointmentColumnName.Start]).ToLocalTime() &&
                    newAppointmentEndDateTime <= ((DateTime)row[AppointmentColumnName.End]).ToLocalTime())
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ValidateOverlappingAppointment(AppState appState, DateTime newAppointmentDateTime, out DateTime? overlappedDateTime, out int overLappedId)
        {
            overlappedDateTime = null;
            DataTable appointmentTable = appState.DbDataSet.DataSet.Tables[TableName.Appointment]!;
            overLappedId = -1;

            foreach (DataRow row in appointmentTable.Rows)
            {
                if (newAppointmentDateTime.Date.CompareTo(((DateTime)row[AppointmentColumnName.Start]).ToLocalTime().Date) == 0 &&
                    newAppointmentDateTime.Hour.CompareTo(((DateTime)row[AppointmentColumnName.Start]).ToLocalTime().Hour) == 0)
                {
                    overlappedDateTime = ((DateTime)row[AppointmentColumnName.Start]).ToLocalTime();
                    overLappedId = (int)row[AppointmentColumnName.AppointmentId];

                    return true;
                }
                else if (newAppointmentDateTime.Date.CompareTo(((DateTime)row[AppointmentColumnName.End]).ToLocalTime().Date) == 0 &&
                    newAppointmentDateTime.Hour.CompareTo(((DateTime)row[AppointmentColumnName.End]).ToLocalTime().Hour) == 0)
                {
                    overlappedDateTime = ((DateTime)row[AppointmentColumnName.End]).ToLocalTime();
                    overLappedId = (int)row[AppointmentColumnName.AppointmentId];

                    return true;
                }
            }

            return false;
        }
    }
}
