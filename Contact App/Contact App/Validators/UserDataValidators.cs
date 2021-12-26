using Contact_App.ServiceData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Contact_App.Validators
{
    public static class UserDataValidators
    {
        public static bool IsDataValid(string data, CheckedItem checkedItem)
        {
            if (checkedItem == CheckedItem.Login)
                return IsLoginValid(data);
            return IsPasswordValid(data);
        }

        private static bool IsLoginValid(string login)
        {
            var regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{3,15}$");
            
            return regex.IsMatch(login);
        }

        private static bool IsPasswordValid(string password)
        {
            var regex = new Regex(@"(?=.*\d{1})(?=.*?[A-Z]{1})(?=.*?[a-z]{1}).{7,15}");

            return regex.IsMatch(password);
        }
    }
}
