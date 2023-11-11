using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Library.Model.structure
{
    public abstract class DatabaseSchema 
    {
        public abstract String dbName { get; }
        public abstract Dictionary<int, string> TableNames { get; }
        public abstract Dictionary<int, string[]> PrimaryKeys { get; }
        public abstract Dictionary<int, string[]> ForeignKeys { get; }
        public abstract Dictionary<int, string[]> FKTables { get; }
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
