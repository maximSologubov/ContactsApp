using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Contact_App.Services.Settings
{
    public class SettingsManager : ISettinngsManager
    {       
        public string LoggedUser
        {
            get => Preferences.Get(nameof(LoggedUser), "");
            set => Preferences.Set(nameof(LoggedUser), value);
        }
       
    }
}
