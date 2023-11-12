using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Library.Model.Structure
{
    public static class DbStructure
    {
        public static class ClientSchedule
        {
            public const int User = 0;
            public const int Appointment = 1;
            public const int Customer = 2;
            public const int Address = 3;
            public const int City = 4;
            public const int Country = 5;
        }
        public class ClientDatabaseSchema : DbSchema
        {
            public override String DbName { get => _dbName; }
            public override Dictionary<int, string> TableNames { get => _tableNames; }
            public override Dictionary<int, string[]> PrimaryKeys { get => _primaryKeys; }
            public override Dictionary<int, string[]> ForeignKeys { get => _foreignKeys; }
            public override Dictionary<int, string[]> FKTables { get => _fkTablesContraints; }

            private const string _dbName = "client_schedule";

            private readonly Dictionary<int, string> _tableNames = new Dictionary<int, string>()
            {
                {ClientSchedule.User, "user" },
                {ClientSchedule.Appointment, "appointment" },
                {ClientSchedule.Customer, "customer" },
                {ClientSchedule.Address, "address" },
                {ClientSchedule.City, "city" },
                {ClientSchedule.Country, "country" }
            };

            public static readonly Dictionary<int, string[]> _primaryKeys = new Dictionary<int, string[]>()
            {
                {ClientSchedule.User, new[]{"userId"} },
                {ClientSchedule.Appointment, new[]{"appointmentId"} },
                {ClientSchedule.Customer, new[] {"customerId"} },
                {ClientSchedule.Address, new[] {"addressId"} },
                {ClientSchedule.City, new[] { "cityId"} },
                {ClientSchedule.Country, new[] { "countryId"}}
            };

            public static readonly Dictionary<int, string[]> _foreignKeys = new Dictionary<int, string[]>()
            {
                {ClientSchedule.User, new[]{String.Empty} },
                {ClientSchedule.Appointment, new []{"customerId", "userId" } },
                {ClientSchedule.Customer, new[] {"addressId" } },
                {ClientSchedule.Address, new[] {"cityId" } },
                {ClientSchedule.City, new[] {"countryId" } },
                {ClientSchedule.Country, new[] {String.Empty } }
            };

            public static readonly Dictionary<int, string[]> _fkTablesContraints = new Dictionary<int, string[]>()
            {
                {ClientSchedule.User, new[]{String.Empty} },
                {ClientSchedule.Appointment, new []{ "customer", "user" } },
                {ClientSchedule.Customer, new[] { "address" } },
                {ClientSchedule.Address, new[] { "city" } },
                {ClientSchedule.City, new[] { "country" } },
                {ClientSchedule.Country, new[] {String.Empty } }
            };
        }
    }
}
