using System.Globalization;

namespace Scheduling_UI_Library
{
    // It changes the Thread's current culture and the UI current culter
    // based on the user's action. It stores the culture's code for language translation. 
    public static class LangTranslator
    {
        public const string EN = "en";
        public const string ES = "es";
        public const string ZH = "zh";
        private static string CurrentCode = string.Empty;
        public static void SetLangCode(string code)
        {
            CultureInfo _info = new(code);
            Thread.CurrentThread.CurrentCulture = _info;
            Thread.CurrentThread.CurrentUICulture = _info;

            CurrentCode = code;
        }

        public static string GetLangCode()
        {
            return CurrentCode;
        }
    }
}
