using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Data;

namespace Scheduling_Console_App.Data
{
    internal interface IConnectable
    {
        bool Open();

        bool Close();

        IConnectable Command(string commandString);

        IConnectable Execute();

    }
}
