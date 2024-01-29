using System.Data;
using Scheduling_Logic.Model.Structure;

namespace Scheduling_API.Controller.State
{
    // It provided specific database view that the user would see the overall data from the system.
    // One of the customers' informatin and the other of the appointments' information.
    public class AppDataView
    {
        const string date = "date";
        const string time = "time";
        private readonly DataSet tableDataSet;

        public AppDataView(AppState appState)
        {
            this.tableDataSet = appState.DbDataSet.DataSet;
        }

        public DataTable CustomerTableView
        {
            get
            {
                return GetCustomerDataView();
            }
        }

        public DataTable AppointmentTableView
        {
            get
            {
                return GetAppointmentDataView();
            }
        }

        private static void SetCustomerTable(DataTable customerTableView)
        {
            customerTableView.TableName = "CustomerTableView";

            customerTableView.Columns.AddRange(new DataColumn[] {
                new DataColumn(ClientScheduleDbSchema.CustomerColumnName.CustomerId, typeof(int)),
                new DataColumn(ClientScheduleDbSchema.CustomerColumnName.CustomerName),
                new DataColumn(ClientScheduleDbSchema.AddressColumnName.Phone),
                new DataColumn(ClientScheduleDbSchema.AddressColumnName.Address),
                new DataColumn(ClientScheduleDbSchema.AddressColumnName.Address2),
                new DataColumn(ClientScheduleDbSchema.CityColumnName.City),
                new DataColumn(ClientScheduleDbSchema.CountryColumnName.Country),
            });
        }

        private static void SetAppointmentTable(DataTable appointmentTableView)
        {
            appointmentTableView.TableName = "AppointmentTableView";

            appointmentTableView.Columns.AddRange(new DataColumn[] {
                 new DataColumn(ClientScheduleDbSchema.AppointmentColumnName.AppointmentId, typeof(int)),
                new DataColumn(ClientScheduleDbSchema.CustomerColumnName.CustomerName),
                new DataColumn(ClientScheduleDbSchema.AppointmentColumnName.Title),
                new DataColumn(ClientScheduleDbSchema.AppointmentColumnName.Description),
                new DataColumn(ClientScheduleDbSchema.AppointmentColumnName.Location),
                new DataColumn(ClientScheduleDbSchema.AppointmentColumnName.Contact),
                new DataColumn(ClientScheduleDbSchema.AppointmentColumnName.Type),
                new DataColumn(ClientScheduleDbSchema.AppointmentColumnName.Url),
                new DataColumn($"{ClientScheduleDbSchema.AppointmentColumnName.Start} {date}", typeof(DateTime)),
                new DataColumn($"{ClientScheduleDbSchema.AppointmentColumnName.Start} {time}", typeof(string)),
                new DataColumn($"{ClientScheduleDbSchema.AppointmentColumnName.End} {date}", typeof(DateTime)),
                new DataColumn($"{ClientScheduleDbSchema.AppointmentColumnName.End} {time}", typeof(string)),
            });
        }

        private DataTable GetCustomerDataView()
        {
            DataTable viewTable = new();

            DataTable countryTable = tableDataSet.Tables[ClientScheduleDbSchema.TableName.Country]!;
            DataTable cityTable = tableDataSet.Tables[ClientScheduleDbSchema.TableName.City]!;
            DataTable addressTable = tableDataSet.Tables[ClientScheduleDbSchema.TableName.Address]!;
            DataTable customerTable = tableDataSet.Tables[ClientScheduleDbSchema.TableName.Customer]!;

            SetCustomerTable(viewTable);

            var query = from cityRow in cityTable.AsEnumerable()
                        join countryRow in countryTable.AsEnumerable()
                        on cityRow.Field<int>(ClientScheduleDbSchema.CityColumnName.CountryId) 
                            equals countryRow.Field<int>(ClientScheduleDbSchema.CountryColumnName.CountryId)
                        join addressRow in addressTable.AsEnumerable()
                        on cityRow.Field<int>(ClientScheduleDbSchema.CityColumnName.CityId) 
                            equals addressRow.Field<int>(ClientScheduleDbSchema.AddressColumnName.CityId)
                        join customerRow in customerTable.AsEnumerable()
                        on addressRow.Field<int>(ClientScheduleDbSchema.AddressColumnName.AddressId) 
                            equals customerRow.Field<int>(ClientScheduleDbSchema.CustomerColumnName.AddressId)
                        orderby customerRow.Field<int>(ClientScheduleDbSchema.CustomerColumnName.CustomerId)
                        select new
                        {
                            CustomerId = customerRow.Field<int>(ClientScheduleDbSchema.CustomerColumnName.CustomerId),
                            CustomerName = customerRow.Field<string>(ClientScheduleDbSchema.CustomerColumnName.CustomerName),
                            Phone = addressRow.Field<string>(ClientScheduleDbSchema.AddressColumnName.Phone),
                            Address = addressRow.Field<string>(ClientScheduleDbSchema.AddressColumnName.Address),
                            Address2 = addressRow.Field<string>(ClientScheduleDbSchema.AddressColumnName.Address2),
                            CityName = cityRow.Field<string>(ClientScheduleDbSchema.CityColumnName.City),
                            CountryName = countryRow.Field<string>(ClientScheduleDbSchema.CountryColumnName.Country),
                        };

            query.ToList().ForEach(item =>
            {
                DataRow newCustomerViewRow = viewTable.NewRow();

                newCustomerViewRow[ClientScheduleDbSchema.CustomerColumnName.CustomerId] = item.CustomerId;
                newCustomerViewRow[ClientScheduleDbSchema.CustomerColumnName.CustomerName] = item.CustomerName;
                newCustomerViewRow[ClientScheduleDbSchema.AddressColumnName.Phone] = item.Phone;
                newCustomerViewRow[ClientScheduleDbSchema.AddressColumnName.Address] = item.Address;
                newCustomerViewRow[ClientScheduleDbSchema.AddressColumnName.Address2] = item.Address2;
                newCustomerViewRow[ClientScheduleDbSchema.CityColumnName.City] = item.CityName;
                newCustomerViewRow[ClientScheduleDbSchema.CountryColumnName.Country] = item.CountryName;

                viewTable.Rows.Add(newCustomerViewRow);
            });

            return viewTable;
        }

        private DataTable GetAppointmentDataView()
        {
            DataTable viewTable = new();

            DataTable countryTable = tableDataSet.Tables[ClientScheduleDbSchema.TableName.Country]!;
            DataTable cityTable = tableDataSet.Tables[ClientScheduleDbSchema.TableName.City]!;
            DataTable addressTable = tableDataSet.Tables[ClientScheduleDbSchema.TableName.Address]!;
            DataTable customerTable = tableDataSet.Tables[ClientScheduleDbSchema.TableName.Customer]!;
            DataTable appointmentTable = tableDataSet.Tables[ClientScheduleDbSchema.TableName.Appointment]!;

            SetAppointmentTable(viewTable);

            var query = from customerRow in customerTable.AsEnumerable()
                        join appointmentRow in appointmentTable.AsEnumerable()
                        on customerRow.Field<int>(ClientScheduleDbSchema.CustomerColumnName.CustomerId) 
                            equals appointmentRow.Field<int>(ClientScheduleDbSchema.AppointmentColumnName.CustomerId)
                        orderby appointmentRow.Field<int>(ClientScheduleDbSchema.AppointmentColumnName.AppointmentId)
                        select new
                        {
                            AppointmentId = appointmentRow.Field<int>(ClientScheduleDbSchema.AppointmentColumnName.AppointmentId),
                            CustomerName = customerRow.Field<string>(ClientScheduleDbSchema.CustomerColumnName.CustomerName),
                            AppointmentTitle = appointmentRow.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Title),
                            AppointmentDescription = appointmentRow.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Description),
                            AppointmentLocation = appointmentRow.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Location),
                            AppointmentContact = appointmentRow.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Contact),
                            AppointmentType = appointmentRow.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Type),
                            AppointmentUrl = appointmentRow.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Url),
                            AppointmentStart = appointmentRow.Field<DateTime>(ClientScheduleDbSchema.AppointmentColumnName.Start),
                            AppointmentEnd = appointmentRow.Field<DateTime>(ClientScheduleDbSchema.AppointmentColumnName.End),
                        };

            query.ToList().ForEach(item =>
            {
                DataRow newAppointmentViewRow = viewTable.NewRow();

                newAppointmentViewRow[ClientScheduleDbSchema.AppointmentColumnName.AppointmentId] = item.AppointmentId;
                newAppointmentViewRow[ClientScheduleDbSchema.CustomerColumnName.CustomerName] = item.CustomerName;
                newAppointmentViewRow[ClientScheduleDbSchema.AppointmentColumnName.Title] = item.AppointmentTitle;
                newAppointmentViewRow[ClientScheduleDbSchema.AppointmentColumnName.Description] = item.AppointmentDescription;
                newAppointmentViewRow[ClientScheduleDbSchema.AppointmentColumnName.Location] = item.AppointmentLocation;
                newAppointmentViewRow[ClientScheduleDbSchema.AppointmentColumnName.Contact] = item.AppointmentContact;
                newAppointmentViewRow[ClientScheduleDbSchema.AppointmentColumnName.Type] = item.AppointmentType;
                newAppointmentViewRow[ClientScheduleDbSchema.AppointmentColumnName.Url] = item.AppointmentUrl;
                newAppointmentViewRow[$"{ClientScheduleDbSchema.AppointmentColumnName.Start} {date}"] = item.AppointmentStart.ToLocalTime().ToShortDateString();
                newAppointmentViewRow[$"{ClientScheduleDbSchema.AppointmentColumnName.Start} {time}"] = item.AppointmentStart.ToLocalTime().ToShortTimeString();
                newAppointmentViewRow[$"{ClientScheduleDbSchema.AppointmentColumnName.End} {date}"] = item.AppointmentEnd.ToLocalTime().ToShortDateString();
                newAppointmentViewRow[$"{ClientScheduleDbSchema.AppointmentColumnName.End} {time}"] = item.AppointmentEnd.ToLocalTime().ToShortTimeString();

                viewTable.Rows.Add(newAppointmentViewRow);
            });

            return viewTable;
        }
    }
}
