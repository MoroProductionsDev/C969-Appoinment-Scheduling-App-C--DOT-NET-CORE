using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Library
{
    /*
     * Description: This interface is the contract that different database connectors should implement these
     *              properties and behaviors.
     */
    public interface IDatabaseConnector : IDisposable 
    {
        ConnectionState ConnectionState { get; }
        IDbDataAdapter DbDataAdapter { get; }

        void OpenConnection();
        void CloseConnection();
        IDbCommand CreateDbCommand(in string commandText);
        void CreateDbDataAdapter();
        void Execute();
    }
}
