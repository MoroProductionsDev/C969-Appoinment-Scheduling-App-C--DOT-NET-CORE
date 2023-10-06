using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Console_App.Data
{
    public struct Data
    {
        public Type[] types { get; set; }
        public object[] values { get; set; }
        public int instances { get; set; }

    }

    internal class DatabaseConnector : IConnectable
    {
        public event EventHandler<EventArgs> throwedExceptionEventHandler;
        public event EventHandler<DbConnectionEventArgs> ChangedStateEventHandler;
        public ConnectionState State { get; private set; }

            

        // private members
        private DbConnection _conn;
        private DbCommand _command;
        private DbException excp;
        
        private enum SQLCommandType { Select, Update, Delete };
        private Dictionary<SQLCommandType, string> SQLQueryKeywords = new Dictionary<SQLCommandType, string>()
        {
            [SQLCommandType.Select] = "SELECT",
            [SQLCommandType.Update] = "UPDATE",
            [SQLCommandType.Delete] = "DELETE"
        };

        public DatabaseConnector(ConnectionStringSettings configuration) {
            switch (configuration?.ProviderName)
            {
                case Provider.MySql:
                    _conn = new MySqlConnection(configuration.ConnectionString);
                    State = _conn.State;
                    break;
            }

            Data data = new Data();
        }

        public bool Open()
        {
            bool taskCompleted = false;
            try
            {
                if (_conn is MySqlConnection && !_conn.State.Equals(ConnectionState.Open))
                {
                    ((MySqlConnection)_conn)?.Open();

                    // Event trigger when state change.
                    ChangedStateEventHandler?.Invoke(this, new DbConnectionEventArgs(_conn));
                    taskCompleted = true;
                }
            }
            catch (MySqlException mySqlExcp)
            {
/*                throwedExceptionEventHandler?.Invoke(this, EventArgs.Empty);
                excp = mySqlExcp;
                Console.WriteLine($"{mySqlExcp}\n{mySqlExcp.Message}");*/

            } catch(InvalidOperationException invldOprtExcp)
            {
/*                throwedExceptionEventHandler?.Invoke(this, EventArgs.Empty);
                Console.WriteLine($"{invldOprtExcp}\n{invldOprtExcp.Message}");*/
            }
            return taskCompleted;
        }
        public bool Close()
        {
            bool taskCompleted = false;
            if (_conn is MySqlConnection)
            {
                ((MySqlConnection)_conn)?.Close();

                // Event trigger when state change.
                ChangedStateEventHandler?.Invoke(this, new DbConnectionEventArgs(_conn));
                taskCompleted = true;
            }

            return taskCompleted;
        }

        public IConnectable Command(string commandString)
        {
            _command = null;
            commandString = "SELECT * FROM client_schedule.user;";
            if (_conn is MySqlConnection && _conn.State.Equals(ConnectionState.Open))
            {
                _command = new MySqlCommand(commandString, ((MySqlConnection)_conn));
            }

            return this;
        }

        public IConnectable Execute()
        {
            if (_command.Connection is MySqlConnection)
            {
                if (_command is null)
                {
                    return this;
                }

                if (_command.CommandText.Contains(SQLQueryKeywords[SQLCommandType.Select]))
                {
                    Read();
                }
                else if (_command.CommandText.Contains(SQLQueryKeywords[SQLCommandType.Update]))
                {
                    Console.WriteLine();
                }
                else if (_command.CommandText.Contains(SQLQueryKeywords[SQLCommandType.Delete]))
                {
                    Console.WriteLine();
                }
            }

            return this;
        }

        private IConnectable Read()
        {
            if (_command.Connection is MySqlConnection)
            {
                var reader = _command.ExecuteReader();
                Data data = new Data();

                data.types = new Type[reader.FieldCount];
                data.values = new object[reader.FieldCount];


                while (reader.Read())
                {
                      data.instances = reader.GetValues(data.values);
                    
                      for (int i = 0; i < reader.FieldCount; ++i)
                      {
                        data.types[i] = reader.GetFieldType(i);
                      }
                }
                
                reader.Close();
            }

            return this;
        }

        public void Write()
        {
            throw new NotImplementedException();
        }

        public void stateChangedEventAction(object sender, DbConnectionEventArgs e)
        {
            // Updates the state of the connection
            State = e.Connection.State;
            Console.WriteLine($"Action {e.Connection.State}");
        }

        public void throwedExceptionEventAction(object sender, DbConnectionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
