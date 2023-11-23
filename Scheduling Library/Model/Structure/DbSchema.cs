using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Library.Model.Structure
{
    public abstract class DbSchema 
    {
        public abstract String DbName { get; }
        public abstract Dictionary<int, string> TableNamesIndented { get; }
        public abstract Dictionary<string, string[]> PrimaryKeysNames { get; }
        public abstract Dictionary<string, string[]> ForeignKeysNames { get; }
        public abstract Dictionary<string, string[]> FKTablesNames { get; }
        protected IEnumerable<T> GetEnumarated<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        protected IList<T> GetList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}
