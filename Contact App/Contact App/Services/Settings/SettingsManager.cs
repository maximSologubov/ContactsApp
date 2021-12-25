﻿using System;
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
        public string SortListBy
        {
            get => Preferences.Get(nameof(SortListBy), "");
            set => Preferences.Set(nameof(SortListBy), value);
        }

        public bool ChangeSort
        {
            get => Preferences.Get(nameof(ChangeSort), false);
            set => Preferences.Set(nameof(ChangeSort), value);
        }

        public bool DarkTheme
        {
            get => Preferences.Get(nameof(DarkTheme), false);
            set => Preferences.Set(nameof(DarkTheme), value);
        }
    }
}
