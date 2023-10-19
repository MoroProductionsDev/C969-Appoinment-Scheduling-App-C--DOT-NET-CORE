using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingLibrary
{
    /*
     * Description: This interface is the contract that different database connectors should implement these
     *              properties and behaviors.
     */
    public interface IDatabaseConnector : IDisposable 
    {
        IDataReader DbDataReader { get; }

        void OpenConnection();
        void CloseConnection();
        IDatabaseConnector CreateCommand(string commandText);
        void Execute();
        /*Task<IDatabaseConnector> ConnectAsync(string connectionString);*/
    }
}
