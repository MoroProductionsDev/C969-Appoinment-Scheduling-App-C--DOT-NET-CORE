using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Structure;
using Scheduling_Logic.Model.Data;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;
using Scheduling_API.Controller.Process;
using System.ComponentModel.DataAnnotations;

namespace Scheduling_API.Controller
{
    public static class AppController
    {
        public static void AuthenticateLogIn(AppState? appState)
        {
            AppState.StaticValidateAppStateForNull(appState);
            appState?.Authenticate();
        }

        public static void MapDataBaseToDataSet(AppState? appState)
        {
            AppState.StaticValidateAppStateForNull(appState);
            appState?.DbDataSet.Mapping();
        }

        public static void FetchLocationAndTimeZone(AppState? appState)
        {
            AppState.StaticValidateAppStateForNull(appState);

            appState!.GetGeoLocationData();
        }

        public static void AddCustomerRecord(AppState? appState)
        {
            AppState.StaticValidateAppStateForNull(appState);

            // Gather user details and default values
            appState!.AppData.AddCustomerValAndDefaultVal(appState);

            // -- Country --
            // Prepare data for the Data Set
            string insertCountrySqlStatement = appState!.AppData.GetInsertCountrySQlStatement(appState);
            string[] countryColumnNames = ((ClientScheduleDbSchema) appState!.DbSchema).GetCountryColumnNames();
            ArrayList countryValues = appState!.AppData.GetRecordValuesAsArrayList<ClientScheduleRecord.CountryRecord>();

            // Insert in the DataSet in the Database
            appState!.DbDataSet.Insert(ClientScheduleTableName.Country,
                                        insertCountrySqlStatement,
                                        countryColumnNames,
                                        countryValues);

            // -- City --
            // Prepare data for the Data Set
            string insertCitySqlStatement = appState!.AppData.GetInsertCitySQlStatement(appState);
            string[] cityColumnNames = ((ClientScheduleDbSchema)appState!.DbSchema).GetCityColumnNames();
            ArrayList cityValues = appState!.AppData.GetRecordValuesAsArrayList<ClientScheduleRecord.CityRecord>();

            // Insert in the DataSet in the Database
            appState!.DbDataSet.Insert(ClientScheduleTableName.City,
                                        insertCitySqlStatement,
                                        cityColumnNames,
                                        cityValues);

            // -- Address --
            // Prepare data for the Data Set
            string insertAddressSqlStatement = appState!.AppData.GetInsertAddressSQlStatement(appState);
            string[] addressColumnNames = ((ClientScheduleDbSchema)appState!.DbSchema).GetAddressColumnNames();
            ArrayList addressValues = appState!.AppData.GetRecordValuesAsArrayList<ClientScheduleRecord.AddressRecord>();

            // Insert in the DataSet in the Database
            appState!.DbDataSet.Insert(ClientScheduleTableName.Address,
                                        insertAddressSqlStatement,
                                        addressColumnNames,
                                        addressValues);

            // -- Customer --
            // Prepare data for the Data Set
            string insertCustomerSqlStatement = appState!.AppData.GetInsertCustomerSQlStatement(appState);
            string[] customerColumnNames = ((ClientScheduleDbSchema)appState!.DbSchema).GetCustomerColumnNames();
            ArrayList customersValues = appState!.AppData.GetRecordValuesAsArrayList<ClientScheduleRecord.CustomerRecord>();

            // Insert in the DataSet in the Database
            appState!.DbDataSet.Insert(ClientScheduleTableName.Customer,
                                        insertCustomerSqlStatement,
                                        customerColumnNames,
                                        customersValues);
        }

        public static void AddAppointmentRecord(AppState? appState)
        {
            AppState.StaticValidateAppStateForNull(appState);


            // Gather user details and default values
            appState!.AppData.AddAppointmentValAndDefaultVal(appState);

            appState!.ValidateAppointment();

            // -- Appointment --
            // Prepare data for the Data Set
            string insertAppointmentSqlStatement = appState!.AppData.GetInsertAppointmentSQlStatement(appState);
            string[] appointmentColumnNames = ((ClientScheduleDbSchema)appState!.DbSchema).GetAppointmentColumnNames();
            ArrayList appointmentValues = appState!.AppData.GetRecordValuesAsArrayList<ClientScheduleRecord.AppointmentRecord>();

            // Insert in the DataSet in the Database
            appState!.DbDataSet.Insert(ClientScheduleTableName.Appointment,
                                       insertAppointmentSqlStatement,
                                       appointmentColumnNames,
                                       appointmentValues);

        }

        public static void UpdateCustomerRecord(AppState? appState)
        {
            AppState.StaticValidateAppStateForNull(appState);

            appState?.DbDataSet.Update<string>(ClientScheduleDbSchema._dbName,  ClientScheduleTableName.Customer, CustomerColumnName.CustomerName, "Ina Prufung", "Moro Men");
        }

        public static void DeleteCustomerRecord(AppState? appState) {
            AppState.StaticValidateAppStateForNull(appState);

            appState?.DbDataSet.Delete<string>(ClientScheduleTableName.Customer, CustomerColumnName.CustomerName, "Moro Men");
        }

        public static void GenerateReport(AppState? appState)
        {
            AppState.StaticValidateAppStateForNull(appState);

            appState?.Report.Generate(Report.Type.LocationSchedule);
        }
    }
}
