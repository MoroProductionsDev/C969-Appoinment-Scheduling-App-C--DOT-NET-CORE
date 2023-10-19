using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Scheduling_Library
{

    public static class DatabaseType
    {
        public static Type MySqlConn = typeof(MySqlConnection);
    }

    public static class Provider
    {
        public const string MySql = "MySql.Data.MySqlClient";
    }

    internal static class SqlQueryKeyword
    {
        public const string Select = "SELECT";
        public const string Update = "UPDATE";
        public const string Delete = "DELETE";
    }

    // https://learn.microsoft.com/en-us/sql/relational-databases/clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data?view=sql-server-ver16&viewFallbackFrom=sql-server-2014&redirectedfrom=MSDN
    internal static class SqlTypeName
    {
        public const string Int = "INT";
        public const string String = "VARCHAR";
        public const string TinyInt = "TINYINT";
        public const string DateTime = "DATETIME";
        public const string TimeStamp = "TIMESTAMP";
    }
}
