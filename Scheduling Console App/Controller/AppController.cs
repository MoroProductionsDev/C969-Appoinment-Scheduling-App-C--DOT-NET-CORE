using Scheduling_Console_App.Controller.State;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Console_App.Controller.Validate;
using static Scheduling_Library.Model.Structure.ClientScheduleDbSchema;
using Scheduling_Library.Model.Structure;
using System.Net.PeerToPeer;
using System.Collections;

namespace Scheduling_Console_App.Controller
{
    internal static class AppController
    {
        internal static void AuthenticateLogIn(in AppState appState)
        {
            Validator.CheckCredentials(in appState);
        }

        internal static void MapDataBaseToDataSet(in AppState appState)
        {
            appState.DbDataSet.Mapping();
        }

        internal static void AddCustomerRecord(in AppState appState)
        {
            appState.DbDataSet.Insert(ClientScheduleTableName.Customer);


            /*            appState.AppData.CustomerRecord.CustomerName = "Pedro";
                        appState.AppData.AddressRecord.Address = "4444 Sen Andrin";
                        appState.AppData.AddressRecord.Phone = "(777)-777-7777";

                        DataTable customerTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Customer];
                        DataTable addressTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Address];
                        DataTable cityTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.City];
                        DataTable countryTable = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Country];
                        *//*
                                    IEnumerable<DataRow> query =
                                        from customer in customerTable.AsEnumerable()
                                        join address in addressTable.AsEnumerable()
                                        on customer.Field<int>(CustomerColumnName.AddressId)
                                        equals address.Field<int>(AddressColumnName.AddressId)
                                        into CustomerAddress;*//*

                        var customerAddressQuery =
                            customerTable.AsEnumerable().Join(addressTable.AsEnumerable(),
                            customer => customer.Field<Int32>(CustomerColumnName.AddressId),
                            address => address.Field<Int32>(AddressColumnName.AddressId),
                            (customer, address) => new
                            {
                                CustomerName = customer.Field<String>(CustomerColumnName.CustomerName),
                                PhoneNumber = address.Field<String>(AddressColumnName.Phone),
                                Address = address.Field<String>(AddressColumnName.Address)
                            });

                        var cityCountryQuery =
                            cityTable.AsEnumerable().Join(countryTable.AsEnumerable(),
                            city => city.Field<Int32>(CityColumnName.CountryId),
                            country => country.Field<Int32>(CountryColumnName.CountryId),
                            (city, country) => new
                            {
                                CityName = city.Field<String>(CityColumnName.City),
                                CountryName = country.Field<String>(CountryColumnName.Country),
                            });

                        foreach (var custAddrss in customerAddressQuery)
                        {
                            Console.WriteLine("CustomerName: {0} "
                                            + "PhoneNumber: {1} "
                                            + "Address: {2} ",
                                custAddrss.CustomerName,
                                custAddrss.PhoneNumber,
                                custAddrss.Address);
                        }


                        foreach (var citCount in cityCountryQuery)
                        {
                            Console.WriteLine("CityName: {0} "
                                            + "CountryName: {1} ",
                                citCount.CityName,
                                citCount.CountryName);
                        }*/
            /*
                    IEnumerable<DataRow> userNameAndPw =
                    userQuery.Where((userRow) =>
                    {
                        return
                        userRow.Field<string>(UserColumnName.UserName) == userName &&
                        userRow.Field<string>(UserColumnName.Password) == password;
                    });*/
        }

        internal static void UpdateCustomerRecord(in AppState appState)
        {
            appState.DbDataSet.Update<string>(ClientScheduleTableName.Customer, CustomerColumnName.CustomerName, "Ina Prufung", "Moro Men");

            //appState.AppData.CustomerRecord.CustomerName = "Pepe";
/*            var dtRows = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Customer].Rows.Find(9);

            Console.WriteLine(dtRows.ItemArray.Length);
            Console.WriteLine(dtRows.ItemArray[0]);
            Console.WriteLine(dtRows.ItemArray[1]);

            //dtRows.ItemArray[1] = "Pepe";
            appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Customer].Rows[3][1] = "Pepe";

            Console.WriteLine(dtRows.ItemArray[0]);
            Console.WriteLine(dtRows.ItemArray[1]);*/

            //Validator.DidValueChange(appState, ClientScheduleTableName.Customer);
        }

        internal static void DeleteCustomerRecord(in AppState appState) {
            appState.DbDataSet.Delete<string>(ClientScheduleTableName.Customer, CustomerColumnName.CustomerName, "Moro Men");
        }
    }
}
