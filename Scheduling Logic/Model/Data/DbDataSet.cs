using Scheduling_Logic.Model.Database;
using Scheduling_Logic.Model.Factory;
using Scheduling_Logic.Model.Structure;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;

namespace Scheduling_Logic.Model.Data
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
        public bool Mapped { get; private set; }
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
        public DbDataSet(DbConnector? dbConnector, DbSchema? dbSchema)
        {

            ValidateForNullParamater(dbConnector, nameof(dbConnector));
            ValidateForNullParamater(dbSchema, nameof(dbSchema));

            this.dbConnector = dbConnector!;
            this.dbSchema = dbSchema!;
            this.DataSet = DataInstance.CreateDataSet(dbSchema!.DbName);
            this.Mapped = false;
        }

        // It maps the content of one specific database with the dataset store on this class
        public void Mapping()
        {
            if (this.Mapped)
            {
                return;
            }

            int tableCount = this.dbSchema.TableNamesIndented.Count;

            for (var tableIndex = 0; tableIndex < tableCount; ++tableIndex)
            {
                string tableName = this.dbSchema.TableNamesIndented[tableIndex];

                dbConnector.MapTableAndColumns(this.dbSchema.DbName, tableName); // can throw DbException or InvalidOperationExcetion
                AddTableToDataSet(tableName);
                dbConnector.FillSchema(this.DataSet, tableName); // can throw Exception
                UpdatePrimaryKeyContraint(tableName);
                dbConnector.Fill(this.DataSet, tableName); // can throw SystemException
            }

            for (var tableIndex = 0; tableIndex < tableCount; ++tableIndex)
            {

                string tableName = this.dbSchema.TableNamesIndented[tableIndex];

                DataColumn pkColumn = this.DataSet.Tables[tableName]!.PrimaryKey[0];
                ChangePKAttributes(pkColumn, tableName);
                CreatePrimaryAndForeingKeyRelation(tableName);
                ;
            }

            this.DataSet.AcceptChanges();
            Mapped = true;
        }

        // It search for items and returns the query if found
        public object Search<T>(SearchRecordsDbMetaData<T> searchDbMetaData)
        {
            ValidateIfDataIsMapped();

            IQueryable<DataRow> query = (from row in this.DataSet.Tables[searchDbMetaData.TableName]!.AsEnumerable()
                                         where EqualityComparer<T>.Default
                                                .Equals(row.Field<T>(searchDbMetaData.ValueColumnName), searchDbMetaData.CurrentValue)
                                         select row).AsQueryable();
            return query;
        }

        // It finds the table id base on the user provided data of that table record
        public int GetRowId<T>(FetchIdDbMetaData<T> fetchIdDbMetaData)
        {
            ValidateIfDataIsMapped();

            IQueryable<DataRow> query = (from row in this.DataSet.Tables[fetchIdDbMetaData.TableName]!.AsEnumerable()
                                         where EqualityComparer<T>.Default
                                         .Equals(row.Field<T>(fetchIdDbMetaData.ValueColumnName), fetchIdDbMetaData.CurrentValue)
                                         select row).AsQueryable();

            return (int)query.First()[fetchIdDbMetaData.IdColumnName];
        }

        // It updates the record from the data table and database based on the user's input
        public void Update<T>(UpdateDbMetaData<T> updateDatabaseMetaData, string updateStatement)
        {
            ValidateIfDataIsMapped();

            IQueryable<DataRow>? query;

            if (typeof(T).Equals(typeof(DateTime)))
            {
                query = (from row in this.DataSet.Tables[updateDatabaseMetaData.TableName]!.AsEnumerable()
                         where row.Field<DateTime>(updateDatabaseMetaData.ValueColumnName)!.CompareTo(updateDatabaseMetaData.CurrentValue) == 0
                         select row).AsQueryable();

            }
            else
            {
                query = (from row in this.DataSet.Tables[updateDatabaseMetaData.TableName]!.AsEnumerable()
                         where row.Field<T>(updateDatabaseMetaData.ValueColumnName)!.Equals(updateDatabaseMetaData.CurrentValue)
                         select row).AsQueryable();
            }

            if (query.Any())
            {
                if (typeof(T).Equals(typeof(DateTime)))
                {
                    DateTime newTime = (DateTime)Convert.ChangeType(updateDatabaseMetaData.NewValue, typeof(DateTime))!;

                    query.First()[updateDatabaseMetaData.ValueColumnName] = newTime.ToUniversalTime();
                }
                else
                {
                    query.First()[updateDatabaseMetaData.ValueColumnName] = updateDatabaseMetaData.NewValue;
                }

                this.dbConnector.Update(this.DataSet, updateDatabaseMetaData, updateStatement);
                this.DataSet.Tables[updateDatabaseMetaData.TableName]!.AcceptChanges();
            }
        }

        // It deletes the record from the data table and database based on the user's input
        public void Delete(DeleteDbMetaData deleteDatabaseMetaData, string deleteStatement)
        {
            ValidateIfDataIsMapped();

            IQueryable<DataRow> query = (from row in this.DataSet.Tables[deleteDatabaseMetaData.TableName]!.AsEnumerable()
                                         where row.Field<int>(deleteDatabaseMetaData.IdColumnName).Equals(deleteDatabaseMetaData.IdValue)
                                         select row).AsQueryable();

            if (query.Any())
            {
                query.First().Delete();

                this.dbConnector.Delete(this.DataSet, deleteDatabaseMetaData.TableName, deleteStatement);
                this.DataSet.Tables[deleteDatabaseMetaData.TableName]!.AcceptChanges();
            }
        }

        // It inserts the record to the data table and database based on the user's input
        public void Insert(string tableName, string[] columnNames, string insertStatement, ArrayList columnValues)
        {
            ValidateIfDataIsMapped();

            DataRow newRow = this.DataSet.Tables[tableName]!.NewRow();

            for (int idx = 0; idx < columnNames.Length; ++idx)
            {
                if (columnValues[idx]!.GetType().Equals(typeof(DateTime)))
                {
                    newRow[columnNames[idx]] = ((DateTime)columnValues[idx]!).ToUniversalTime();
                }
                else
                {
                    newRow[columnNames[idx]] = columnValues[idx];
                }
            }

            this.DataSet.Tables[tableName]!.Rows.Add(newRow);
            this.dbConnector.Insert(this.DataSet, tableName, columnNames, insertStatement);
            this.DataSet.Tables[tableName]!.AcceptChanges();
        }

        private static void ValidateForNullParamater(object? param, string paramName, [CallerMemberName] string callerName = "")
        {
            if (param is null)
            {
                throw new DataClassNullException("<Scheduling_Logic.Model.Data>(DbDataSet)",
                new ArgumentNullException(nameof(param),
                    $"[{callerName}][{paramName}] cannot be null."));
            }
        }

        private void ValidateIfDataIsMapped()
        {
            if (!this.Mapped)
            {
                throw new InvalidOperationException("<Scheduling_Logic.Model.Data>(DbDataSet)\n" +
                    $"{nameof(this.DataSet)} has not been mapped yet.");
            }
        }

        private void AddTableToDataSet(string tableName)
        {
            this.DataSet.Tables.Add(tableName);
        }

        private void UpdatePrimaryKeyContraint(string tableName)
        {
            this.DataSet.Tables[tableName]!.Constraints[0].ConstraintName = $"{this.DataSet.Tables[tableName]}_PK";
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
                    DataColumn primaryKey = this.DataSet.Tables[pkTableName]!.PrimaryKey[0];
                    DataColumn foreignKey = this.DataSet.Tables[tableName]!.Columns[fkColumnName]!;

                    foreignKey.ReadOnly = true;

                    // Create a DataRelation to link the two tables.
                    this.DataSet.Relations.Add(new DataRelation($"{tableName}_FK-{fkColumnName}", primaryKey, foreignKey));
                }
            }
        }

        private void ChangePKAttributes(DataColumn pkColumn, string tableName)
        {
            int rowCount = this.DataSet.Tables[tableName]!.Rows.Count;
            string keyColumnName = pkColumn.ColumnName;
            int autoIncrementSeed = (int)this.DataSet.Tables[tableName]!.Rows[rowCount - 1][keyColumnName];

            pkColumn.AutoIncrementSeed = autoIncrementSeed + 1;
            pkColumn.Unique = true;
            pkColumn.ReadOnly = true;
        }
    }
}
