using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Library.Model.Config
{
    //
    public interface IDbConfig
    {
        String ConnectionString { get; }
    }
}
