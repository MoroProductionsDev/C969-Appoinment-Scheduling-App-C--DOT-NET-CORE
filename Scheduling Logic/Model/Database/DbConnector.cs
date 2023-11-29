using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_Logic.Model.Config;
using Scheduling_Logic.Model.Factory;

namespace Scheduling_Logic.Model.Database
{
    /*
     * Description: This class is a wrapper class that handles different objects to succesfully fetch, retrieve
     *              and update data from and to the database.
     *              It expects to implement the [IDatabaseConnector] properties and behaviors.
     */
    public sealed class DbConnector
    {
        private readonly DbProviderFactory dbProviderFactory;
        private readonly DbConnection dbConnection;
        private readonly DbDataAdapter dbDataAdapter;
        private bool disposedValue;

        /*
         * Description: Parameterized Constructor
         * 
         * @param       [String] connectionString   A connection string configuration for the specific
         *                                          database type.
         *                                          
         * mutation    It initialize this.dbConnection object based on the connection type.                                         
         */
        internal DbConnector(IDbConfig config)
        {
            this.dbProviderFactory = DbFactory.CreateDbProviderFactory(config);
            this.dbConnection = dbProviderFactory.CreateConnection();
            this.dbDataAdapter = dbProviderFactory.CreateDataAdapter();

            ConfigureInteralConnection(config);
            ConfigureInteralDbDataAdapter();
        }

        private void ConfigureInteralConnection(IDbConfig config)
        {
            this.dbConnection.ConnectionString = config.ConnectionString;
        }

        private void ConfigureInteralDbDataAdapter()
        {
            this.dbDataAdapter.SelectCommand = this.dbProviderFactory.CreateCommand();
            this.dbDataAdapter.SelectCommand.Connection = this.dbConnection;
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.data.dataset?view=net-7.0
        public void MapTableAndColumns(string dbName, string tableName)
        {
            this.dbDataAdapter.SelectCommand.CommandText = $"SELECT * FROM `{dbName}`.`{tableName}`;";
            ITableMapping tableMapping = dbDataAdapter.TableMappings.Add(tableName, tableName);
            IDataReader dataReader = dbDataAdapter.SelectCommand.ExecuteReader();

            for (var fieldIdx = 0; fieldIdx < dataReader.FieldCount; ++fieldIdx)
            {
                tableMapping.ColumnMappings.Add(dataReader.GetName(fieldIdx), dataReader.GetName(fieldIdx));
            }

            if (!dataReader.IsClosed)
            {
                dataReader.Close();
            }
        }

        public void FillSchema(DataSet dataSet, string tableName)
        {
            this.dbDataAdapter.FillSchema(dataSet, SchemaType.Mapped, tableName);
        }

        public void Fill(DataSet dataSet, string tableName)
        {
            this.dbDataAdapter.Fill(dataSet, tableName);
        }

        public Task FillSchemaAsync(in IDbDataAdapter dbDataAdapter, in DataSet dataSet)
        {
            TaskCompletionSource<DataTable[]> taskCompletionSource = new TaskCompletionSource<DataTable[]>();
            try
            {
                DataTable[] result = this.dbDataAdapter.FillSchema(dataSet, SchemaType.Mapped);
                taskCompletionSource.SetResult(result);
            }
            catch (Exception exception)
            {
                taskCompletionSource.SetException(exception);
            }

            return taskCompletionSource.Task;
        }

        public Task FillAsync(in IDbDataAdapter dbDataAdapter, in DataSet dataSet)
        {
            TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
            try
            {
                int result = dbDataAdapter.Fill(dataSet);
                taskCompletionSource.SetResult(result);
            }
            catch (Exception exception)
            {
                taskCompletionSource.SetException(exception);
            }

            return taskCompletionSource.Task;
        }

        public void Update(DataSet dataSet, string dbName, string tableName)
        {
            DbCommandBuilder sqlCommandBuilder = this.dbProviderFactory.CreateCommandBuilder();

            this.dbDataAdapter.SelectCommand.CommandText = $"SELECT * FROM `{dbName}`.`{tableName}`;";
            sqlCommandBuilder.DataAdapter = this.dbDataAdapter;
            sqlCommandBuilder.GetUpdateCommand();

            this.dbDataAdapter.Update(dataSet, tableName);
        }

        public void Insert(DataSet dataSet, string tableName)
        {
            DbCommandBuilder sqlCommandBuilder = this.dbProviderFactory.CreateCommandBuilder();
            
            sqlCommandBuilder.GetInsertCommand();

            this.dbDataAdapter.Update(dataSet, tableName);
        }

        /*        public void Update()
                {
                    dbDataAdapter.SelectCommand.CommandText = $"SELECT * FROM `{this.dbSchema.DbName}`.`{tableName}`;";

                    var mySqlCommand = new MySqlCommand();
                    mySqlCommand.Connection = (MySqlConnection)((DbConnector<T>)this.dbConnector).dbConnection;

                    mySqlCommand.CommandText = $"UPDATE `{this.dbSchema.DbName}`.`{tableName}` SET customerName = @custName " + "WHERE customerID = @custID;";
                    mySqlCommand.Parameters.Add("@custName", MySqlDbType.VarChar);
                    mySqlCommand.Parameters.Add("@custID", MySqlDbType.Int32);

                    mySqlCommand.Parameters["@custName"].Value = newValue;
                    mySqlCommand.Parameters["@custID"].Value = 3;

                    ((MySqlDataAdapter)dbDataAdapter).UpdateCommand = mySqlCommand;
                    Console.WriteLine(((MySqlDataAdapter)dbDataAdapter).Update(this.DataSet, tableName));
                }*/


        internal ReadOnlySpan<IDbConnection> Connection()
        {
            return new ReadOnlySpan<IDbConnection>();
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

        public ConnectionState ConnState
        {
            get => this.dbConnection.State;
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
                this.dbConnection?.Dispose();

/*                this.DbCommand = null;*/
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~DbConnector()
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