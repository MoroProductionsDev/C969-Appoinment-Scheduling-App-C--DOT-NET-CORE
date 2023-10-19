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

namespace Scheduling_Library
{
    /*
     * Description: This class is a wrapper class that handles different objects to succesfully fetch, retrieve
     *              and update data from and to the database.
     *              It expects to implement the [IDatabaseConnector] properties and behaviors.
     */
    internal class MySqlConnector : IDatabaseConnector
    {
        private readonly IDbConnection dbConnection;
        private IDbCommand dbCommand;
        private bool disposedValue;

        // DataReader that stores the fetched data returned from the database.
        public IDataReader DbDataReader { get; private set; }

        /*
         * Description: Parameterized Constructor
         * 
         * @param       [Type] connectionType       It carries a IDbConnection (Type) that will be use to create the
         *                                          right object type.
         *              [String] connectionString   A connection string configuration for the specific
         *                                          database type.
         *                                          
         * mutation    It initialize this.dbConnection object based on the connection type.                                         
         */
        internal MySqlConnector(Type connectionType, string connectionString)
        {
            this.dbConnection = DatabaseInstance.CreateDbConnection(connectionType, connectionString);
        }

        /*
         * Description: It attempts to open the connection if the object is not null and if the connection is not open already.
         */
        public void OpenConnection()
        {
            if (dbConnection != null && !dbConnection.State.Equals(ConnectionState.Open))
            {
                this.dbConnection.Open();
                // Event trigger when state change.
                /*ChangedStateEventHandler?.Invoke(this, new DbConnectionEventArgs(_conn));*/
            }
        }

        /*
        * Description: It closes the connection.
        */
        public void CloseConnection()
        {
            this.dbConnection?.Close();
            /* ChangedStateEventHandler?.Invoke(this, new DbConnectionEventArgs(_conn));*/
        }

        /*
         * Description: It attempts to create a database command based on the command string provided.
         * 
         * @param       [String] commandText    It carries the command text that should be created and executed.
         * 
         * mutation     It initialize this.dbCommand object based on the connection type and the command string provided.
         * 
         * @return      This instance of the class. (Pipeline)
         */
        public IDatabaseConnector CreateCommand(string commandText)
        {
            this.dbCommand?.Dispose();
            this.dbCommand = null;

            if (this.dbConnection != null && this.dbConnection.State.Equals(ConnectionState.Open))
            {
                this.dbCommand = DatabaseInstance.CreateDbCommand(typeof(MySqlConnection), commandText);
                this.dbCommand.Connection = dbConnection;
            }
            return this;
        }

        /*
         * Description: It execute the database command based on the command text.
         */
        public void Execute()
        {
            if (this.dbCommand != null)
            {
                if (this.dbCommand.CommandText.ToUpper().Contains(SqlQueryKeyword.Select))
                {
                    this.Read();
                }
                else if (this.dbCommand.CommandText.ToUpper().Contains(SqlQueryKeyword.Update))
                {
                    /*Write();*/
                }
                else if (this.dbCommand.CommandText.ToUpper().Contains(SqlQueryKeyword.Delete))
                {
                    /*Write();*/
                }
            }
        }

        /*
         * Description: It attempts to read/fetch the data from the database command provided stored on the class.
         * 
         * @return:         Returns true if the data reader is not null.  
         */
        private bool Read()
        {
            this.DbDataReader = null;

            if (this.dbCommand != null)
            {
                this.DbDataReader = dbCommand.ExecuteReader();
            }

            return (this.DbDataReader != null);
        }


        /*
         * Description: Disposes different properties and fields of managed and unmanaged data.
         * 
         * @param:      [Boolean] disposing     N/A
         */
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                this.DbDataReader.Close();
                this.dbConnection.Close();
                
                this.DbDataReader.Dispose();
                this.dbCommand.Dispose();
                this.dbConnection.Dispose();

                this.DbDataReader = null;
                this.dbCommand = null;
                this.dbCommand = null;
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~MySqlConnector()
        {
               // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
               this.Dispose(disposing: false);
        }

        /*
         * Description: This method will be use by the gargabe collector.
         */
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}