using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Library.Model.Database
{
    /*
     * Description: This interface is the contract that different database connectors should implement these
     *              properties and behaviors.
     */
    public interface IDbConnector : IDisposable 
    {
        ConnectionState ConnState { get; }
        IDbDataAdapter DbDtAdapter { get; } // named this way to avoid confusion with the [Systen.Data.Common.DbDataAdapter] abstract class

        void OpenConnection();
        void CloseConnection();
        IDbCommand CreateDbCommand(in string commandText);
        void CreateDbDataAdapter();
        void Execute();
    }
}
