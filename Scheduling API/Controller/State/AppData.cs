using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Logic.Model.Config;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Database;
using Scheduling_Logic.Model.Structure;
using static Scheduling_Logic.Model.Data.ClientScheduleRecord;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_API.Controller.State
{
    public sealed class AppData
    {
        const char ParameterSymbol = '@';
        const int firstIdIndex = 1;
        public UserRecord UserRecord { get; private set; }
        public AppointmentRecord AppointmentRecord { get; private set; }
        public CustomerRecord CustomerRecord { get; private set; }
        public AddressRecord AddressRecord { get; private set; }
        public CityRecord CityRecord { get; private set; }
        public CountryRecord CountryRecord { get; private set; }

        public AppData()
        {
            this.UserRecord = new UserRecord();
            this.AppointmentRecord = new AppointmentRecord();
            this.CustomerRecord = new CustomerRecord();
            this.AddressRecord = new AddressRecord();
            this.CityRecord = new CityRecord();
            this.CountryRecord = new CountryRecord();
        }

        public void AddUser(ImmutableArray<DataRow> currentUserDataRow)
        {
            this.UserRecord.UserId = (int)currentUserDataRow[0][0];
            this.UserRecord.UserName = (string)currentUserDataRow[0][1];
            // Skip the password, no need to store it.
            this.UserRecord.Active = (bool)currentUserDataRow[0][3];
            this.UserRecord.CreateDate = (DateTime)currentUserDataRow[0][4];
            this.UserRecord.CreatedBy = (string)currentUserDataRow[0][5];
            this.UserRecord.LastUpdate = (DateTime)currentUserDataRow[0][6];
            this.UserRecord.LastUpdateBy = (string)currentUserDataRow[0][7];
        }

        internal void AddUserValAndDefaultVal(AppState appState)
        {
            AddCountry(appState);
            AddCity(appState);
            AddCity(appState);
            AddAddress(appState);
            AddCustomer(appState);
            AddAppointment(appState);
        }

        internal string GetInsertCountrySQlStatement(AppState appState) {
            ClientScheduleDbSchema clientScheduleSchema = (ClientScheduleDbSchema) appState.DbSchema;
            string[] parameters = clientScheduleSchema.GetCountryColumnNames();
            string insertStatement = $"INSERT INTO {ClientScheduleTableName.Country} (";
           

            for (int idx = 0; idx < parameters.Length; ++idx)
            {
                insertStatement += $"{parameters[idx]}";

                if (idx == parameters.Length - 1)
                {
                    insertStatement += ") ";
                } else
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

        internal string GetInsertCitySQlStatement(AppState appState)
        {
            ClientScheduleDbSchema clientScheduleSchema = (ClientScheduleDbSchema) appState.DbSchema;
            string[] parameters = clientScheduleSchema.GetCityColumnNames();
            string insertStatement = $"INSERT INTO {ClientScheduleTableName.City} (";


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

        internal string GetInsertAddressSQlStatement(AppState appState)
        {
            ClientScheduleDbSchema clientScheduleSchema = (ClientScheduleDbSchema) appState.DbSchema;
            string[] parameters = clientScheduleSchema.GetAddressColumnNames();
            string insertStatement = $"INSERT INTO {ClientScheduleTableName.Address} (";


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

        internal string GetInsertCustomerSQlStatement(AppState appState)
        {
            ClientScheduleDbSchema clientScheduleSchema = (ClientScheduleDbSchema) appState.DbSchema;
            string[] parameters = clientScheduleSchema.GetCustomerColumnNames();
            string insertStatement = $"INSERT INTO {ClientScheduleTableName.Customer} (";


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

        internal string GetInsertAppointmentSQlStatement(AppState appState)
        {
            ClientScheduleDbSchema clientScheduleSchema = (ClientScheduleDbSchema) appState.DbSchema;
            string[] parameters = clientScheduleSchema.GetAppointmentColumnNames();
            string insertStatement = $"INSERT INTO {ClientScheduleTableName.Appointment} (";


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
            ArrayList recordValues = new ArrayList();

            switch (typeof(T))
            {
                case Type _ when typeof(CountryRecord) == typeof(T):
                    AssignValueToArrayList<CountryRecord>(this.CountryRecord, recordValues);
                    break;
                case Type _ when typeof(CityRecord) == typeof(T):
                    AssignValueToArrayList<CityRecord>(this.CityRecord, recordValues);
                    break;
                case Type _ when typeof(AddressRecord) == typeof(T):
                    AssignValueToArrayList<AddressRecord>(this.AddressRecord, recordValues);
                    break;
                case Type _ when typeof(CustomerRecord) == typeof(T):
                    AssignValueToArrayList<CustomerRecord>(this.CustomerRecord, recordValues);
                    break;
                case Type _ when typeof(AppointmentRecord) == typeof(T):
                    AssignValueToArrayList<AppointmentRecord>(this.AppointmentRecord, recordValues);
                    break;
                default:
                    throw new ArgumentException("<Scheduling_API.Controller.State>(AppState)\nThis is not one of AppData Record Type");
            }

            return recordValues;
        }

        private void AssignValueToArrayList<T>(T record, ArrayList recordValues)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                recordValues.Add(field.GetValue(record));
            }
        }

        private void AddCountry(AppState appState)
        {
            ValidateForNullAttribute(appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Country], $"{typeof(DataTable)} \"{ClientScheduleTableName.Country}\"");

            // Simplify
            DataTable countryTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Country]!;

            // AppState fetching data of AppState > DbDataSet > DataSet > Specific table
            int lastRowIndex = countryTable.Rows.Count > 0 ? countryTable.Rows.Count - 1 : firstIdIndex; // if there is any row get the last one if not it will be set to zero
            int newCountryId = ((int) countryTable.Rows[lastRowIndex][CountryColumnName.CountryId]) + 1;
            string createdByUserName = (string) countryTable.Rows[0][AllInCommonColumns.CreatedBy];
            string lastUpdatedByUserName = (string) countryTable.Rows[0][AllInCommonColumns.LastUpdateBy];

            // Updating the new Record
            this.CountryRecord.CountryId = newCountryId;
            this.CountryRecord.Country = appState.AppData.CountryRecord.Country ?? ""; // user value
            this.CountryRecord.CreateDate = DateTime.Now;
            this.CountryRecord.CreatedBy = createdByUserName;
            this.CountryRecord.LastUpdate = DateTime.Now;
            this.CountryRecord.LastUpdateBy = lastUpdatedByUserName;
        }

        private void AddCity(AppState appState)
        {
            ValidateForNullAttribute(appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.City], $"{typeof(DataTable)} \"{ClientScheduleTableName.City}\"");

            // Simplify
            DataTable cityTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.City]!;

            // AppState fetching data of AppState > DbDataSet > DataSet > Specific table
            int lastRowIndex = cityTable.Rows.Count > 0 ? cityTable.Rows.Count - 1 : firstIdIndex; // if there is any row get the last one if not it will be set to zero
            int newCityId = ((int) cityTable.Rows[lastRowIndex][CityColumnName.CityId]) + 1;
            string createdByUserName = (string) cityTable.Rows[0][AllInCommonColumns.CreatedBy];
            string lastUpdatedByUserName = (string) cityTable.Rows[0][AllInCommonColumns.LastUpdateBy];

            // Updating the new Record
            this.CityRecord.CityId = newCityId;
            this.CityRecord.City = appState.AppData.CityRecord.City; // user value
            this.CityRecord.CountryId = this.CountryRecord.CountryId;
            this.CityRecord.CreateDate = DateTime.Now;
            this.CityRecord.CreatedBy = createdByUserName;
            this.CityRecord.LastUpdate = DateTime.Now;
            this.CityRecord.LastUpdateBy = lastUpdatedByUserName;
        }

        private void AddAddress(AppState appState)
        {
            ValidateForNullAttribute(appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Address], $"{typeof(DataTable)} \"{ClientScheduleTableName.Address}\"");

            // Simplify
            DataTable addressTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Address]!;

            // AppState fetching data of AppState > DbDataSet > DataSet > Specific table
            int lastRowIndex = addressTable.Rows.Count > 0 ? addressTable.Rows.Count - 1 : firstIdIndex; // if there is any row get the last one if not it will be set to zero
            int newAddressId = ((int) addressTable.Rows[lastRowIndex][AddressColumnName.AddressId]) + 1;
            string createdByUserName = (string) addressTable.Rows[0][AllInCommonColumns.CreatedBy];
            string lastUpdatedByUserName = (string) addressTable.Rows[0][AllInCommonColumns.LastUpdateBy];

            // Updating the new Record
            this.AddressRecord.AddressId = newAddressId;
            this.AddressRecord.Address = appState.AppData.AddressRecord.Address ?? String.Empty; // user value
            this.AddressRecord.Address2 = appState.AppData.AddressRecord.Address2 ?? String.Empty; // user value
            this.AddressRecord.CityId = this.CityRecord.CityId;
            this.AddressRecord.PostalCode = appState.AppData.AddressRecord.PostalCode ?? String.Empty; // user value
            this.AddressRecord.Phone = appState.AppData.AddressRecord.Phone ?? "(###) ###-####";  // user value
            this.AddressRecord.CreateDate = DateTime.Now;
            this.AddressRecord.CreatedBy = createdByUserName;
            this.AddressRecord.LastUpdate = DateTime.Now;
            this.AddressRecord.LastUpdateBy = lastUpdatedByUserName;
        }

        private void AddCustomer(AppState appState)
        {
            ValidateForNullAttribute(appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Customer], $"{typeof(DataTable)} \"{ClientScheduleTableName.Customer}\"");

            // Simplify
            DataTable customerTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Customer]!;

            // AppState fetching data of AppState > DbDataSet > DataSet > Specific table
            int lastRowIndex = customerTable.Rows.Count > 0 ? customerTable.Rows.Count - 1 : firstIdIndex; // if there is any row get the last one if not it will be set to zero
            int newCustomersId = ((int) customerTable.Rows[lastRowIndex][CustomerColumnName.CustomerId]) + 1;
            string createdByUserName = (string) customerTable.Rows[0][AllInCommonColumns.CreatedBy];
            string lastUpdatedByUserName = (string) customerTable.Rows[0][AllInCommonColumns.LastUpdateBy];

            // Updating the new Record
            this.CustomerRecord.CustomerId = newCustomersId;
            this.CustomerRecord.CustomerName = appState.AppData.CustomerRecord.CustomerName ?? String.Empty; // user value
            this.CustomerRecord.AddressId = this.AddressRecord.AddressId;
            this.CustomerRecord.Active = true;
            this.CustomerRecord.CreateDate = DateTime.Now;
            this.CustomerRecord.CreatedBy = createdByUserName;
            this.CustomerRecord.LastUpdate = DateTime.Now;
            this.CustomerRecord.LastUpdateBy = lastUpdatedByUserName;
        }

        private void AddAppointment(AppState appState)
        {
            ValidateForNullAttribute(appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Appointment], $"{typeof(DataTable)} \"{ClientScheduleTableName.Appointment}\"");
            
            // Simplify
            DataTable appointmentTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Appointment]!;

            // AppState fetching data of AppState > DbDataSet > DataSet > Specific table
            int lastRowIndex = appointmentTable.Rows.Count > 0 ? appointmentTable.Rows.Count - 1 : firstIdIndex; // if there is any row get the last one if not it will be set to zero
            int newAppointmentId = ((int) appointmentTable.Rows[lastRowIndex][AppointmentColumnName.AppointmentId]) + 1;
            string createdByUserName = (string) appointmentTable.Rows[0][AllInCommonColumns.CreatedBy];
            string lastUpdatedByUserName = (string) appointmentTable.Rows[0][AllInCommonColumns.LastUpdateBy];

            // Updating the new Record
            this.AppointmentRecord.AppointmentId = newAppointmentId;
            this.AppointmentRecord.CustomerId = this.CustomerRecord.CustomerId;
            this.AppointmentRecord.UserID = this.UserRecord.UserId;
            this.AppointmentRecord.Title = appState.AppData.AppointmentRecord.Title ?? String.Empty; // user value
            this.AppointmentRecord.Description = appState.AppData.AppointmentRecord.Description ?? "not needed"; // user value
            this.AppointmentRecord.Location = appState.AppData.AppointmentRecord.Location ?? String.Empty; // user value
            this.AppointmentRecord.Type = appState.AppData.AppointmentRecord.Type ?? String.Empty; // user value
            this.AppointmentRecord.Url = appState.AppData.AppointmentRecord.Url ?? String.Empty;  // user value
            this.AppointmentRecord.Start = appState.AppData.AppointmentRecord.Start;  // user value
            this.AppointmentRecord.End = appState.AppData.AppointmentRecord.End;  // user value
            this.AppointmentRecord.CreateDate = DateTime.Now;
            this.AppointmentRecord.CreatedBy = createdByUserName;
            this.AppointmentRecord.LastUpdate = DateTime.Now;
            this.AppointmentRecord.LastUpdateBy = lastUpdatedByUserName;
        }

        private void ValidateForNullAttribute(object? variable, string variableName, [CallerMemberName] string callerName = "")
        {
            if (variable is null)
            {
                throw new AppDataNullException("<Scheduling_API.Controller.State>(AppState)\n" +
                    new ArgumentNullException($"[{variable}][{variableName}] has not been filled and mapped yet."));
            }
        }
    }
}
