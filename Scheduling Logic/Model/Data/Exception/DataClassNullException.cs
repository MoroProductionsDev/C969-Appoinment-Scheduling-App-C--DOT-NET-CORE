using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Logic.Model.Data
{
    internal class DataClassNullException : Exception
    {
        internal DataClassNullException() { }

        internal DataClassNullException(string message)
            : base(message) { }

        internal DataClassNullException(string message, Exception inner)
            : base(message, inner) { }
    }
}
