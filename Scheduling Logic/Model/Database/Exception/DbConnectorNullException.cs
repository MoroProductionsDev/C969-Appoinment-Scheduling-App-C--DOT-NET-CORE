using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Logic.Model.Database
{
    internal class DbConnectorNullException : Exception
    {
        internal DbConnectorNullException(string message)
            : base(message) { }

        internal DbConnectorNullException(string message, Exception inner)
            : base(message, inner) { }
    }
}
