using Scheduling_Library.Model.structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Library
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
         * Description: This function creates and returns a new [DataTable] object.
         *                                      
         * @return      A instance of a [DataTable].
         */
        public static DataTable createDataTable()
        {
            return new DataTable();
        }

        public static DataSet createDataSet() { 
            return new DataSet(); 
        }

        /*
         * Description: This function creates and returns a new [DatabaseDataTable] object base on the
         *              the reference [IDataReader] and the [DataTable] provided..
         * 
         * @param       [IDataReader] reader        It carries a referemce [IDataReader] that will be use catch the fetched data.
         *              [DataTable] dataTable       It carries a [DataTable] that will be use to stored, updated data from and to the user.
         *                                      
         * @return      A instance of a [DatabaseDataTable].
         */
        public static DatabaseDataTable CreateDbDataTable(in IDatabaseConnector databaseConnector, in DatabaseSchema schema)
        {
            return new DatabaseDataTable(in databaseConnector, in schema);
        }
    }
}
