using System;
using System.Collections.Generic;
using System.Text;

namespace Contact_App.Services.Settings
{
    public interface ISettinngsManager
    {
        string LoggedUser { get ; set; }
        string SortListBy { get; set; }
        bool ChangeSort { get; set; }
        bool DarkTheme { get; set; }

    }
}
