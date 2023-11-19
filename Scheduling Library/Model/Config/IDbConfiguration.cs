using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Console_App.Controller.Config
{
    //
    public interface IDbConfiguration
    {
        String ConnectionString { get; }
        Type ConnectionType { get; }
    }
}
