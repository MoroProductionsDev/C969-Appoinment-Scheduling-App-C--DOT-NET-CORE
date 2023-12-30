﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Structure;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_API.Controller
{
    public static class AppController
    {
        public static void AuthenticateLogIn(AppState? appState)
        {
            appState?.Authenticate();
        }

        public static void MapDataBaseToDataSet(AppState? appState)
        {
            appState?.DbDataSet.Mapping();
        }

        public static void AddCustomerRecord(AppState? appState)
        {
            appState?.DbDataSet.Insert(ClientScheduleTableName.Customer);


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

        public static void UpdateCustomerRecord(AppState? appState)
        {
            appState?.DbDataSet.Update<string>(ClientScheduleDbSchema._dbName,  ClientScheduleTableName.Customer, CustomerColumnName.CustomerName, "Ina Prufung", "Moro Men");

            //appState.AppData.CustomerRecord.CustomerName = "Pepe";
/*            var dtRows = appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Customer].Rows.Find(9);
s
            Console.WriteLine(dtRows.ItemArray.Length);
            Console.WriteLine(dtRows.ItemArray[0]);
            Console.WriteLine(dtRows.ItemArray[1]);

            //dtRows.ItemArray[1] = "Pepe";
            appState.DbDataSet.DataSet.Tables[ClientScheduleTableName.Customer].Rows[3][1] = "Pepe";

            Console.WriteLine(dtRows.ItemArray[0]);
            Console.WriteLine(dtRows.ItemArray[1]);*/

            //Validator.DidValueChange(appState, ClientScheduleTableName.Customer);
        }

        public static void DeleteCustomerRecord(AppState? appState) {
            appState?.DbDataSet.Delete<string>(ClientScheduleTableName.Customer, CustomerColumnName.CustomerName, "Moro Men");
        }
    }
}
