using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Xml.Serialization;
using SchedulingLibrary;

namespace Scheduling_Console_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Application application = new Application();

            application.Run(Provider.MySql);
        }
    }
}
