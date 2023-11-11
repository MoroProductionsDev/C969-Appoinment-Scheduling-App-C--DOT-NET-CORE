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
using Scheduling_Library.Model.database;
using Scheduling_Library.Model.factory;
using Scheduling_Library.Model.structure;

namespace Scheduling_Library.Model.data
{
    /*
     * Description: This class is use to store the returned fetched value that the
     *              the database reader returns when it succesfully fetch data.
     */

    /* References:
     * Reference from the Microsoft's page on how to use the [DataTable], [DataRows], [DataColumns], and [Data Sets].
     * https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-7.0 
     * How to use switch statements on system's type.
     * https://stackoverflow.com/questions/43080505/how-to-switch-on-system-type
     */
    public sealed class DbDataSet
    {
        private readonly IDbConnector dbConnector;
        private readonly DbSchema dbSchema;
        private readonly DataSet dataSet;

        /*
         * Description: Parameterized Constructor
         * 
         * @param       [IDataReader] reader        It carries a reference to database data reader returned from it.
         *              [DataTable] dataTable       It carried a reference to data table use to store the fetched data provided
         *                                          by the database data reader.
         *                                          
         * mutation     It initialize this.DbDataTable property/object based on the [DataTable] reference passed.
         *              It initialize this.reader private field/object based on the [IDataReader] reference passed.  
         */
        public DbDataSet(in IDbConnector connector, in DbSchema schema)
        {
            this.dbConnector = connector;
            this.dbSchema = schema;
            this.dataSet = DataInstance.createDataSet(schema.dbName);
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
            IDbDataAdapter dbDataAdapter = this.dbConnector.DbDataAdapter;


            dbDataAdapter.SelectCommand = this.dbConnector.CreateDbCommand(String.Empty);

            for (var tableIndex = 0; tableIndex < tableCount; ++tableIndex)
            {
                string tableName = this.dbSchema.TableNames[tableIndex];

                MapTableAndColumns(in dbDataAdapter, in tableName);
                AddDataSetTable(in tableName);
                FillSchema(in dbDataAdapter, in tableName);
                UpdatePrimaryKeyContraint(tableIndex);
                Fill(in dbDataAdapter, in tableName);
            }

            for (var tableIndex = 0; tableIndex < tableCount; ++tableIndex)
            {
                CreateRelationsBetween(tableIndex);
            }
        }


        // https://learn.microsoft.com/en-us/dotnet/api/system.data.dataset?view=net-7.0
        private void MapTableAndColumns(in IDbDataAdapter dbDataAdapter, in String tableName)
        {
            dbDataAdapter.SelectCommand.CommandText = $"SELECT * FROM `{this.dbSchema.dbName}`.`{tableName}`;"; ;
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

        private void AddDataSetTable(in String tableName)
        {
            this.dataSet.Tables.Add(tableName);
        }

        private void FillSchema(in IDbDataAdapter dbDataAdapter, in String tableName)
        {
            ((DbDataAdapter)dbDataAdapter).FillSchema(this.dataSet, SchemaType.Mapped, tableName);
        }

        private void Fill(in IDbDataAdapter dbDataAdapter, in String tableName)
        {
            ((DbDataAdapter)dbDataAdapter).Fill(this.dataSet, tableName);
        }

        private void UpdatePrimaryKeyContraint(int tableIdx)
        {
            this.dataSet.Tables[tableIdx].Constraints[0].ConstraintName = $"{this.dataSet.Tables[tableIdx]}_PK";
        }

        private void CreateRelationsBetween(int tableIdx)
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
                DataColumn primaryKey = this.dataSet.Tables[pkTableName].PrimaryKey[0];
                DataColumn foreignKey = this.dataSet.Tables[tableIdx].Columns[fkColumnName];

                string tableName = dbSchema.TableNames[tableIdx];
                this.dataSet.Relations.Add(new DataRelation($"{tableName}_FK-{fkColumnName}", primaryKey, foreignKey));
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
