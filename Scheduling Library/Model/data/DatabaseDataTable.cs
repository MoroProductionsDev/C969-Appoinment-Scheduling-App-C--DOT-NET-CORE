using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Scheduling_Library
{
    /*
     * Description: This class is use to store the returned fetched value that the
     *              the database reader returns when it succesfully fetch data.
     */

    /* References:
     * Reference from the Microsoft's page on how to use the [DataTable], [DataRows], [DataColumns], and [Data Sets].
     * https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-7.0 
     * How to use switch statements on system's type.
     * https://stackoverflow.com/questions/43080505/how-to-switch-on-system-type
     */
    public class DatabaseDataTable
    {
        private readonly IDataReader reader;  // Reference to database data reader
        public DataTable DbDataTable { get; } // Database data table

        /*
         * Description: Parameterized Constructor
         * 
         * @param       [IDataReader] reader        It carries a reference to database data reader returned from it.
         *              [DataTable] dataTable       It carried a reference to data table use to store the fetched data provided
         *                                          by the database data reader.
         *                                          
         * mutation     It initialize this.DbDataTable property/object based on the [DataTable] reference passed.
         *              It initialize this.reader private field/object based on the [IDataReader] reference passed.  
         */
        public DatabaseDataTable(IDataReader reader, DataTable dataTable)
        {
            this.DbDataTable = dataTable;
            this.reader = reader;
        }

        /*
         * Description: It creates the [DataTable] that stores data provided by the objects' [IDataReader].
         */
        public void Create()
        {
            DataColumn[] columns = this.CreateDataColumns();

            this.DbDataTable.Columns.AddRange(columns);

            while (this.reader.Read()) 
            {
                DataRow row = DbDataTable.NewRow();
                for (int i = 0; i < this.reader.FieldCount; ++i)
                {
                    string columnName = DbDataTable.Columns[i].ColumnName;
                    Type columnType = DbDataTable.Columns[i].DataType;

                    var valueAsStr = this.reader.GetString(i);

                    if (typeof(String) != columnType)
                    {
                        row[columnName] = this.ParseData(columnType, valueAsStr);
                    } else
                    {
                        row[columnName] = valueAsStr;
                    }
                }
                this.DbDataTable.Rows.Add(row);
            }

            this.DbDataTable.Load(this.reader); // metadata including the table's name
            this.DbDataTable.PrimaryKey = new DataColumn[] { this.DbDataTable.Columns[0] }; // primary key as an array of [DataColumn]
                                                                                            // allowing composite primary keys to be made.
        }

        /*
         * Description: It creates a [DataColumn] that stores each column provided by the reader's then adds it to the [DataTable] object.
         */
        private DataColumn[] CreateDataColumns()
        {
            DataColumn[] columns = new DataColumn[this.reader.FieldCount];
            for (int i = 0; i < this.reader.FieldCount; ++i)
            {
                var column= new DataColumn();
                column.DataType = ConvertSqlType(this.reader.GetDataTypeName(i));
                column.ColumnName = this.reader.GetName(i);
                column.ReadOnly = true;
                column.AllowDBNull = false;
                column.Unique = (0 == i) ? true: false; // primary key

                columns[i] = column; // add the single column to the columns array
            }

            return columns;
        }

        /*
         * Description: It returns a System.Typ based on the provided database type return by the reader, stored as a string.
         */
        private Type ConvertSqlType(string dbTypeName)
        {
            Type type = null;
            switch(dbTypeName)
            {
                case SqlTypeName.TinyInt:
                    type = typeof(Boolean);
                    break;
                case SqlTypeName.Int:
                    type = typeof(Int32);
                    break;
                case SqlTypeName.String:
                    type = typeof(String);
                    break;
                case SqlTypeName.DateTime:

                case SqlTypeName.TimeStamp:
                    type = typeof(DateTime);
                    break;
            }

            return type;
        }

        /*
         * Description: It parses the string provided based on the column type.
         */ 
        private object ParseData(Type actualColumnType, string dataStrValue)
        {
            object value = null;

            switch(actualColumnType)
            {
                case Type _ when typeof(Int32) == actualColumnType:
                    value = Int32.Parse(dataStrValue);
                    break;
                case Type _ when typeof(Boolean) == actualColumnType:
                    value = Boolean.Parse(dataStrValue);
                    break;
                case Type _ when typeof(DateTime) == actualColumnType:
                    value = DateTime.Parse(dataStrValue);
                    break;
            }

            return value;
        }
    }
}
