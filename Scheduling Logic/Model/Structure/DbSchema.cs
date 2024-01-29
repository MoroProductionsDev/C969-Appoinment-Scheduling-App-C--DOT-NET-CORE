namespace Scheduling_Logic.Model.Structure
{
    // Abstract class use the basic require information fro the database schema.
    public abstract class DbSchema
    {
        public abstract string DbName { get; }
        public abstract Dictionary<int, string> TableNamesIndented { get; }
        public abstract Dictionary<string, string[]> PrimaryKeysNames { get; }
        public abstract Dictionary<string, string[]> ForeignKeysNames { get; }
        public abstract Dictionary<string, string[]> FKTablesNames { get; }
    }
}
