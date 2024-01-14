using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Org.BouncyCastle.Tls.Crypto;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_API.Controller.Process
{
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

                DataColumn month = new DataColumn(monthColName, typeof(int));
                DataColumn year = new DataColumn(yearColName, typeof(int));
                DataColumn type = new DataColumn(typeColName);
                DataColumn count = new DataColumn(countColName, typeof(int));

                this.ReportTable.Columns.AddRange(new DataColumn[] { month, year, type, count });
            };

            Action SetConsultantScheduleReportTable = () =>
            {
                this.ReportTable.TableName = "ConsultantScheduleReport";

                DataColumn UserName = new DataColumn(UserColumnName.UserName);
                DataColumn CustomerName = new DataColumn(CustomerColumnName.CustomerName);
                DataColumn appointmentTitle = new DataColumn(AppointmentColumnName.Title);
                DataColumn appointmentDescription = new DataColumn(AppointmentColumnName.Description);
                DataColumn appointmentLocation = new DataColumn(AppointmentColumnName.Location);
                DataColumn apointmentStartDateTime = new DataColumn(AppointmentColumnName.Start);
                DataColumn apointmentEndDateTime = new DataColumn(AppointmentColumnName.End);

                this.ReportTable.Columns.AddRange(new DataColumn[] { UserName, CustomerName, appointmentTitle, appointmentDescription,
                                            appointmentLocation, apointmentStartDateTime, apointmentEndDateTime});
            };

            Action SetCustomerLocationReportTable = () =>
            {
                this.ReportTable.TableName = "CustomerLocationReport";

                DataColumn CityName = new DataColumn(CityColumnName.City);
                DataColumn CountryName = new DataColumn(CountryColumnName.Country);
                DataColumn CustomerName = new DataColumn(CustomerColumnName.CustomerName);
                DataColumn appointmentTitle = new DataColumn(AppointmentColumnName.Title);

                this.ReportTable.Columns.AddRange(new DataColumn[] { CityName, CountryName, CustomerName, appointmentTitle});
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
            Func<DataTable> GetSortedAppointmentTable = () =>
            {
                DataTable appointmentTable = this.dataSet.Tables[ClientScheduleTableName.Appointment]!;
                return appointmentTable.AsEnumerable()
                  .OrderBy(r => r.Field<DateTime>(AppointmentColumnName.Start))
                  .ThenBy(r => r.Field<string>(AppointmentColumnName.Type)).CopyToDataTable();
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
            Dictionary<string, int> Dct_appointmentTypeOfTheMonth = new Dictionary<string, int>();
            
            if (sortedTable is null)
            {
                return;
            }
            
            foreach (DataRow row in sortedTable!.Rows)
            {
                DateTime appointmentDate = (DateTime) row[AppointmentColumnName.Start];
                string appointmentType = (string) row[AppointmentColumnName.Type];

                if (0 == currentMonth && 0 == currentYear && currentType.Equals(string.Empty))
                {
                    currentMonth = appointmentDate.Month;
                    currentYear = appointmentDate.Year;
                    currentType = appointmentType;
                }

                if (currentMonth != appointmentDate.Month || currentYear != appointmentDate.Year)
                {
                    AssiggnValReportTable(Dct_appointmentTypeOfTheMonth, new List<int>() { currentMonth, currentYear });

                    Dct_appointmentTypeOfTheMonth.Clear();

                    currentMonth = appointmentDate.Month;
                    currentYear = appointmentDate.Year;
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
            DataTable appointmentTable = this.dataSet.Tables[ClientScheduleTableName.Appointment]!;
            DataTable userTable = this.dataSet.Tables[ClientScheduleTableName.User]!;
            DataTable customerTable = this.dataSet.Tables[ClientScheduleTableName.Customer]!;

            HashSet<int> userUniqueIds = new HashSet<int>(); // to fetch unique Id sets

            foreach (DataRow row in appointmentTable.Rows)
            {
                userUniqueIds.Add((int)row[AppointmentColumnName.UserId]); // unique ids from another table foreign keys
            }

            foreach (int userId in userUniqueIds)
            {
                var query = from apptRow in appointmentTable.AsEnumerable()
                            join userRow in userTable.AsEnumerable()
                            on apptRow.Field<int>(AppointmentColumnName.UserId) equals userRow.Field<int>(UserColumnName.UserId)
                            join customerRow in customerTable.AsEnumerable()
                            on apptRow.Field<int>(AppointmentColumnName.CustomerId) equals customerRow.Field<int>(CustomerColumnName.CustomerId)
                            select new
                            {
                                UserName = userRow.Field<string>(UserColumnName.UserName),
                                CustomerName = customerRow.Field<string>(CustomerColumnName.CustomerName),
                                AppointmentTitle = apptRow.Field<string>(AppointmentColumnName.Title),
                                AppointmentDescription = apptRow.Field<string>(AppointmentColumnName.Description),
                                AppointmentLocation = apptRow.Field<string>(AppointmentColumnName.Location),
                                AppointmentStartDateTime = apptRow.Field<DateTime>(AppointmentColumnName.Start),
                                AppointmentEndDateTime = apptRow.Field<DateTime>(AppointmentColumnName.End)
                            };


                query.ToList().ForEach(item =>
                {
                    DataRow newConsultantScheduleRow = this.ReportTable.NewRow();

                    newConsultantScheduleRow[UserColumnName.UserName] = item.UserName;
                    newConsultantScheduleRow[CustomerColumnName.CustomerName] = item.CustomerName;
                    newConsultantScheduleRow[AppointmentColumnName.Title] = item.AppointmentTitle;
                    newConsultantScheduleRow[AppointmentColumnName.Description] = item.AppointmentDescription;
                    newConsultantScheduleRow[AppointmentColumnName.Location] = item.AppointmentLocation;
                    newConsultantScheduleRow[AppointmentColumnName.Start] = item.AppointmentStartDateTime;
                    newConsultantScheduleRow[AppointmentColumnName.End] = item.AppointmentEndDateTime;

                    this.ReportTable.Rows.Add(newConsultantScheduleRow);
                });
            }
        }

        private void CreateCustomerLocationPerAppointmentReport()
        {
            DataTable countryTable = this.dataSet.Tables[ClientScheduleTableName.Country]!;
            DataTable cityTable = this.dataSet.Tables[ClientScheduleTableName.City]!;
            DataTable addressTable = this.dataSet.Tables[ClientScheduleTableName.Address]!;
            DataTable customerTable = this.dataSet.Tables[ClientScheduleTableName.Customer]!;
            DataTable appointmentTable = this.dataSet.Tables[ClientScheduleTableName.Appointment]!;


            var query = from cityRow in cityTable.AsEnumerable()
                        join countryRow in countryTable.AsEnumerable()
                        on cityRow.Field<int>(CityColumnName.CountryId) equals countryRow.Field<int>(CountryColumnName.CountryId)
                        join addressRow in addressTable.AsEnumerable()
                        on cityRow.Field<int>(CityColumnName.CityId) equals addressRow.Field<int>(AddressColumnName.CityId)
                        join customerRow in customerTable.AsEnumerable()
                        on addressRow.Field<int>(AddressColumnName.AddressId) equals customerRow.Field<int>(CustomerColumnName.AddressId)
                        join appointmentRow in appointmentTable.AsEnumerable()
                        on customerRow.Field<int>(CustomerColumnName.CustomerId) equals appointmentRow.Field<int>(AppointmentColumnName.CustomerId)
                        select new
                        {
                            CityName = cityRow.Field<string>(CityColumnName.City),
                            CountryName = countryRow.Field<string>(CountryColumnName.Country),
                            CustomerName = customerRow.Field<string>(CustomerColumnName.CustomerName),
                            AppointmentTitle = appointmentRow.Field<string>(AppointmentColumnName.Title),
                        };


            query.ToList().ForEach(item =>
            {
                DataRow newCustomerLocationRow = this.ReportTable.NewRow();


                newCustomerLocationRow[CityColumnName.City] = item.CityName;
                newCustomerLocationRow[CountryColumnName.Country] = item.CountryName;
                newCustomerLocationRow[CustomerColumnName.CustomerName] = item.CustomerName;
                newCustomerLocationRow[AppointmentColumnName.Title] = item.AppointmentTitle;

                this.ReportTable.Rows.Add(newCustomerLocationRow);
            });
        }
    }
}
