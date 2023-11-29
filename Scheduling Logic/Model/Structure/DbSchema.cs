using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Logic.Model.Structure
{
    public abstract class DbSchema 
    {
        public abstract string DbName { get; }
        public abstract Dictionary<int, string> TableNamesIndented { get; }
        public abstract Dictionary<string, string[]> PrimaryKeysNames { get; }
        public abstract Dictionary<string, string[]> ForeignKeysNames { get; }
        public abstract Dictionary<string, string[]> FKTablesNames { get; }
    }
}
