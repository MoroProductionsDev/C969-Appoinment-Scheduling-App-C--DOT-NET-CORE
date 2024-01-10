using Scheduling_API.Controller.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_API.Controller.Process
{
    internal static class TimeConverter
    {
        internal static DateTime AdjustTimeBasedOnUserTimeZone(DateTime dateTime, AppState appState) {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(appState.TimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, userTimeZone);
        }

        internal static DateTime AdjustTimeBasedOnUTC(DateTime dateTime, AppState appState)
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(appState.TimeZone);
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, userTimeZone);
        }
    }
}
