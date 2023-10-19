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
    internal class ConsoleOutput
    {
        /*
         * Description: It shows the [DataTable] reference object in a table formated way.
         */
        internal void ShowTable(DataTable dataTable)
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
    }
}
