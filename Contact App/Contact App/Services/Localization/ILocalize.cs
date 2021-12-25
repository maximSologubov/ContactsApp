using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Contact_App.Services.Localization
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
