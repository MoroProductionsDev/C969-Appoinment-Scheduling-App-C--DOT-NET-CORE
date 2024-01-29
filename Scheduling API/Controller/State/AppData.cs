using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Structure;
using System.Collections;
using System.Collections.Immutable;
using System.Data;
using System.Reflection;

namespace Scheduling_API.Controller.State
{
    // It holds record entities use to store the user input and later to update to the database.
    // It also creates specific SQL statements based on the user's action and data provided.
    public sealed class AppData
    {
        const char ParameterSymbol = '@';
        const int firstIdIndex = 1;
        public const int BusinessOpeningHour = 8;  // 8 AM
        public const int BusinessClosingHour = 17; // 5 PM
        public const int HoursDifferenceMilitaryToStandard = 12;
        public ClientScheduleData.UserRecord UserRecord { get; private set; }
        public ClientScheduleData.AppointmentRecord AppointmentRecord { get; private set; }
        public ClientScheduleData.CustomerRecord CustomerRecord { get; private set; }
        public ClientScheduleData.AddressRecord AddressRecord { get; private set; }
        public ClientScheduleData.CityRecord CityRecord { get; private set; }
        public ClientScheduleData.CountryRecord CountryRecord { get; private set; }
        public AppData()
        {
            this.UserRecord = new();
            this.AppointmentRecord = new();
            this.CustomerRecord = new();
            this.AddressRecord = new();
            this.CityRecord = new();
            this.CountryRecord = new();
        }

        public void AddUser(ImmutableArray<DataRow> currentUserDataRow)
        {
            this.UserRecord.UserId = (int)currentUserDataRow[0][0];
            this.UserRecord.UserName = (string)currentUserDataRow[0][1];
            // Skip the password, no need to store it.
            this.UserRecord.Active = (sbyte)currentUserDataRow[0][3];
            this.UserRecord.CreateDate = (DateTime)currentUserDataRow[0][4];
            this.UserRecord.CreatedBy = (string)currentUserDataRow[0][5];
            this.UserRecord.LastUpdate = (DateTime)currentUserDataRow[0][6];
            this.UserRecord.LastUpdateBy = (string)currentUserDataRow[0][7];
        }

        internal void AddCustomerValAndDefaultVal(AppState appState)
        {
            AddCountry(appState);
            AddCity(appState);
            AddCity(appState);
            AddAddress(appState);
            AddCustomer(appState);

        }

        internal void AddAppointmentValAndDefaultVal(AppState appState)
        {
            AddAppointment(appState);
        }

        internal static string GetUpdateSQLStatement<T>(UpdateDbMetaData<T> updateDatabaseMetaData)
        {
            return $"UPDATE `{updateDatabaseMetaData.DbName}`.`{updateDatabaseMetaData.TableName}` " +
                        $"SET {updateDatabaseMetaData.ValueColumnName} = {ParameterSymbol}{updateDatabaseMetaData.ValueColumnName} " +
                        $"WHERE {updateDatabaseMetaData.IdColumnName} = {updateDatabaseMetaData.IdValue};";
        }

        internal static string GetDeleteSQLStatement(DeleteDbMetaData deleteDatabaseMetaData)
        {
            return $"DELETE FROM `{deleteDatabaseMetaData.DbName}`.`{deleteDatabaseMetaData.TableName}` " +
              $"WHERE {deleteDatabaseMetaData.IdColumnName} = {deleteDatabaseMetaData.IdValue}";
        }

        internal static string GetInsertCountrySQlStatement()
        {
            string[] parameters = ClientScheduleDbSchema.GetCountryColumnNames();
            string insertStatement = $"INSERT INTO {ClientScheduleDbSchema.TableName.Country} (";


            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ") ";
                }
                else
                {
                    insertStatement += ", ";
                }
            }

            insertStatement += $"VALUES (";

            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{ParameterSymbol}{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ");";
                }
                else
                {
                    insertStatement += ", ";
                }
            }

            return insertStatement;
        }

        internal static string GetInsertCitySQlStatement()
        {
            string[] parameters = ClientScheduleDbSchema.GetCityColumnNames();
            string insertStatement = $"INSERT INTO {ClientScheduleDbSchema.TableName.City} (";


            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ") ";
                }
                else
                {
                    insertStatement += ", ";
                }
            }

            insertStatement += $"VALUES (";

            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{ParameterSymbol}{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ");";
                }
                else
                {
                    insertStatement += ", ";
                }
            }

            return insertStatement;
        }

        internal static string GetInsertAddressSQlStatement()
        {
            string[] parameters = ClientScheduleDbSchema.GetAddressColumnNames();
            string insertStatement = $"INSERT INTO {ClientScheduleDbSchema.TableName.Address} (";


            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ") ";
                }
                else
                {
                    insertStatement += ", ";
                }
            }

            insertStatement += $"VALUES (";

            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{ParameterSymbol}{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ");";
                }
                else
                {
                    insertStatement += ", ";
                }
            }

            return insertStatement;
        }

        internal static string GetInsertCustomerSQlStatement()
        {
            string[] parameters = ClientScheduleDbSchema.GetCustomerColumnNames();
            string insertStatement = $"INSERT INTO {ClientScheduleDbSchema.TableName.Customer} (";


            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ") ";
                }
                else
                {
                    insertStatement += ", ";
                }
            }

            insertStatement += $"VALUES (";

            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{ParameterSymbol}{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ");";
                }
                else
                {
                    insertStatement += ", ";
                }
            }

            return insertStatement;
        }

        internal static string GetInsertAppointmentSQlStatement()
        {
            string[] parameters = ClientScheduleDbSchema.GetAppointmentColumnNames();
            string insertStatement = $"INSERT INTO {ClientScheduleDbSchema.TableName.Appointment} (";


            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ") ";
                }
                else
                {
                    insertStatement += ", ";
                }
            }

            insertStatement += $"VALUES (";

            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{ParameterSymbol}{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ");";
                }
                else
                {
                    insertStatement += ", ";
                }
            }

            return insertStatement;
        }

        internal ArrayList GetRecordValuesAsArrayList<T>()
        {
            ArrayList recordValues = new();

            switch (typeof(T))
            {
                case Type _ when typeof(ClientScheduleData.CountryRecord) == typeof(T):
                    AssignValueToArrayList(this.CountryRecord, recordValues);
                    break;
                case Type _ when typeof(ClientScheduleData.CityRecord) == typeof(T):
                    AssignValueToArrayList(this.CityRecord, recordValues);
                    break;
                case Type _ when typeof(ClientScheduleData.AddressRecord) == typeof(T):
                    AssignValueToArrayList(this.AddressRecord, recordValues);
                    break;
                case Type _ when typeof(ClientScheduleData.CustomerRecord) == typeof(T):
                    AssignValueToArrayList(this.CustomerRecord, recordValues);
                    break;
                case Type _ when typeof(ClientScheduleData.AppointmentRecord) == typeof(T):
                    AssignValueToArrayList(this.AppointmentRecord, recordValues);
                    break;
                default:
                    throw new ArgumentException("<Scheduling_API.Controller.State>(AppState)\nThis is not one of AppData Record Type");
            }

            return recordValues;
        }

        private static void AssignValueToArrayList<T>(T record, ArrayList recordValues)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                recordValues.Add(field.GetValue(record));
            }
        }

        private void AddCountry(AppState appState)
        {
            ValidateForNullAttribute(appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.Country], $"{typeof(DataTable)} \"{ClientScheduleDbSchema.TableName.Country}\"");

            // Simplify
            DataTable countryTable = appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.Country]!;

            // AppState fetching data of AppState > DbDataSet > DataSet > Specific table
            int lastRowIndex = countryTable.Rows.Count > 0 ? countryTable.Rows.Count - 1 : firstIdIndex; // if there is any row get the last one if not it will be set to zero
            int newCountryId = ((int)countryTable.Rows[lastRowIndex][ClientScheduleDbSchema.CountryColumnName.CountryId]) + 1;
            string createdByUserName = (string)countryTable.Rows[0][ClientScheduleDbSchema.AllInCommonColumns.CreatedBy];
            string lastUpdatedByUserName = (string)countryTable.Rows[0][ClientScheduleDbSchema.AllInCommonColumns.LastUpdateBy];

            // Updating the new Record
            this.CountryRecord.CountryId = newCountryId;
            this.CountryRecord.Country = appState.AppData.CountryRecord.Country ?? ""; // user value
            this.CountryRecord.CreateDate = DateTime.Now; // Local time based on Database CURRENT_TIMESTAMP
            this.CountryRecord.CreatedBy = createdByUserName;
            this.CountryRecord.LastUpdate = DateTime.Now; // Local time based on Database CURRENT_TIMESTAMP
            this.CountryRecord.LastUpdateBy = lastUpdatedByUserName;
        }

        private void AddCity(AppState appState)
        {
            ValidateForNullAttribute(appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.City], $"{typeof(DataTable)} \"{ClientScheduleDbSchema.TableName.City}\"");

            // Simplify
            DataTable cityTable = appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.City]!;

            // AppState fetching data of AppState > DbDataSet > DataSet > Specific table
            int lastRowIndex = cityTable.Rows.Count > 0 ? cityTable.Rows.Count - 1 : firstIdIndex; // if there is any row get the last one if not it will be set to zero
            int newCityId = ((int)cityTable.Rows[lastRowIndex][ClientScheduleDbSchema.CityColumnName.CityId]) + 1;
            string createdByUserName = (string)cityTable.Rows[0][ClientScheduleDbSchema.AllInCommonColumns.CreatedBy];
            string lastUpdatedByUserName = (string)cityTable.Rows[0][ClientScheduleDbSchema.AllInCommonColumns.LastUpdateBy];

            // Updating the new Record
            this.CityRecord.CityId = newCityId;
            this.CityRecord.City = appState.AppData.CityRecord.City; // user value
            this.CityRecord.CountryId = this.CountryRecord.CountryId;
            this.CityRecord.CreateDate = DateTime.Now; // Local time based on Database CURRENT_TIMESTAMP
            this.CityRecord.CreatedBy = createdByUserName;
            this.CityRecord.LastUpdate = DateTime.Now; // Local time based on Database CURRENT_TIMESTAMP
            this.CityRecord.LastUpdateBy = lastUpdatedByUserName;
        }

        private void AddAddress(AppState appState)
        {
            ValidateForNullAttribute(appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.Address], $"{typeof(DataTable)} \"{ClientScheduleDbSchema.TableName.Address}\"");

            // Simplify
            DataTable addressTable = appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.Address]!;

            // AppState fetching data of AppState > DbDataSet > DataSet > Specific table
            int lastRowIndex = addressTable.Rows.Count > 0 ? addressTable.Rows.Count - 1 : firstIdIndex; // if there is any row get the last one if not it will be set to zero
            int newAddressId = ((int)addressTable.Rows[lastRowIndex][ClientScheduleDbSchema.AddressColumnName.AddressId]) + 1;
            string createdByUserName = (string)addressTable.Rows[0][ClientScheduleDbSchema.AllInCommonColumns.CreatedBy];
            string lastUpdatedByUserName = (string)addressTable.Rows[0][ClientScheduleDbSchema.AllInCommonColumns.LastUpdateBy];

            // Updating the new Record
            this.AddressRecord.AddressId = newAddressId;
            this.AddressRecord.Address = appState.AppData.AddressRecord.Address ?? String.Empty; // user value
            this.AddressRecord.Address2 = appState.AppData.AddressRecord.Address2 ?? String.Empty; // user value
            this.AddressRecord.CityId = this.CityRecord.CityId;
            this.AddressRecord.PostalCode = appState.AppData.AddressRecord.PostalCode ?? String.Empty; // user value
            this.AddressRecord.Phone = appState.AppData.AddressRecord.Phone ?? "(###) ###-####";  // user value
            this.AddressRecord.CreateDate = DateTime.Now; // Local time based on Database CURRENT_TIMESTAMP
            this.AddressRecord.CreatedBy = createdByUserName;
            this.AddressRecord.LastUpdate = DateTime.Now; // Local time based on Database CURRENT_TIMESTAMP
            this.AddressRecord.LastUpdateBy = lastUpdatedByUserName;
        }

        private void AddCustomer(AppState appState)
        {
            ValidateForNullAttribute(appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.Customer], $"{typeof(DataTable)} \"{ClientScheduleDbSchema.TableName.Customer}\"");

            // Simplify
            DataTable customerTable = appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.Customer]!;

            // AppState fetching data of AppState > DbDataSet > DataSet > Specific table
            int lastRowIndex = customerTable.Rows.Count > 0 ? customerTable.Rows.Count - 1 : firstIdIndex; // if there is any row get the last one if not it will be set to zero
            int newCustomersId = ((int)customerTable.Rows[lastRowIndex][ClientScheduleDbSchema.CustomerColumnName.CustomerId]) + 1;
            string createdByUserName = (string)customerTable.Rows[0][ClientScheduleDbSchema.AllInCommonColumns.CreatedBy];
            string lastUpdatedByUserName = (string)customerTable.Rows[0][ClientScheduleDbSchema.AllInCommonColumns.LastUpdateBy];

            // Updating the new Record
            this.CustomerRecord.CustomerId = newCustomersId;
            this.CustomerRecord.CustomerName = appState.AppData.CustomerRecord.CustomerName ?? String.Empty; // user value
            this.CustomerRecord.AddressId = this.AddressRecord.AddressId;
            this.CustomerRecord.Active = true;
            this.CustomerRecord.CreateDate = DateTime.Now; // Local time based on Database CURRENT_TIMESTAMP
            this.CustomerRecord.CreatedBy = createdByUserName;
            this.CustomerRecord.LastUpdate = DateTime.Now; // Local time based on Database CURRENT_TIMESTAMP
            this.CustomerRecord.LastUpdateBy = lastUpdatedByUserName;
        }

        private void AddAppointment(AppState appState)
        {
            ValidateForNullAttribute(appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.Appointment], $"{typeof(DataTable)} \"{ClientScheduleDbSchema.TableName.Appointment}\"");

            // Simplify
            DataTable appointmentTable = appState.DbDataSet.DataSet.Tables[ClientScheduleDbSchema.TableName.Appointment]!;

            // AppState fetching data of AppState > DbDataSet > DataSet > Specific table
            int lastRowIndex = appointmentTable.Rows.Count > 0 ? appointmentTable.Rows.Count - 1 : firstIdIndex; // if there is any row get the last one if not it will be set to zero
            int newAppointmentId = ((int)appointmentTable.Rows[lastRowIndex][ClientScheduleDbSchema.AppointmentColumnName.AppointmentId]) + 1;
            string createdByUserName = (string)appointmentTable.Rows[0][ClientScheduleDbSchema.AllInCommonColumns.CreatedBy];
            string lastUpdatedByUserName = (string)appointmentTable.Rows[0][ClientScheduleDbSchema.AllInCommonColumns.LastUpdateBy];

            // Updating the new Record
            this.AppointmentRecord.AppointmentId = newAppointmentId;
            this.AppointmentRecord.CustomerId = this.CustomerRecord.CustomerId;
            this.AppointmentRecord.UserID = this.UserRecord.UserId;
            this.AppointmentRecord.Title = appState.AppData.AppointmentRecord.Title ?? "not needed"; // user value
            this.AppointmentRecord.Description = appState.AppData.AppointmentRecord.Description ?? "not needed"; // user value
            this.AppointmentRecord.Location = appState.AppData.AppointmentRecord.Location ?? "not needed"; // user value
            this.AppointmentRecord.Type = appState.AppData.AppointmentRecord.Type ?? String.Empty; // user value
            this.AppointmentRecord.Url = appState.AppData.AppointmentRecord.Url ?? "not needed";  // user value
            this.AppointmentRecord.Start = appState.AppData.AppointmentRecord.Start.ToUniversalTime();  // user value - UTC
            this.AppointmentRecord.End = appState.AppData.AppointmentRecord.End.ToUniversalTime();  // user value - UTC
            this.AppointmentRecord.CreateDate = DateTime.Now; // Local time based on Database CURRENT_TIMESTAMP
            this.AppointmentRecord.CreatedBy = createdByUserName;
            this.AppointmentRecord.LastUpdate = DateTime.Now; // Local time based on Database CURRENT_TIMESTAMP
            this.AppointmentRecord.LastUpdateBy = lastUpdatedByUserName;
        }

        private static void ValidateForNullAttribute(object? variable, string variableName)
        {
            if (variable is null)
            {
                throw new AppDataNullException("<Scheduling_API.Controller.State>(AppState)\n" +
                    new ArgumentNullException($"[{variable}][{variableName}] has not been filled and mapped yet."));
            }
        }
    }
}
