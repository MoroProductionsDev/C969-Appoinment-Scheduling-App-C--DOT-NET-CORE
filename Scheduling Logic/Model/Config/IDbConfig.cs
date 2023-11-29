using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Logic.Model.Config
{
    //
    public interface IDbConfig
    {
        string ConnectionString { get; }
    }
}
