using Scheduling_Console_App.Controller.State;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Console_App.Controller.Validate;
using static Scheduling_Library.Model.Structure.ClientScheduleDbSchema;
using Scheduling_Library.Model.Structure;

namespace Scheduling_Console_App.Controller
{
    internal static class AppController
    {
        internal static void AuthenticateLogIn(in AppState appState)
        {
            Validator.CheckCredentials(in appState);
        }

        internal static void MapDataBaseToDataSet(in AppState appState)
        {
            appState.DbDataSet.Mapping();
        }

        static void AddCustomerRecord()
        {

        }

        static void UpdateCustomerRecord(in AppState appState)
        {

        }
        static void DeleteCustomerRecord() { 
        
        }
    }
}
