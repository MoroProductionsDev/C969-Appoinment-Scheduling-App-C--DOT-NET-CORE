using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_API.Controller.State
{
    internal class AppDataNullException : Exception
    {
        internal AppDataNullException() { }

        internal AppDataNullException(string message)
            : base(message) { }

        internal AppDataNullException(string message, Exception inner)
            : base(message, inner) { }
    }
}