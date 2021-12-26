

using Contact_App.Services.Localization;
using System.Globalization;

namespace Contact_App.Droid
{
    public class Localize : ILocalize
    {
        public System.Globalization.CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;

            var netLanguage = androidLocale.ToString().Replace("_", "-");

            return new System.Globalization.CultureInfo(netLanguage);
        }
    }
}