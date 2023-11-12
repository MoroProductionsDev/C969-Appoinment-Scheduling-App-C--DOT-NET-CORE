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

namespace Scheduling_Console_App.Controller.State
{
    internal sealed class AppState
    {
        public DbSchema DbSchema { get; private set; }
        public IDbConnector DbConnector { get; set; }
        public DbDataSet DbDataSet { get; set; }

        internal AppState()
        {
            this.DbSchema = new DbStructure.ClientDatabaseSchema();
        }
    }
}
