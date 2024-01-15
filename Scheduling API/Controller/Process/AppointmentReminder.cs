using Scheduling_API.Controller.State;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_API.Controller.Process
{
    internal static class AppointmentReminder
    {
        const int preAppointmentAlertMinutes = 15;

        internal static void AlertUserMin(AppState appState)
        {
            DataTable appointmentTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Appointment]!;

            foreach (DataRow row in appointmentTable.Rows)
            {
                TimeSpan timespan = ((DateTime) row[AppointmentColumnName.Start]).Subtract(DateTime.Now);

                if (timespan.Minutes >= 0 && timespan.Minutes <= 15)
                {
                    Console.WriteLine($"Alert {timespan.Minutes} left.");
                }
            }
        }            
    }
}
