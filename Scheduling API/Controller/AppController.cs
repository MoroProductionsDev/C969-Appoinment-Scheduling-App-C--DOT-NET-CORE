using Scheduling_API.Controller.Process;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Structure;
using System.Collections;

namespace Scheduling_API.Controller
{
    // It redirects the flow of the application.
    // It should be called from the application's view and it takes the control
    // pass it down the AppState for specic models processing.
    public static class AppController
    {
        public static void AuthenticateLogIn(AppState? appState)
        {
            AppState.StaticValidateAppStateForNull(appState);
            appState?.Authenticate();
        }

        public static void UnAutenticate(AppState? appState)
        {
            AppState.StaticValidateAppStateForNull(appState);
            appState?.UnAuthenticate();
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

        public static void GetRecordID<T>(AppState? appState, FetchIdDbMetaData<T> fetchIdDbMetaData)
        {
            AppState.StaticValidateAppStateForNull(appState);

            appState!.StoredFetchedId(appState!.DbDataSet.GetRowId(fetchIdDbMetaData));
        }

        public static void AddCustomerRecord(AppState? appState)
        {
            AppState.StaticValidateAppStateForNull(appState);

            // Gather user details and default values
            appState!.AppData.AddCustomerValAndDefaultVal(appState);

            // -- Country --
            // Prepare data for the Data Set
            string insertCountrySqlStatement = AppData.GetInsertCountrySQlStatement();
            string[] countryColumnNames = ClientScheduleDbSchema.GetCountryColumnNames();
            ArrayList countryValues = appState!.AppData.GetRecordValuesAsArrayList<ClientScheduleData.CountryRecord>();

            // Insert in the DataSet in the Database
            appState!.DbDataSet.Insert(ClientScheduleDbSchema.TableName.Country,
                                        countryColumnNames,
                                        insertCountrySqlStatement,
                                        countryValues);

            // -- City --
            // Prepare data for the Data Set
            string insertCitySqlStatement = AppData.GetInsertCitySQlStatement();
            string[] cityColumnNames = ClientScheduleDbSchema.GetCityColumnNames();
            ArrayList cityValues = appState!.AppData.GetRecordValuesAsArrayList<ClientScheduleData.CityRecord>();

            // Insert in the DataSet in the Database
            appState!.DbDataSet.Insert(ClientScheduleDbSchema.TableName.City,
                                        cityColumnNames,
                                        insertCitySqlStatement,
                                        cityValues);

            // -- Address --
            // Prepare data for the Data Set
            string insertAddressSqlStatement = AppData.GetInsertAddressSQlStatement();
            string[] addressColumnNames = ClientScheduleDbSchema.GetAddressColumnNames();
            ArrayList addressValues = appState!.AppData.GetRecordValuesAsArrayList<ClientScheduleData.AddressRecord>();

            // Insert in the DataSet in the Database
            appState!.DbDataSet.Insert(ClientScheduleDbSchema.TableName.Address,
                                        addressColumnNames,
                                        insertAddressSqlStatement,
                                        addressValues);

            // -- Customer --
            // Prepare data for the Data Set
            string insertCustomerSqlStatement = AppData.GetInsertCustomerSQlStatement();
            string[] customerColumnNames = ClientScheduleDbSchema.GetCustomerColumnNames();
            ArrayList customersValues = appState!.AppData.GetRecordValuesAsArrayList<ClientScheduleData.CustomerRecord>();

            // Insert in the DataSet in the Database
            appState!.DbDataSet.Insert(ClientScheduleDbSchema.TableName.Customer,
                                        customerColumnNames,
                                        insertCustomerSqlStatement,
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
            string insertAppointmentSqlStatement = AppData.GetInsertAppointmentSQlStatement();
            string[] appointmentColumnNames = ClientScheduleDbSchema.GetAppointmentColumnNames();
            ArrayList appointmentValues = appState!.AppData.GetRecordValuesAsArrayList<ClientScheduleData.AppointmentRecord>();

            // Insert in the DataSet in the Database
            appState!.DbDataSet.Insert(ClientScheduleDbSchema.TableName.Appointment,
                                       appointmentColumnNames,
                                       insertAppointmentSqlStatement,
                                       appointmentValues);

        }

        public static void UpdateRecord<T>(AppState? appState, UpdateDbMetaData<T> updateMetaData)
        {
            AppState.StaticValidateAppStateForNull(appState);

            // -- Customer --
            // Prepare data for the Data Set
            string updateSqlStatement = AppData.GetUpdateSQLStatement(updateMetaData);
            appState?.DbDataSet.Update(updateMetaData, updateSqlStatement);
        }

        public static void DeleteRecord(AppState? appState, DeleteDbMetaData deleteDatabaseMetaData)
        {
            AppState.StaticValidateAppStateForNull(appState);

            string deleteAppointmentSqlStatement = AppData.GetDeleteSQLStatement(deleteDatabaseMetaData);

            appState?.DbDataSet.Delete(deleteDatabaseMetaData,
                                        deleteAppointmentSqlStatement);
        }

        public static void GenerateReport(AppState? appState, Report.Type reportType)
        {
            AppState.StaticValidateAppStateForNull(appState);

            appState?.Report.Generate(reportType);
        }
    }
}
