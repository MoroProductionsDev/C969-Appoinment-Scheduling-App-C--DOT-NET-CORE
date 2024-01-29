namespace Scheduling_Logic.Model.Structure
{
    // Client's schedule database schema (table, columns, primary keys, foreign keys per table)
    public sealed class ClientScheduleDbSchema : DbSchema
    {
        public const string _dbName = "client_schedule";

        private static readonly Dictionary<int, string> _tableNamesIndented = new()
        {
            {0, TableName.User},
            {1, TableName.Appointment},
            {2, TableName.Customer},
            {3, TableName.Address},
            {4, TableName.City},
            {5, TableName.Country}
        };

        private static readonly Dictionary<string, string[]> _primaryKeys = new()
        {
            {TableName.User, new[]{UserColumnName.UserId} },
            {TableName.Appointment, new[]{AppointmentColumnName.AppointmentId} },
            {TableName.Customer, new[] {CustomerColumnName.CustomerId} },
            {TableName.Address, new[] {AddressColumnName.AddressId} },
            {TableName.City, new[] {CityColumnName.CityId} },
            {TableName.Country, new[] {CountryColumnName.CountryId}}
        };

        private static readonly Dictionary<string, string[]> _foreignKeys = new()
        {
            {TableName.User, new[]{ string.Empty} },
            {TableName.Appointment, new []{ AppointmentColumnName.CustomerId, AppointmentColumnName .UserId} },
            {TableName.Customer, new[] {CustomerColumnName.AddressId} },
            {TableName.Address, new[] {AddressColumnName.CityId} },
            {TableName.City, new[] {CityColumnName.CountryId} },
            {TableName.Country, new[] { string.Empty } }
        };

        private static readonly Dictionary<string, string[]> _fkTablesContraints = new()
        {
            {TableName.User, new[]{ string.Empty} },
            {TableName.Appointment, new []{TableName.Customer, TableName.User} },
            {TableName.Customer, new[] {TableName.Address} },
            {TableName.Address, new[] {TableName.City} },
            {TableName.City, new[] {TableName.Country} },
            {TableName.Country, new[] {string.Empty } }
        };

        public override string DbName { get => _dbName; }
        public override Dictionary<int, string> TableNamesIndented { get => _tableNamesIndented; }
        public override Dictionary<string, string[]> PrimaryKeysNames { get => _primaryKeys; }
        public override Dictionary<string, string[]> ForeignKeysNames { get => _foreignKeys; }
        public override Dictionary<string, string[]> FKTablesNames { get => _fkTablesContraints; }

        public static class TableName
        {
            public const string User = "user";
            public const string Appointment = "appointment";
            public const string Customer = "customer";
            public const string Address = "address";
            public const string City = "city";
            public const string Country = "country";
        }

        public static class UserColumnName
        {
            public const string UserId = "userId";
            public const string UserName = "userName";
            public const string Password = "password";
            public const string Active = "active";
        }

        public static class AppointmentColumnName
        {
            public const string AppointmentId = "appointmentId";
            public const string CustomerId = "customerId";
            public const string UserId = "userId";
            public const string Title = "title";
            public const string Description = "description";
            public const string Location = "location";
            public const string Contact = "contact";
            public const string Type = "type";
            public const string Url = "url";
            public const string Start = "start";
            public const string End = "end";
        }

        public static class CustomerColumnName
        {
            public const string CustomerId = "customerId";
            public const string CustomerName = "customerName";
            public const string AddressId = "addressId";
            public const string Active = "active";
        }

        public static class AddressColumnName
        {
            public const string AddressId = "addressId";
            public const string Address = "address";
            public const string Address2 = "address2";
            public const string CityId = "cityId";
            public const string PostalCode = "postalCode";
            public const string Phone = "phone";
        }

        public static class CityColumnName
        {
            public const string CityId = "cityId";
            public const string City = "city";
            public const string CountryId = "countryId";
        }

        public static class CountryColumnName
        {
            public const string CountryId = "countryId";
            public const string Country = "country";
        }

        public static class AllInCommonColumns
        {
            public const string CreateDate = "createDate";
            public const string CreatedBy = "createdBy";
            public const string LastUpdate = "lastUpdate";
            public const string LastUpdateBy = "lastUpdateBy";
        }

        public static string[] GetCountryColumnNames()
        {
            return new[]
            {
                $"{CountryColumnName.CountryId}",
                $"{CountryColumnName.Country}",
                $"{AllInCommonColumns.CreateDate}",
                $"{AllInCommonColumns.CreatedBy}",
                $"{AllInCommonColumns.LastUpdate}",
                $"{AllInCommonColumns.LastUpdateBy}"
            };
        }

        public static string[] GetCityColumnNames()
        {
            return new[]
            {
                $"{CityColumnName.CityId}",
                $"{CityColumnName.City}",
                $"{CityColumnName.CountryId}",
                $"{AllInCommonColumns.CreateDate}",
                $"{AllInCommonColumns.CreatedBy}",
                $"{AllInCommonColumns.LastUpdate}",
                $"{AllInCommonColumns.LastUpdateBy}"
            };
        }

        public static string[] GetAddressColumnNames()
        {
            return new[]
            {
                $"{AddressColumnName.AddressId}",
                $"{AddressColumnName.Address}",
                $"{AddressColumnName.Address2}",
                $"{AddressColumnName.CityId}",
                $"{AddressColumnName.PostalCode}",
                $"{AddressColumnName.Phone}",
                $"{AllInCommonColumns.CreateDate}",
                $"{AllInCommonColumns.CreatedBy}",
                $"{AllInCommonColumns.LastUpdate}",
                $"{AllInCommonColumns.LastUpdateBy}"
            };
        }

        public static string[] GetCustomerColumnNames()
        {
            return new[]
            {
                $"{CustomerColumnName.CustomerId}",
                $"{CustomerColumnName.CustomerName}",
                $"{CustomerColumnName.AddressId}",
                $"{CustomerColumnName.Active}",
                $"{AllInCommonColumns.CreateDate}",
                $"{AllInCommonColumns.CreatedBy}",
                $"{AllInCommonColumns.LastUpdate}",
                $"{AllInCommonColumns.LastUpdateBy}"
            };
        }

        public static string[] GetAppointmentColumnNames()
        {
            return new[]
            {
                $"{AppointmentColumnName.AppointmentId}",
                $"{AppointmentColumnName.CustomerId}",
                $"{AppointmentColumnName.UserId}",
                $"{AppointmentColumnName.Title}",
                $"{AppointmentColumnName.Description}",
                $"{AppointmentColumnName.Location}",
                $"{AppointmentColumnName.Contact}",
                $"{AppointmentColumnName.Type}",
                $"{AppointmentColumnName.Url}",
                $"{AppointmentColumnName.Start}",
                $"{AppointmentColumnName.End}",
                $"{AllInCommonColumns.CreateDate}",
                $"{AllInCommonColumns.CreatedBy}",
                $"{AllInCommonColumns.LastUpdate}",
                $"{AllInCommonColumns.LastUpdateBy}"
            };
        }
    }
}
