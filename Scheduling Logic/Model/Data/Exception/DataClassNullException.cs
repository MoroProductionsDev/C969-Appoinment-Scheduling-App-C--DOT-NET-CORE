namespace Scheduling_Logic.Model.Data
{
    // User defined excpetion to validate the data classes on the [Data] folder
    internal class DataClassNullException : Exception
    {
        internal DataClassNullException() { }

        internal DataClassNullException(string message)
            : base(message) { }

        internal DataClassNullException(string message, Exception inner)
            : base(message, inner) { }
    }
}
