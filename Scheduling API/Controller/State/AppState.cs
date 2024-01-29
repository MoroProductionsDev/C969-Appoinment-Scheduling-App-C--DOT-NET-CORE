using Scheduling_API.Controller.Factory;
using Scheduling_API.Controller.Process;
using Scheduling_API.Controller.Validate;
using Scheduling_Logic.Model.Config;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Database;
using Scheduling_Logic.Model.NetWork;
using Scheduling_Logic.Model.Structure;
using System.Runtime.CompilerServices;

namespace Scheduling_API.Controller.State
{
    // Store the overall state of specific structure that will be required all around the
    // application's lifetime.
    public sealed class AppState
    {
        private AuthenticationLogger? authLogger;
        public bool Authenticated { get; private set; }
        public string Location { get; private set; } = String.Empty;
        public string TimeZone { get; private set; } = String.Empty;
        public AppData AppData { get; private set; }
        public AppDataView AppDataView { get; private set; }
        public DbSchema DbSchema { get; private set; }
        public DbConnector DbConnector { get; private set; }
        public DbDataSet DbDataSet { get; private set; }
        public Exception? AppException { get; private set; }
        public Report Report { get; private set; }

        public List<int> UpcomingAppointmentIds = new();
        public List<int> UpcomingAppointmentRemainingMinutes = new();

        public int SelectedId { get; private set; } = -1;

        internal AppState(IDbConfig? dbConfig, string dbName)
        {
            ValidateForNullParamater(dbConfig, nameof(dbConfig));
            ValidateForNullParamater(dbName, nameof(dbName));

            this.DbSchema = AppFactory.BuildDbSchema(dbName)!;
            ValidateForNullClassVariable(this.DbSchema, nameof(this.DbSchema));

            this.DbConnector = AppFactory.BuildDatabaseConnector(dbConfig!)!;
            ValidateForNullClassVariable(this.DbConnector, nameof(this.DbConnector));

            this.DbDataSet = AppFactory.BuildDatabaseDataSet(this)!;
            ValidateForNullClassVariable(this.DbDataSet, nameof(this.DbDataSet));

            this.AppData = AppFactory.BuildAppData(dbName)!;
            ValidateForNullClassVariable(this.AppData, nameof(this.AppData));

            this.AppDataView = AppFactory.BuildAppDataView(this, dbName)!;
            ValidateForNullClassVariable(this.AppDataView, nameof(this.AppDataView));

            Report = new Report(this.DbDataSet.DataSet);
        }

        public static void StaticValidateAppStateForNull(AppState? controlAppState, [CallerMemberName] string callerName = "")
        {
            if (controlAppState is null)
            {
                throw new AppStateNullException("<Scheduling_API.Controller.State>(AppState)\n" +
                    $"[{callerName}][{nameof(controlAppState)}] cannot be null.");
            }
        }

        public void ClearAppException()
        {
            this.AppException = null;
        }

        public void NewUnauthorizedAccessException(string message)
        {
            this.AppException = new UnauthorizedAccessException(message);
        }

        internal void Authenticate()
        {
            this.Authenticated = Validator.CheckCredentials(this);

            if (Authenticated)
            {
                this.authLogger = new AuthenticationLogger();
                this.authLogger.WriteLog(this);

                AppointmentReminder.AlertUserMin(this);
            }
        }

        internal void UnAuthenticate()
        {
            this.Authenticated = false;
        }

        internal void StoredFetchedId(int id)
        {
            this.SelectedId = id;
        }

        internal void ValidateAppointment()
        {
            Validator.ValidateOutsideBusinessHours(this.AppData.AppointmentRecord);
            // Validator.CheckForOverlappingAppointment(this, this.AppData.AppointmentRecord);
        }

        internal void GetGeoLocationData()
        {
            Location = NetworkDataProvider.GetUserLocation();
            TimeZone = NetworkDataProvider.GetUserTimeZone();
        }

        internal void StoredDbException(object sender, EventArgs e)
        {
            if (AppException == null)
            {
                this.AppException = sender as Exception;
            }
        }

        private static void ValidateForNullParamater(object? param, string paramName, [CallerMemberName] string callerName = "")
        {
            if (param is null)
            {
                throw new AppStateNullException("<Scheduling_API.Controller.State>(AppState)",
                new ArgumentNullException(paramName,
                    $"[{callerName}][{paramName}] cannot be null."));
            }
        }

        private static void ValidateForNullClassVariable(object? variable, string variableName, [CallerMemberName] string callerName = "")
        {
            if (variable is null)
            {
                throw new AppStateNullException("<Scheduling_API.Controller.State>(AppState)\n" +
                    $"[{callerName}][{variableName}] cannot be null.");
            }
        }
    }
}
