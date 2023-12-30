using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Logic.Model.Structure;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Database;

namespace Scheduling_Logic.Model.Factory
{
    public static class DataInstance
    {
        /*
         * Description: This static class applies the S.O.L.I.[D] specially the "D" for "Dependency Inversion".
         *              It will return instances related to data table that will be use to store information for the database object fetches
         *              and updates.
         *              
         *              S.O.L.I.D. stands for: 
         *              S – Single-Responsibility Principle. 
         *              O – Open-Closed Principle. (Open for extension but close to modifications)
         *              L – Liskov Substitution Principle. 
         *              I – Interface Segregation Principle. 
         *              D – Dependency Inversion
         */


        /*
         * Description: This function creates and returns a new [DbDataSet] object base on the
         *              the reference [IDbConnector] and the [DbSchema] provided..
         * 
         * @param       [IDbConnector] dbConnector       It carries a [IDbconnector] instance with data about the database connection.
         *              [DbSchema] dbSchema       It has information about the database structure.
         *                                      
         * @return      A instance of a [DbDataSet].
         */
        public static DbDataSet CreateDbDataTable(DbConnector? dbConnector, DbSchema? dbSchema)
        {
             return new DbDataSet(dbConnector, dbSchema);
        }

        internal static DataSet createDataSet(string dtSetName) { 
            return new DataSet(dtSetName); 
        }
    }
}
