namespace Scheduling_Logic.Model.Data
{
    // Data required to the update database dataset and database
    public record UpdateDbMetaData<T>
    {
        public string DbName { private set; get; }
        public string TableName { private set; get; }
        public string ValueColumnName { private set; get; }
        public T CurrentValue { private set; get; }
        public T NewValue { private set; get; }
        public string IdColumnName { private set; get; }
        public int IdValue { private set; get; }

        public UpdateDbMetaData(string _dbName, string _tableName, string _valueColumnName, T _currentValue, T _newValue, string _idColumnName, int _idValue)
        {
            this.DbName = _dbName;
            this.TableName = _tableName;
            this.ValueColumnName = _valueColumnName;
            this.CurrentValue = _currentValue;
            this.NewValue = _newValue;
            this.IdColumnName = _idColumnName;
            this.IdValue = _idValue;
        }
    }
    public record DeleteDbMetaData
    {
        public string DbName { private set; get; }
        public string TableName { private set; get; }
        public string IdColumnName { private set; get; }
        public int IdValue { private set; get; }

        public DeleteDbMetaData(string _dbName, string _tableName, string _idColumnName, int _idValue)
        {
            this.DbName = _dbName;
            this.TableName = _tableName;
            this.IdColumnName = _idColumnName;
            this.IdValue = _idValue;
        }
    }

    public record FetchIdDbMetaData<T>
    {
        public string DbName { private set; get; }
        public string TableName { private set; get; }
        public string ValueColumnName { private set; get; }
        public T CurrentValue { private set; get; }
        public string IdColumnName { private set; get; }
        public FetchIdDbMetaData(string _dbName, string _tableName, string _valueColumnName, T _currentValue, string _idColumnName)
        {
            this.DbName = _dbName;
            this.TableName = _tableName;
            this.ValueColumnName = _valueColumnName;
            this.CurrentValue = _currentValue;
            this.IdColumnName = _idColumnName;
        }
    }

    public record SearchRecordsDbMetaData<T>
    {
        public string DbName { private set; get; }
        public string TableName { private set; get; }
        public string ValueColumnName { private set; get; }
        public T CurrentValue { private set; get; }
        public SearchRecordsDbMetaData(string _dbName, string _tableName, string _valueColumnName, T _currentValue)
        {
            this.DbName = _dbName;
            this.TableName = _tableName;
            this.ValueColumnName = _valueColumnName;
            this.CurrentValue = _currentValue;
        }
    }
}
