// See https://aka.ms/new-console-template for more information
//using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Scheduling_API.Controller.State;
using Scheduling_API.Model.State;

// Following C# naming conventions:
// https://en.wikibooks.org/wiki/C_Sharp_Programming/Naming#:~:text=Namespaces%20are%20named%20using%20Pascal%20Case%20%28also%20called,the%20first%20letter%20capitalized%20%28MyXmlNamespace%20instead%20of%20MyXMLNamespace%29.
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names
namespace Scheduling_Console_App 
{
    internal sealed partial class Program
    {
        static void Main(string[] args)
        {
            Application application = new Application(AppDbInfo.MySqlClient, AppDbInfo.ClientScheduleDbName);

            application.Run();
        }
    }
}