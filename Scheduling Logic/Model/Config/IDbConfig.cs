namespace Scheduling_Logic.Model.Config
{
    // Interface to has the basics requirements for database configurations.
    public interface IDbConfig
    {
        string ConnectionString { get; }
    }
}
