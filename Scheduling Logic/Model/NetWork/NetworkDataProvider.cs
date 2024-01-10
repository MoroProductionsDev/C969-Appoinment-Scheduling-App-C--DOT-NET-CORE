using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Logic.Model.NetWork
{
    public static class NetworkDataProvider
    {
        private readonly static HttpClient client = new HttpClient();
        private readonly static string ipInfoApiToken = "YOUR IP INFO API KEY";
        private static string userIp = String.Empty;
        private const string ipUri = "http://ifconfig.me";

        public static async void GetUserIp()
        {
            userIp = (await client.GetStringAsync(ipUri)).ToString();
        }

        public static string GetUserLocation()
        {
            string result = String.Empty;
            string location = String.Empty;

            if (userIp == String.Empty)
            {
                GetUserIp();
            }

            result = client.GetStringAsync($"https://ipinfo.io/{userIp}/city?token={ipInfoApiToken}").Result;
            location += result.AsSpan(0, result.Length - 1).ToString();
            location += ", ";

            result = client.GetStringAsync($"https://ipinfo.io/{userIp}/country?token={ipInfoApiToken}").Result;
            location += result.AsSpan(0, result.Length - 1).ToString();

            return location;
        }

        public static string GetUserTimeZone()
        {
            string result = String.Empty;
            string timeZone = String.Empty;

            if (userIp == String.Empty)
            {
                GetUserIp();
            }

            result = client.GetStringAsync($"https://ipinfo.io/{userIp}/timezone?token={ipInfoApiToken}").Result;
            timeZone += result.AsSpan(0, result.Length - 1).ToString();

            return timeZone;
        }
    }
}
