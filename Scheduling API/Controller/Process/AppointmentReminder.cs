using Scheduling_API.Controller.State;
using System.Data;
using Scheduling_Logic.Model.Structure;

namespace Scheduling_API.Controller.Process
{
    // It searches in the AppState database dataset if there's any upcoming appointment's in the upcoming specific
    // time listed in the class.
    internal static class AppointmentReminder
    {
        const int preAppointmentAlertMinutes = 15;

        internal static void AlertUserMin(AppState appState)
        {
            DataTable appointmentTable = appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.Appointment]!;

            foreach (DataRow row in appointmentTable.Rows)
            {
                int appointmentId = (int) row[ClientScheduleDbSchema.AppointmentColumnName.AppointmentId];
                TimeSpan localRemainingTimeSpanDifference = ((DateTime) row[ClientScheduleDbSchema.AppointmentColumnName.Start]).ToLocalTime().Subtract(DateTime.Now);

                if (localRemainingTimeSpanDifference.Hours == 0 && localRemainingTimeSpanDifference.Minutes >= 0 && localRemainingTimeSpanDifference.Minutes <= preAppointmentAlertMinutes)
                {
                    appState.UpcomingAppointmentIds.Add(appointmentId);
                    appState.UpcomingAppointmentRemainingMinutes.Add(localRemainingTimeSpanDifference.Minutes + 1); // to compensate for the remaining seconds
                }
            }
        }
    }
}
