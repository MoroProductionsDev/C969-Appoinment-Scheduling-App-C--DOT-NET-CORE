using Scheduling_API.Controller.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_API.Controller.Process
{
    internal class AuthenticationLogger
    {
        const int rootAppPathDirectoryLevels = 4;
        private static readonly string parentDirector = $"..{Path.DirectorySeparatorChar}";
        private static readonly string logsDirectory = $"Logs{Path.DirectorySeparatorChar}";
        const string fileName = $"authentication_logs.txt";
        private readonly string directoryRelativePath;
        private readonly string fileRelativePath;
        private FileStream fileStream;
        private StreamWriter fileWriter;

        internal AuthenticationLogger()
        {
            this.directoryRelativePath = Path.Join(new StringBuilder().Insert(0, parentDirector, rootAppPathDirectoryLevels).ToString(), logsDirectory);
            this.fileRelativePath = Path.Join(directoryRelativePath, fileName);

            Directory.CreateDirectory(this.directoryRelativePath);

            this.fileStream = new FileStream(this.fileRelativePath, FileMode.Append, FileAccess.Write);
            this.fileWriter = new StreamWriter(fileStream);
        }

        ~AuthenticationLogger() { 
            fileWriter.Close();
            fileWriter.DisposeAsync();

            fileStream.Close();
            fileStream.DisposeAsync();
        }

        internal void WriteLog(AppState appState)
        {
            using (fileStream)
            {
                using (fileWriter)
                {
                    if (new FileInfo(fileRelativePath).Length == 0)
                    {
                        // File Header
                        fileWriter.WriteAsync(String.Format("{0, -20} | {1,-20} | {2,-15} {3, -15}\n", "UserName", "Login Date", "Login Time", "Second(s)"));
                        fileWriter.WriteLineAsync(new string('-', 86));
                    }

                    fileWriter.WriteLineAsync(String.Format("{0, -20} | {1, -20} | {2, -15} {3, -15}", 
                                                appState.AppData.UserRecord.UserName, 
                                                DateTime.Now.ToShortDateString(), 
                                                DateTime.Now.ToLocalTime().ToShortTimeString(),
                                                $"{DateTime.Now.Second} sec"));

                    fileStream.FlushAsync();
                    fileWriter.Close();
                }
                fileStream.Close();
            }
        }
    }
}
