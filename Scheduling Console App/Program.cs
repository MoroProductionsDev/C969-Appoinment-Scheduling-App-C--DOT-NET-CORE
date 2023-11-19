using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Scheduling_Library.Model.Structure;

// Following C# naming conventions:
// https://en.wikibooks.org/wiki/C_Sharp_Programming/Naming#:~:text=Namespaces%20are%20named%20using%20Pascal%20Case%20%28also%20called,the%20first%20letter%20capitalized%20%28MyXmlNamespace%20instead%20of%20MyXMLNamespace%29.
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names
namespace Scheduling_Console_App
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            Application application = new Application(DbProvider.MySqlClient, DbName.ClientScheduleDbName);

            application.Run();
        }
    }
}
