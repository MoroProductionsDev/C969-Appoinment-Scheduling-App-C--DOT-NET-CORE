using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
/*
namespace Scheduling_Console_App.Data
{
  internal class UserEntity : DbConnector
    {
*//*
        public struct UserData {
            int userId;
            string UserName;
            string password;
            bool active;
            MySqlDateTime createdDate;
            string createdBy;
            MySqlDateTime lastUpdate;
            string lastUpdateBy;
        }
        protected override MySqlDataReader Reader { get; set; }
        protected override MySqlCommand Command { get; set; }
        public UserEntity(string connectionString) : base(connectionString)
        {
            base.Connection.Open();
        }

        public void selectUser()
        {
            string commandString = "SELECT * FROM client_schedule.user;";
            Command = new MySqlCommand(commandString, base.Connection);
            
            Reader = Command.ExecuteReader();
            populateEntity();
            

*//*            Console.WriteLine(Reader.GetTextReader(4).ReadLine());*//*
        }

        private void populateEntity<T>()
        {
            List<string> tableData = new List<string>();
            while (Reader.Read())
            {

                for (int i = 0; i < Reader.FieldCount; ++i)
                {
                    tableData.Add(Reader.GetString(i));
*//*                    typeof(UserData).GetRuntimeFields();

                    fieldInfo[i].SetValueDirect(fieldInfo[i].FieldType, Reader.GetTextReader(i).ReadLine());
                    // Console.WriteLine(Reader.GetTextReader(i).ReadLine());
                    //Console.WriteLine(fieldInfo[i]);*//*
                }
            }
*//*
            foreach(var use in user)
            {
                Console.WriteLine(use);
            }*//*

            Reader.Close();
        }
    }*//*
}
*/