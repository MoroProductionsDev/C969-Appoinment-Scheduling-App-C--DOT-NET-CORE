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
        }
        /*
         * 
         */
        public void Populate()
        {
            Stopwatch stopwatch = new Stopwatch();

            //stopwatch.Start();
            Mapping();
            // Mapping();
            //stopwatch.Stop();

            //Console.WriteLine($"nanoseconds: {String.Format("{0:n0}", stopwatch.ElapsedMilliseconds * 1000000)}");
        }

        private void Mapping()
        {
            int tableCount = this.dbSchema.TableNames.Count;
            IDbDataAdapter dbDataAdapter = this.dbConnector.DbDtAdapter;


            dbDataAdapter.SelectCommand = this.dbConnector.CreateDbCommand(String.Empty);

            for (var tableIndex = 0; tableIndex < tableCount; ++tableIndex)
            {
                string tableName = this.dbSchema.TableNames[tableIndex];

                MapTableAndColumns(in dbDataAdapter, in tableName);
                AddTableToDataSet(in tableName);
                FillSchema(in dbDataAdapter, in tableName);
                UpdatePrimaryKeyContraint(tableIndex);
                Fill(in dbDataAdapter, in tableName);
            }

            for (var tableIndex = 0; tableIndex < tableCount; ++tableIndex)
            {
                CreatePrimaryAndForeingKeyRelation(tableIndex);
            }
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

        private void UpdatePrimaryKeyContraint(int tableIdx)
        {
            this.DataSet.Tables[tableIdx].Constraints[0].ConstraintName = $"{this.DataSet.Tables[tableIdx]}_PK";
        }

        private void CreatePrimaryAndForeingKeyRelation(int tableIdx)
        {
            int fkKeyCount = this.dbSchema.ForeignKeys[tableIdx].Length;

            for (var fkIdx = 0; fkIdx < fkKeyCount; ++fkIdx)
            {
                string pkTableName = this.dbSchema.FKTables[tableIdx][fkIdx];
                string fkColumnName = this.dbSchema.ForeignKeys[tableIdx][fkIdx];

                if (String.Empty == pkTableName)
                {
                    continue;
                }

                // Create a DataRelation to link the two tables.
                DataColumn primaryKey = this.DataSet.Tables[pkTableName].PrimaryKey[0];
                DataColumn foreignKey = this.DataSet.Tables[tableIdx].Columns[fkColumnName];

                string tableName = dbSchema.TableNames[tableIdx];
                this.DataSet.Relations.Add(new DataRelation($"{tableName}_FK-{fkColumnName}", primaryKey, foreignKey));
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

        /*        
        private void CreatePrimaryKey(in DataTable table, DbStructure.ClientDatabaseSchema.Table tableEnum)
        {
            table.PrimaryKey = new DataColumn[]{table.Columns[DbStructure.ClientDatabaseSchema.PrimaryKeys[tableEnum]]};
        }*/
    }
}
