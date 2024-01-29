namespace Scheduling_API.Controller.State
{
    // User defined excpetion to validate the AppData class on the [Data] folder
    internal class AppDataNullException : Exception
    {
        internal AppDataNullException() { }

        internal AppDataNullException(string message)
            : base(message) { }

        internal AppDataNullException(string message, Exception inner)
            : base(message, inner) { }
    }
}