using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scheduling_Library.Model.database
{
    /*
     * Description: This class is a wrapper class that handles different objects to succesfully fetch, retrieve
     *              and update data from and to the database.
     *              It expects to implement the [IDatabaseConnector] properties and behaviors.
     */
    internal sealed class MySqlConnector : IDbConnector
    {
        private readonly IDbConnection dbConnection;

        private IDbCommand _dbCommand;
        private IDbDataAdapter _dbDataAdapter;
        private bool disposedValue;

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
        internal MySqlConnector(in Type connectionType, in String connectionString)
        {
            this.dbConnection = DbInstance.CreateDbConnection(connectionType, connectionString);
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

        public ConnectionState ConnectionState
        {
            get => this.dbConnection.State;
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
        public IDbCommand CreateDbCommand(in String commandText)
        {
            /*            this._dbCommand?.Dispose();
                        this._dbCommand = null;*/

            IDbCommand dbCommand = null;

            if (this.dbConnection.State.Equals(ConnectionState.Open))
            {
                dbCommand = DbInstance.CreateDbCommand(commandText, this.dbConnection);
            } else
            {
                throw new Exception();
            }

            return dbCommand;
        }

        public void CreateDbDataAdapter()
        {
            /*IDbDataAdapter dbDataAdapter = null;*/

            if (this.dbConnection.State.Equals(ConnectionState.Open))
            {
                this._dbDataAdapter = DbInstance.CreateDbDataAdapter( this.dbConnection);
            }
            // Excpt
        }

        public IDbDataAdapter DbDataAdapter
        {
            get
            {
                if (null == _dbDataAdapter)
                {
                    CreateDbDataAdapter();
                }

                return _dbDataAdapter;
            }

            private set
            {
                _dbDataAdapter = value;
            }
        }


        /*
         * Description: It execute the database command based on the command text.
         */
        public void Execute()
        {
            if (this._dbCommand != null)
            {
                if (this._dbCommand.CommandText.ToUpper().Contains(DmlSqlKeyword.Select[0]))
                {
                    this.Read();
                }
                else if (this._dbCommand.CommandText.ToUpper().Contains(DmlSqlKeyword.Update[0]))
                {
                    /*Write();*/
                }
                else if (this._dbCommand.CommandText.ToUpper().Contains(DmlSqlKeyword.Delete[0]))
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

            if (this._dbCommand != null)
            {
                this.DbDataReader = this._dbCommand.ExecuteReader();
            }

            return (this.DbDataReader != null);
        }

        private int Write()
        {
            int affectedRows = 0;
            if (this._dbCommand != null)
            {

                this._dbCommand.Transaction = ((MySqlConnection) (this._dbCommand.Connection)).BeginTransaction();
                try
                {
                    affectedRows = this._dbCommand.ExecuteNonQuery();
                    this._dbCommand.Transaction.Commit();                    
                }
                catch(Exception)
                {
                    this._dbCommand.Transaction.Rollback();
                }
            }
            return affectedRows;
        }


        /*
         * Description: Disposes different properties and fields of managed and unmanaged data.
         * 
         * @param:      [Boolean] disposing     N/A
         */
        public void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                this.DbDataReader?.Close();
                this.dbConnection?.Close();
                
                this.DbDataReader?.Dispose();
                this._dbCommand?.Dispose();
                this.dbConnection?.Dispose();

                this.DbDataReader = null;
                this._dbCommand = null;
/*                this.DbCommand = null;*/
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