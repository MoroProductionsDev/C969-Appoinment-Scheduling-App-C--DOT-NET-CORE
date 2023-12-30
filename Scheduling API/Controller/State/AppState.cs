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
using Org.BouncyCastle.Asn1.Cms;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Scheduling_API.Controller.State
{
    public sealed class AppState
    {
        public bool Authenticated { get; private set; }
        public AppData AppData { get; private set; }
        internal DbSchema DbSchema { get; private set; }
        internal DbConnector DbConnector { get; private set; }
        internal DbDataSet DbDataSet { get; private set; }
        internal Exception? appException { get; private set; }

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
        }

        internal void StoredDbException(object sender, EventArgs e)
        {
            if (appException == null)
            {
                this.appException = sender as Exception;
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
