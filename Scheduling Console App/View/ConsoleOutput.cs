using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Console_App
{
    /*
     * Description: This class is use to output related application data to the console.
     */
    internal static class ConsoleOutput
    {
        /*
         * Description: It shows the [DataTable] reference object in a table formated way.
         */
        internal static void ShowTable(DataTable dataTable)
        {
            Console.WriteLine(dataTable.TableName);

            foreach (DataColumn col in dataTable.Columns)
            {
                Console.Write("{0,-14}", col.ColumnName);
            }
            Console.WriteLine();

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn col in dataTable.Columns)
                {
                    if (col.DataType.Equals(typeof(DateTime)))
                        Console.Write("{0,-14:d}", row[col]);
                    else if (col.DataType.Equals(typeof(Decimal)))
                        Console.Write("{0,-14:C}", row[col]);
                    else
                        Console.Write("{0,-14}", row[col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        //
        internal static void WriteLine(string actionMsg)
        {
            Console.WriteLine(actionMsg);
        }

        internal static void ShowDataColumnInfo(in DataColumnCollection dataColumns)
        {
            foreach (DataColumn dtColumn in dataColumns)
            {
                Console.WriteLine($"   Column Name: {dtColumn.ColumnName}");
                Console.WriteLine($" Default Value: {dtColumn.DefaultValue}");
                Console.WriteLine($"     Data Type: {dtColumn.DataType}");
                Console.WriteLine($"        Unique: {dtColumn.Unique}");
                Console.WriteLine($" Allow Db Null: {dtColumn.AllowDBNull}");
                Console.WriteLine($"Column Mapping: {dtColumn.ColumnMapping}");
            }
            Console.WriteLine();
        }
    }
}
