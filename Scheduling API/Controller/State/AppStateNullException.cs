using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_API.Controller.State
{
    internal class AppStateNullException : Exception
    {
        internal AppStateNullException() { }

        internal AppStateNullException(string message)
            : base(message) { }

        internal AppStateNullException(string message, Exception inner)
            : base(message, inner) { }
    }
}
