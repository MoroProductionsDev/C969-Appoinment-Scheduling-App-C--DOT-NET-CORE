using Scheduling_API.Controller;
using Scheduling_API.Controller.Factory;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Config;

namespace Scheduling_UI_App.UI_Process
{
    // It faciliates the quick and global access of the AppState to any UI control or form.
    public static class UIState
    {
        public static AppState? State;

        public static void Configure(string providerName, string dbName)
        {
            IDbConfig? dbConfig = AppFactory.BuildDatabaseConfiguration(providerName);
            State = AppFactory.BuildAppState(dbConfig, dbName);

            DataMapping();
        }

        public static void BindAppStateBindingSource(BindingSource bindingSource)
        {
            if (State is null)
            {
                throw new NullReferenceException("State has not been configure");
            }

            bindingSource.DataSource = State;
        }

        private static void DataMapping()
        {
            AppController.MapDataBaseToDataSet(State);
        }
    }
}
