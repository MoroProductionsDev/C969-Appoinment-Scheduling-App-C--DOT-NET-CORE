using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Scheduling_Library.Model.Structure
{
    public static class DbProvider
    {
        public const string MySqlClient = "MySql.Data.MySqlClient";
    }

    public static class DbName
    {
        public const string ClientScheduleDbName = "client_schedule";
    }
}
