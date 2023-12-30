using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Scheduling_API.Controller;
using Scheduling_API.Controller.Factory;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Config;

namespace Scheduling_UI_App.UI_State
{
    internal sealed class UIState
    {
        //internal bool excpTriggered;
        internal readonly AppState appState;
        //internal Exception? appException;

        internal UIState(string providerName, string dbName)
        {
            IDbConfig? dbConfig = AppFactory.BuildDatabaseConfiguration(providerName);
            this.appState = AppFactory.BuildAppState(dbConfig, dbName);

            DataMapping();
        }

        private void DataMapping()
        {
            AppController.MapDataBaseToDataSet(this.appState);
        }
    }
}
