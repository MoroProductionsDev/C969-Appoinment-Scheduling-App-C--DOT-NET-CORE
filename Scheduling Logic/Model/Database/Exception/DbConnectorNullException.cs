namespace Scheduling_Logic.Model.Database
{
    internal class DbConnectorNullException : Exception
    {
        // User defined excpetion to validate the database classes on the [Database] folder
        internal DbConnectorNullException() { }

        internal DbConnectorNullException(string message)
            : base(message) { }

        internal DbConnectorNullException(string message, Exception inner)
            : base(message, inner) { }
    }
}
