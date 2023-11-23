using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Scheduling_Library.Model.Factory;
using Scheduling_Library.Model.Structure;
using Scheduling_Library.Model.Database;
using static Scheduling_Library.Model.Structure.ClientScheduleDbSchema;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace Scheduling_Library.Model.Data
{
    /*
     * Description: This class is use to store the returned fetched value that the
     *              the database connector returns when it succesfully fetch data, as well
     *              as insert update or delete data from the database base on its data set.
     */

    /* References:
     * Reference from the Microsoft's page on how to use the [DataTable], [DataRows], [DataColumns], and [Data Sets].
     * https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-7.0 
     */
    public sealed class DbDataSet
    {
        private readonly IDbConnector dbConnector;
        private readonly DbSchema dbSchema;
        private bool mapped;
        public DataSet DataSet { get; private set; }

        /*
         * Description: Parameterized Constructor
         * 
         * @param       [IDbConnector] dbConnector      It carries a reference to database connector that is the relation with the datatbase.
         *              [DbSchema] dbSchema             It carried a reference to database schema use to structure correctly the data set.
         *                                          
         * mutation     It initialize this.dbConnector private field/object based on the [IDbConnector] reference passed.
         *              It initialize this.dbSchema private field/object based on the [IDataReader] reference passed.
         *              It initialize this.dataSet private field/object based on the [DbSchema] reference passed.
         */
        public DbDataSet(in IDbConnector dbConnector, in DbSchema dbSchema)
        {
            this.dbConnector = dbConnector;
            this.dbSchema = dbSchema;
            this.DataSet = DataInstance.createDataSet(dbSchema.DbName);
            this.mapped= false;
        }
        /*
         * 
        */

        public void Mapping()
        {
            if (this.mapped)
            {
                return;
            }

            int tableCount = this.dbSchema.TableNamesIndented.Count;
            IDbDataAdapter dbDataAdapter = this.dbConnector.DbDtAdapter;


            dbDataAdapter.SelectCommand = this.dbConnector.CreateDbCommand(String.Empty);

            for (var tableIndex = 0; tableIndex < tableCount; ++tableIndex)
            {
                string tableName = this.dbSchema.TableNamesIndented[tableIndex];

                MapTableAndColumns(in dbDataAdapter, in tableName);
                AddTableToDataSet(in tableName);
                FillSchema(in dbDataAdapter, in tableName);
                UpdatePrimaryKeyContraint(in tableName);
                Fill(in dbDataAdapter, in tableName);
            }

            for (var tableIndex = 0; tableIndex < tableCount; ++tableIndex)
            {

                string tableName = this.dbSchema.TableNamesIndented[tableIndex];

                DataColumn pkColumn = this.DataSet.Tables[tableName].PrimaryKey[0];
                ChangePKAttributes(pkColumn, tableName);
                CreatePrimaryAndForeingKeyRelation(in tableName);

/*
                int columnCount = this.DataSet.Tables[tableName].Columns.Count;

                for (var colIdx = columnCount - 4; colIdx < columnCount; ++colIdx)
                {
                    DataColumn column = this.DataSet.Tables[tableName].Columns[colIdx];

                    if (column.DataType == typeof(DateTime))
                    {
                        
                        SetDefaultVal<DateTime>(column);
                    }
                    else
                    {
                        SetDefaultVal<String>(column);
                    }
                }*/

                //this.DataSet.Tables[tableName].AcceptChanges();
            }

            //this.DataSet.AcceptChanges();
            mapped = true;
        }

        public void Update<T>(String tableName, String ColumnName, T currentValue, T newValue)
        {
            IDbDataAdapter dbDataAdapter = this.dbConnector.DbDtAdapter;
            IQueryable<DataRow> query = (from row in this.DataSet.Tables[tableName].AsEnumerable()
                        //where row.Field<T>(ColumnName) == value
                        where EqualityComparer<T>.Default.Equals(row.Field<T>(ColumnName), currentValue)
                        select row).AsQueryable();

            if (query.Count() > 0)
            {
                query.First()[ColumnName] = newValue;
                //this.DataSet.Tables[tableName].AcceptChanges();

                dbDataAdapter.SelectCommand.CommandText = $"SELECT * FROM `{this.dbSchema.DbName}`.`{tableName}`;";

                var mySqlCommand = new MySqlCommand();
                mySqlCommand.Connection = (MySqlConnection)((MySqlConnector)this.dbConnector).dbConnection;

                mySqlCommand.CommandText = $"UPDATE `{this.dbSchema.DbName}`.`{tableName}` SET customerName = @custName " + "WHERE customerID = @custID;";
                mySqlCommand.Parameters.Add("@custName", MySqlDbType.VarChar);
                mySqlCommand.Parameters.Add("@custID", MySqlDbType.Int32);

                mySqlCommand.Parameters["@custName"].Value = newValue;
                mySqlCommand.Parameters["@custID"].Value = 3;

                ((MySqlDataAdapter)dbDataAdapter).UpdateCommand = mySqlCommand;
                Console.WriteLine(((MySqlDataAdapter) dbDataAdapter).Update(this.DataSet, tableName));
            }
        }

        public void Delete<T>(String tableName, String ColumnName, T currentValue)
        {
            IDbDataAdapter dbDataAdapter = this.dbConnector.DbDtAdapter;
            IQueryable<DataRow> query = (from row in this.DataSet.Tables[tableName].AsEnumerable()
                                             //where row.Field<T>(ColumnName) == value
                                         where EqualityComparer<T>.Default.Equals(row.Field<T>(ColumnName), currentValue)
                                         select row).AsQueryable();

            if (query.Count() > 0)
            {
                query.First().Delete();
                this.DataSet.Tables[tableName].AcceptChanges();

                

                //SqlCommandBuilder builder = new SqlCommandBuilder(dbDataAdapter);
                //adapter.UpdateCommand = builder.GetUpdateCommand();
                dbDataAdapter.Update(this.DataSet);
                this.DataSet.AcceptChanges();
            }
        }

        public void Insert(String tableName)
        {
            DataRow newRow = this.DataSet.Tables[tableName].NewRow();

            newRow["customerName"] = "Peppe";

            this.DataSet.Tables[tableName].Rows.Add(newRow);
        }


        // https://learn.microsoft.com/en-us/dotnet/api/system.data.dataset?view=net-7.0
        private void MapTableAndColumns(in IDbDataAdapter dbDataAdapter, in String tableName)
        {
            dbDataAdapter.SelectCommand.CommandText = $"SELECT * FROM `{this.dbSchema.DbName}`.`{tableName}`;"; ;
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

        private void AddTableToDataSet(in String tableName)
        {
            this.DataSet.Tables.Add(tableName);
        }

        private void FillSchema(in IDbDataAdapter dbDataAdapter, in String tableName)
        {
            ((DbDataAdapter)dbDataAdapter).FillSchema(this.DataSet, SchemaType.Mapped, tableName);
        }

        private void Fill(in IDbDataAdapter dbDataAdapter, in String tableName)
        {
            ((DbDataAdapter)dbDataAdapter).Fill(this.DataSet, tableName);
        }

        private void UpdatePrimaryKeyContraint(in String tableName)
        {
            this.DataSet.Tables[tableName].Constraints[0].ConstraintName = $"{this.DataSet.Tables[tableName]}_PK";
        }

        private void CreatePrimaryAndForeingKeyRelation(in String tableName)
        {
            int fkKeyCount = this.dbSchema.ForeignKeysNames[tableName].Length;

            for (var fkIdx = 0; fkIdx < fkKeyCount; ++fkIdx)
            {
                string pkTableName = this.dbSchema.FKTablesNames[tableName][fkIdx];
                string fkColumnName = this.dbSchema.ForeignKeysNames[tableName][fkIdx];  

                if (String.Empty != pkTableName)
                {
                    DataColumn primaryKey = this.DataSet.Tables[pkTableName].PrimaryKey[0];
                    DataColumn foreignKey = this.DataSet.Tables[tableName].Columns[fkColumnName];

                    foreignKey.ReadOnly = true;

                    // Create a DataRelation to link the two tables.
                    this.DataSet.Relations.Add(new DataRelation($"{tableName}_FK-{fkColumnName}", primaryKey, foreignKey));
                }
            }
        }

        private void ChangePKAttributes(DataColumn pkColumn, in String tableName)
        {
            int rowCount = this.DataSet.Tables[tableName].Rows.Count;
            string keyColumnName = pkColumn.ColumnName;
            int autoIncrementSeed = (int)this.DataSet.Tables[tableName].Rows[rowCount - 1][keyColumnName];

            pkColumn.AutoIncrementSeed = autoIncrementSeed + 1;
            pkColumn.Unique = true;
            pkColumn.ReadOnly = true;
        }

        private void SetDefaultVal<T>(DataColumn column)
        {
            if (typeof(T) == typeof(DateTime))
            {
                column.DefaultValue = DateTime.UtcNow.ToString();
            } else
            {
                column.DefaultValue = this.DataSet.Tables["user"].Rows[0]["userName"];
            }
        }

        private Task FillDataAsync(in IDbDataAdapter dbDataAdapter, in DataSet dataSet)
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

        private Task SetSchemaAsync(in IDbDataAdapter dbDataAdapter, in DataSet dataSet)
        {
            TaskCompletionSource<DataTable[]> taskCompletionSource = new TaskCompletionSource<DataTable[]>();
            try
            {
                DataTable[] result = dbDataAdapter.FillSchema(dataSet, SchemaType.Mapped);
                taskCompletionSource.SetResult(result);
            }
            catch (Exception exception)
            {
                taskCompletionSource.SetException(exception);
            }

            return taskCompletionSource.Task;
        }
    }
}
