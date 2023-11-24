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
        private readonly DbConnector dbConnector;
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
        public DbDataSet(DbConnector dbConnector, DbSchema dbSchema)
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

            for (var tableIndex = 0; tableIndex < tableCount; ++tableIndex)
            {
                string tableName = this.dbSchema.TableNamesIndented[tableIndex];

                dbConnector.MapTableAndColumns("client_schedule", tableName);
                AddTableToDataSet(tableName);
                dbConnector.FillSchema(this.DataSet, tableName);
                UpdatePrimaryKeyContraint(tableName);
                dbConnector.Fill(this.DataSet, tableName);
            }

            for (var tableIndex = 0; tableIndex < tableCount; ++tableIndex)
            {

                string tableName = this.dbSchema.TableNamesIndented[tableIndex];

                DataColumn pkColumn = this.DataSet.Tables[tableName].PrimaryKey[0];
                ChangePKAttributes(pkColumn, tableName);
                CreatePrimaryAndForeingKeyRelation(tableName);

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

            this.DataSet.AcceptChanges();
            mapped = true;
        }

        public void Update<T>(string dbName, string tableName, string ColumnName, T currentValue, T newValue)
        {
            IQueryable<DataRow> query = (from row in this.DataSet.Tables[tableName].AsEnumerable()
                        where EqualityComparer<T>.Default.Equals(row.Field<T>(ColumnName), currentValue)
                        select row).AsQueryable();

            if (query.Count() > 0)
            {
                query.First()[ColumnName] = newValue;

                this.dbConnector.Update(this.DataSet, dbName, tableName);
                this.DataSet.Tables[tableName].AcceptChanges();
            }
        }

        public void Delete<T>(string tableName, string ColumnName, T currentValue)
        {
            IQueryable<DataRow> query = (from row in this.DataSet.Tables[tableName].AsEnumerable()
                                         where EqualityComparer<T>.Default.Equals(row.Field<T>(ColumnName), currentValue)
                                         select row).AsQueryable();

            if (query.Count() > 0)
            {
                query.First().Delete();
                this.DataSet.Tables[tableName].AcceptChanges();

                //SqlCommandBuilder builder = new SqlCommandBuilder(dbDataAdapter);
                //adapter.UpdateCommand = builder.GetUpdateCommand();
                //dbDataAdapter.Update(this.DataSet);
                this.DataSet.AcceptChanges();
            }
        }

        public void Insert(string tableName)
        {
            DataRow newRow = this.DataSet.Tables[tableName].NewRow();

            newRow["customerName"] = "Peppe";

            this.DataSet.Tables[tableName].Rows.Add(newRow);
        }

        private void AddTableToDataSet(string tableName)
        {
            this.DataSet.Tables.Add(tableName);
        }

        private void UpdatePrimaryKeyContraint(string tableName)
        {
            this.DataSet.Tables[tableName].Constraints[0].ConstraintName = $"{this.DataSet.Tables[tableName]}_PK";
        }

        private void CreatePrimaryAndForeingKeyRelation(string tableName)
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

        private void ChangePKAttributes(DataColumn pkColumn, string tableName)
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
    }
}
