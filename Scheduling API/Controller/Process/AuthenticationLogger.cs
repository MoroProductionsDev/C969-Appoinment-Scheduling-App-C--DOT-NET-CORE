using Scheduling_API.Controller.State;
using System.Text;

namespace Scheduling_API.Controller.Process
{
    // It logs data when the user has succesfully authenticated.
    internal sealed class AuthenticationLogger
    {
        const int rootAppPathDirectoryLevels = 4;
        private static readonly string parentDirector = $"..{Path.DirectorySeparatorChar}";
        private static readonly string logsDirectory = $"Logs{Path.DirectorySeparatorChar}";
        const string fileName = $"authentication_logs.txt";
        private readonly string directoryRelativePath;
        private readonly string fileRelativePath;
        private readonly FileStream fileStream;
        private readonly StreamWriter fileWriter;

        internal AuthenticationLogger()
        {
            this.directoryRelativePath = Path.Join(new StringBuilder().Insert(0, parentDirector, rootAppPathDirectoryLevels).ToString(), logsDirectory);
            this.fileRelativePath = Path.Join(directoryRelativePath, fileName);

            Directory.CreateDirectory(this.directoryRelativePath);

            this.fileStream = new FileStream(this.fileRelativePath, FileMode.Append, FileAccess.Write);
            this.fileWriter = new StreamWriter(fileStream);
        }

        ~AuthenticationLogger()
        {
            fileWriter.Close();
            fileWriter.Dispose();

            fileStream.Close();
            fileStream.Dispose();
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
