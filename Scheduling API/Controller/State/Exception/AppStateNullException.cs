namespace Scheduling_API.Controller.State
{
    // User defined excpetion to validate the AppState class on the [Data] folder
    internal class AppStateNullException : Exception
    {
        internal AppStateNullException() { }

        internal AppStateNullException(string message)
            : base(message) { }

        internal AppStateNullException(string message, Exception inner)
            : base(message, inner) { }
    }
}
