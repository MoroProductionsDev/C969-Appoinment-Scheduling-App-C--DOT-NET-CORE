using System.Data;
using Scheduling_Logic.Model.Structure;

namespace Scheduling_API.Controller.Process
{
    // This class is use to generate different reports of the customers and appoinments' data.
    public class Report
    {
        private readonly DataSet dataSet;
        public DataTable ReportTable;
        public enum Type
        {
            AppointmentMonthlyTypes,
            ConsultantSchedule,
            LocationSchedule
        }
        const string monthColName = "month";
        const string yearColName = "year";
        const string typeColName = "type";
        const string countColName = "count";

        public Report(DataSet dataSet)
        {
            this.dataSet = dataSet;
            this.ReportTable = new DataTable();
        }

        public void Generate(Type reportType)
        {
            this.ReportTable.Clear();
            this.ReportTable.Columns.Clear();

            switch (reportType)
            {
                case Type.AppointmentMonthlyTypes:
                    SetReportedTableColumns(reportType);
                    CreateAppointmentTypesPerMonth();
                    break;
                case Type.ConsultantSchedule:
                    SetReportedTableColumns(reportType);
                    CreateConsultantSchedule();
                    break;
                case Type.LocationSchedule:
                    SetReportedTableColumns(reportType);
                    CreateCustomerLocationPerAppointmentReport();
                    break;
            }
        }

        private void SetReportedTableColumns(Type reportType)
        {
            Action SetAppointmentMonthsReportTable = () =>
            {
                this.ReportTable.TableName = "AppointmentMonthsReport";

                DataColumn month = new(monthColName, typeof(int));
                DataColumn year = new(yearColName, typeof(int));
                DataColumn type = new(typeColName);
                DataColumn count = new(countColName, typeof(int));

                this.ReportTable.Columns.AddRange(new DataColumn[] { month, year, type, count });
            };

            Action SetConsultantScheduleReportTable = () =>
            {
                this.ReportTable.TableName = "ConsultantScheduleReport";

                DataColumn UserName = new(ClientScheduleDbSchema.UserColumnName.UserName);
                DataColumn CustomerName = new(ClientScheduleDbSchema.CustomerColumnName.CustomerName);
                DataColumn appointmentTitle = new(ClientScheduleDbSchema.AppointmentColumnName.Title);
                DataColumn appointmentDescription = new(ClientScheduleDbSchema.AppointmentColumnName.Description);
                DataColumn appointmentLocation = new(ClientScheduleDbSchema.AppointmentColumnName.Location);
                DataColumn apointmentStartDateTime = new(ClientScheduleDbSchema.AppointmentColumnName.Start);
                DataColumn apointmentEndDateTime = new(ClientScheduleDbSchema.AppointmentColumnName.End);

                this.ReportTable.Columns.AddRange(new[] { UserName, CustomerName, appointmentTitle, appointmentDescription,
                                            appointmentLocation, apointmentStartDateTime, apointmentEndDateTime});
            };

            Action SetCustomerLocationReportTable = () =>
            {
                this.ReportTable.TableName = "CustomerLocationReport";

                DataColumn CityName = new(ClientScheduleDbSchema.CityColumnName.City);
                DataColumn CountryName = new(ClientScheduleDbSchema.CountryColumnName.Country);
                DataColumn CustomerName = new(ClientScheduleDbSchema.CustomerColumnName.CustomerName);
                DataColumn appointmentTitle = new(ClientScheduleDbSchema.AppointmentColumnName.Title);

                this.ReportTable.Columns.AddRange(new[] { CityName, CountryName, CustomerName, appointmentTitle });
            };

            switch (reportType)
            {
                case Type.AppointmentMonthlyTypes:
                    SetAppointmentMonthsReportTable();
                    break;
                case Type.ConsultantSchedule:
                    SetConsultantScheduleReportTable();
                    break;
                case Type.LocationSchedule:
                    SetCustomerLocationReportTable();
                    break;
            }
        }

        private void CreateAppointmentTypesPerMonth()
        {
            // funtional/anonymous lambdas fuction pointers use for clear readabilty and
            // to store the functions at the body and call it after when needed. (Closure)
            Func<DataTable> GetSortedAppointmentTable = () =>
            {
                DataTable appointmentTable = this.dataSet.Tables[ClientScheduleDbSchema.TableName.Appointment]!;
                return appointmentTable.AsEnumerable()
                  .OrderBy(r => r.Field<DateTime>(ClientScheduleDbSchema.AppointmentColumnName.Start))
                  .ThenBy(r => r.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Type)).CopyToDataTable();
            };

            Action<Dictionary<string, int>, List<int>> AssiggnValReportTable = (appointmentTypeOfTheMonth, DateIntegrals) =>
            {
                foreach (var aptType in appointmentTypeOfTheMonth)
                {
                    DataRow newAppointmentRow = this.ReportTable.NewRow();

                    newAppointmentRow[monthColName] = DateIntegrals[0];
                    newAppointmentRow[yearColName] = DateIntegrals[1];
                    newAppointmentRow[typeColName] = aptType.Key;
                    newAppointmentRow[countColName] = aptType.Value;

                    this.ReportTable.Rows.Add(newAppointmentRow);
                }
            };

            DataTable? sortedTable = GetSortedAppointmentTable();
            int currentMonth = 0, currentYear = 0;
            string currentType = String.Empty;
            Dictionary<string, int> Dct_appointmentTypeOfTheMonth = new();

            if (sortedTable is null)
            {
                return;
            }

            foreach (DataRow row in sortedTable!.Rows)
            {
                DateTime appointmentLocalDate = ((DateTime)row[ClientScheduleDbSchema.AppointmentColumnName.Start]).ToLocalTime();
                string appointmentType = (string)row[ClientScheduleDbSchema.AppointmentColumnName.Type];

                if (0 == currentMonth && 0 == currentYear && currentType.Equals(string.Empty))
                {
                    currentMonth = appointmentLocalDate.Month;
                    currentYear = appointmentLocalDate.Year;
                    currentType = appointmentType;
                }

                if (currentMonth != appointmentLocalDate.Month || currentYear != appointmentLocalDate.Year)
                {
                    AssiggnValReportTable(Dct_appointmentTypeOfTheMonth, new List<int>() { currentMonth, currentYear });

                    Dct_appointmentTypeOfTheMonth.Clear();

                    currentMonth = appointmentLocalDate.Month;
                    currentYear = appointmentLocalDate.Year;
                    currentType = appointmentType;
                }

                if (!Dct_appointmentTypeOfTheMonth.ContainsKey(appointmentType))
                {
                    Dct_appointmentTypeOfTheMonth.Add(appointmentType, 0);
                }

                ++Dct_appointmentTypeOfTheMonth[appointmentType];
            }

            AssiggnValReportTable(Dct_appointmentTypeOfTheMonth, new List<int>() { currentMonth, currentYear });
        }
        private void CreateConsultantSchedule()
        {
            DataTable appointmentTable = this.dataSet.Tables[ClientScheduleDbSchema.TableName.Appointment]!;
            DataTable userTable = this.dataSet.Tables[ClientScheduleDbSchema.TableName.User]!;
            DataTable customerTable = this.dataSet.Tables[ClientScheduleDbSchema.TableName.Customer]!;

            HashSet<int> userUniqueIds = new(); // to fetch unique Id sets

            foreach (DataRow row in appointmentTable.Rows)
            {
                userUniqueIds.Add((int)row[ClientScheduleDbSchema.AppointmentColumnName.UserId]); // unique ids from another table foreign keys
            }

            foreach (int userId in userUniqueIds)
            {
                var query = from apptRow in appointmentTable.AsEnumerable()
                            join userRow in userTable.AsEnumerable()
                            on apptRow.Field<int>(ClientScheduleDbSchema.AppointmentColumnName.UserId) 
                                equals userRow.Field<int>(ClientScheduleDbSchema.UserColumnName.UserId)
                            join customerRow in customerTable.AsEnumerable()
                            on apptRow.Field<int>(ClientScheduleDbSchema.AppointmentColumnName.CustomerId) 
                                equals customerRow.Field<int>(ClientScheduleDbSchema.CustomerColumnName.CustomerId)
                            select new
                            {
                                UserName = userRow.Field<string>(ClientScheduleDbSchema.UserColumnName.UserName),
                                CustomerName = customerRow.Field<string>(ClientScheduleDbSchema.CustomerColumnName.CustomerName),
                                AppointmentTitle = apptRow.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Title),
                                AppointmentDescription = apptRow.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Description),
                                AppointmentLocation = apptRow.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Location),
                                AppointmentLocalStartDateTime = apptRow.Field<DateTime>(ClientScheduleDbSchema.AppointmentColumnName.Start).ToLocalTime(),
                                AppointmentLocalEndDateTime = apptRow.Field<DateTime>(ClientScheduleDbSchema.AppointmentColumnName.End).ToLocalTime()
                            };


                // Lambda expresion that assigns values to new data row based on the prvious IEnumerable <a'>
                // annonymous type respond from the query above.
                query.ToList().ForEach(item =>
                {
                    DataRow newConsultantScheduleRow = this.ReportTable.NewRow();

                    newConsultantScheduleRow[ClientScheduleDbSchema.UserColumnName.UserName] = item.UserName;
                    newConsultantScheduleRow[ClientScheduleDbSchema.CustomerColumnName.CustomerName] = item.CustomerName;
                    newConsultantScheduleRow[ClientScheduleDbSchema.AppointmentColumnName.Title] = item.AppointmentTitle;
                    newConsultantScheduleRow[ClientScheduleDbSchema.AppointmentColumnName.Description] = item.AppointmentDescription;
                    newConsultantScheduleRow[ClientScheduleDbSchema.AppointmentColumnName.Location] = item.AppointmentLocation;
                    newConsultantScheduleRow[ClientScheduleDbSchema.AppointmentColumnName.Start] = item.AppointmentLocalStartDateTime;
                    newConsultantScheduleRow[ClientScheduleDbSchema.AppointmentColumnName.End] = item.AppointmentLocalEndDateTime;

                    this.ReportTable.Rows.Add(newConsultantScheduleRow);
                });
            }
        }

        private void CreateCustomerLocationPerAppointmentReport()
        {
            DataTable countryTable = this.dataSet.Tables[ClientScheduleDbSchema.TableName.Country]!;
            DataTable cityTable = this.dataSet.Tables[ClientScheduleDbSchema.TableName.City]!;
            DataTable addressTable = this.dataSet.Tables[ClientScheduleDbSchema.TableName.Address]!;
            DataTable customerTable = this.dataSet.Tables[ClientScheduleDbSchema.TableName.Customer]!;
            DataTable appointmentTable = this.dataSet.Tables[ClientScheduleDbSchema.TableName.Appointment]!;


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
                        join appointmentRow in appointmentTable.AsEnumerable()
                        on customerRow.Field<int>(ClientScheduleDbSchema.CustomerColumnName.CustomerId) 
                            equals appointmentRow.Field<int>(ClientScheduleDbSchema.AppointmentColumnName.CustomerId)
                        select new
                        {
                            CityName = cityRow.Field<string>(ClientScheduleDbSchema.CityColumnName.City),
                            CountryName = countryRow.Field<string>(ClientScheduleDbSchema.CountryColumnName.Country),
                            CustomerName = customerRow.Field<string>(ClientScheduleDbSchema.CustomerColumnName.CustomerName),
                            AppointmentTitle = appointmentRow.Field<string>(ClientScheduleDbSchema.AppointmentColumnName.Title),
                        };


            query.ToList().ForEach(item =>
            {
                DataRow newCustomerLocationRow = this.ReportTable.NewRow();

                newCustomerLocationRow[ClientScheduleDbSchema.CityColumnName.City] = item.CityName;
                newCustomerLocationRow[ClientScheduleDbSchema.CountryColumnName.Country] = item.CountryName;
                newCustomerLocationRow[ClientScheduleDbSchema.CustomerColumnName.CustomerName] = item.CustomerName;
                newCustomerLocationRow[ClientScheduleDbSchema.AppointmentColumnName.Title] = item.AppointmentTitle;

                this.ReportTable.Rows.Add(newCustomerLocationRow);
            });
        }
    }
}
