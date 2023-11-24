using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Data;
using Scheduling_Library.Model.Structure;
using Scheduling_Library.Model.Data;
using Scheduling_Library.Model.Database;
using Scheduling_Library.Model.Config;
using Scheduling_Console_App.Controller.Factory;


namespace Scheduling_Console_App.Controller.State
{
    internal sealed class AppState
    {
        public DbSchema DbSchema { get; private set; }
        public AppData AppData { get; private set; }
        public DbConnector DbConnector { get; private set; }
        public DbDataSet DbDataSet { get; private set; }

        internal AppState(IDbConfig dbConfig, string dbName)
        {
            this.DbSchema = AppFactory.BuildDbSchema(dbName);
            this.AppData = AppFactory.BuildAppData(dbName);
            this.DbConnector = AppFactory.BuildDatabaseConnector(dbConfig);
            this.DbDataSet = AppFactory.BuildDatabaseDataSet(this);
        }
    }
}
