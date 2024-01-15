using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Data;
using Scheduling_Logic.Model.Structure;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Database;
using Scheduling_Logic.Model.Config;
using Scheduling_API.Controller.Factory;
using Scheduling_API.Controller.Validate;
using Scheduling_API.Controller.Process;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Scheduling_Logic.Model.NetWork;

namespace Scheduling_API.Controller.State
{
    public sealed class AppState
    {
        private readonly AuthenticationLogger authLogger;
        public bool Authenticated { get; private set; }
        public string Location { get; private set; } = String.Empty;
        public string TimeZone { get; private set; } = String.Empty;
        public AppData AppData { get; private set; }
        public DbSchema DbSchema { get; private set; }
        public DbConnector DbConnector { get; private set; }
        public DbDataSet DbDataSet { get; private set; }
        internal Exception? AppException { get; private set; }
        public Report Report { get; private set; }

        internal AppState(IDbConfig? dbConfig, string dbName)
        {
            ValidateForNullParamater(dbConfig, nameof(dbConfig));
            ValidateForNullParamater(dbName, nameof(dbName));

            this.AppData = AppFactory.BuildAppData(dbName)!;
            ValidateForNullClassVariable(this.AppData, nameof(this.AppData));

            this.DbSchema = AppFactory.BuildDbSchema(dbName)!;
            ValidateForNullClassVariable(this.DbSchema, nameof(this.DbSchema));

            this.DbConnector = AppFactory.BuildDatabaseConnector(dbConfig!)!;
            ValidateForNullClassVariable(this.DbConnector, nameof(this.DbConnector));

            this.DbDataSet = AppFactory.BuildDatabaseDataSet(this)!;
            ValidateForNullClassVariable(this.DbDataSet, nameof(this.DbDataSet));

            authLogger = new AuthenticationLogger();
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
        internal void Authenticate()
        {
            Authenticated = Validator.CheckCredentials(this);

            if (Authenticated)
            {
                authLogger.WriteLog(this);
                AppointmentReminder.AlertUserMin(this);
            }
        }

        internal void ValidateAppointment()
        {
            Validator.CheckTimeIsOnBusinnesHours(this.AppData.AppointmentRecord);
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

        private void ValidateForNullParamater(object? param, string paramName, [CallerMemberName] string callerName = "")
        {
            if (param is null)
            {
                throw new AppStateNullException("<Scheduling_API.Controller.State>(AppState)",
                new ArgumentNullException(paramName,
                    $"[{callerName}][{paramName}] cannot be null."));
            }
        }

        private void ValidateForNullClassVariable(object? variable, string variableName, [CallerMemberName] string callerName = "")
        {
            if (variable is null)
            {
                throw new AppStateNullException("<Scheduling_API.Controller.State>(AppState)\n" +
                    $"[{callerName}][{variableName}] cannot be null.");
            }
        }
    }
}
