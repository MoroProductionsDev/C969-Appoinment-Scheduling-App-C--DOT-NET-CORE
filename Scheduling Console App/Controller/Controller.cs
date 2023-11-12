using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Scheduling_Console_App.Application;

namespace Scheduling_Console_App
{
    internal static class Controller
    {
        static void AddCustomerRecord()
        {

        }

        static void UpdateCustomerRecord()
        {

        }
        static void DeleteCustomerRecord() { 
        
        }

      /*  public static string CreateSelectAllCommandString(SqlStringComponent component)
        {
            string commandStr = $"SELECT * " +
                $"FROM `{component.DatabaseName}`.`{component.TableName}`";

            return commandStr;
        }

        public static string CreateInsertCommandString(SqlStringComponent component, FieldInfo[] fieldInfo)
        {
            string commandStr = $"INSERT INTO `{component.DatabaseName}`.`{component.TableName}`\n(";

            for (int i = 0; i < fieldInfo.Length; ++i)
            {
                if (fieldInfo.Length - 1 != i)
                {
                    commandStr += $"`{fieldInfo[i].GetValue(fieldInfo[i])}`, ";
                }
                else
                {
                    commandStr += $"`{fieldInfo[i].GetValue(fieldInfo[i])}`)\n";
                }
            }

            commandStr += $"VALUES\n(";

            for (int i = 0; i < fieldInfo.Length; ++i)
            {
                if (fieldInfo.Length - 1 != i)
                {
                    commandStr += $"`@{fieldInfo[i].GetValue(fieldInfo[i])}`, ";
                }
                else
                {
                    commandStr += $"`@{fieldInfo[i].GetValue(fieldInfo[i])}`)\n";
                }
            }

            return commandStr;
        } */
    }
}
