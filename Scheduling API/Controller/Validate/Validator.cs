using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Data;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_API.Controller.Validate
{
    internal static class Validator
    {
        public static bool CheckCredentials(AppState appState)
        {
            DataTable userTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.User];
            string userName = appState.AppData.UserRecord.UserName;
            string password = appState.AppData.UserRecord.Password;

            IEnumerable<DataRow> userQuery =
            from userRow in userTable.AsEnumerable()
            select userRow;

            IEnumerable<DataRow> userNameAndPw =
            userQuery.Where((userRow) => {
                return
                userRow.Field<string>(UserColumnName.UserName) == userName &&
                userRow.Field<string>(UserColumnName.Password) == password;
            });

            return userNameAndPw.ToList().Count == 1;
        }
/*
        internal static void DidValueChange(in AppState appState, in String tableName)
        {
            DataRow[] dtRows = null;
            switch (tableName)
            {
                case ClientScheduleTableName.Customer:
                    string customerName = appState.AppData.CustomerRecord.CustomerName;
                    Console.WriteLine(customerName);
                    dtRows = appState.DbDataSet.DataSet.Tables[tableName].Select($"{CustomerColumnName.CustomerName} = {CustomerColumnName.CustomerName}");
                    break;
                case ClientScheduleTableName.Address:
                    string phoneNumber = appState.AppData.AddressRecord.Phone;
                    dtRows = appState.DbDataSet.DataSet.Tables[tableName].Select($"{AddressColumnName.Phone} = {phoneNumber}");
                    break;
                case ClientScheduleTableName.City:
                    string cityName = appState.AppData.CityRecord.City;
                    dtRows = appState.DbDataSet.DataSet.Tables[tableName].Select($"{CityColumnName.City} = {cityName}");
                    break;
                case ClientScheduleTableName.Country:
                    string countryName = appState.AppData.CustomerRecord.CustomerName;
                    dtRows = appState.DbDataSet.DataSet.Tables[tableName].Select($"{CountryColumnName.Country} = {countryName}");
                    break;
            }

            Console.WriteLine(dtRows.Count());
        }

        internal static void ff(in AppState appState)
        {
            appState.AppData.CustomerRecord.CustomerName = "Pedro";
            appState.AppData.AddressRecord.Address = "4444 Sen Andrin";
            appState.AppData.AddressRecord.Phone = "(777)-777-7777";

            DataTable customerTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Customer];
            DataTable addressTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Address];
            DataTable cityTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.City];
            DataTable countryTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Country];

            var query = (
                from customer in customerTable.AsEnumerable()
*//*                from address in addressTable.AsEnumerable()
                from city in cityTable.AsEnumerable()
                from country in countryTable.AsEnumerable()*//*
                where customer.Field<Int32>(CustomerColumnName.CustomerId) == 2
                select new
                {
                    CustomerId = customer.Field<Int32>(CustomerColumnName.CustomerId),
                    CustomerName = customer.Field<String>(CustomerColumnName.CustomerName),
*//*                    PhoneNumber = address.Field<String>(AddressColumnName.Phone),
                    Address = address.Field<String>(AddressColumnName.Address),
                    CityName = city.Field<String>(CityColumnName.City),
                    CountryName = country.Field<String>(CountryColumnName.Country),*//*
                });

            Console.WriteLine(query.Count());

            foreach (var row in query)
            {
                Console.WriteLine("CustomerId: {0} "
                                + "CustomerName: {1} ",
*//*                                + "PhoneNumber: {2} "
                                + "Address: {3} "
                                + "Address: {4} "
                                + "CityName: {5} "
                                + "CountryName: {6} ",*//*
                    row.CustomerId,
                    row.CustomerName);
*//*                    row.PhoneNumber,
                    row.Address,
                    row.CityName,
                    row.CountryName);*//*
            }
            Console.WriteLine("_");
        }*/
    }
}
