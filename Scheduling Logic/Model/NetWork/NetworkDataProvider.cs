namespace Scheduling_Logic.Model.NetWork
{
    // Gets the project data required from API's client request
    public static class NetworkDataProvider
    {
        private readonly static HttpClient client = new();
        private readonly static string ipInfoApiToken = "YOUR API KEY";
        private static string userIp = String.Empty;
        private const string ipUri = "http://ifconfig.me";

        public static async void GetUserIp()
        {
            userIp = (await client.GetStringAsync(ipUri)).ToString(); // this can throw - not being catch for now.
        }

        public static string GetUserLocation()
        {
            string result;
            string location = string.Empty;

            if (userIp == string.Empty)
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
            string result;
            string timeZone = string.Empty;

            if (userIp == string.Empty)
            {
                GetUserIp();
            }

            result = client.GetStringAsync($"https://ipinfo.io/{userIp}/timezone?token={ipInfoApiToken}").Result;
            timeZone += result.AsSpan(0, result.Length - 1).ToString();

            return timeZone;
        }
    }
}
