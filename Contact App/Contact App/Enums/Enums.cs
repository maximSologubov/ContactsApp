using System;
using System.Collections.Generic;
using System.Text;

namespace Contact_App.ServiceData
{
    public enum CheckedItem : byte
    {
        Login,
        Password
    }
    public enum CodeUserAuthResult : byte
    {
        Passed,
        InvalidLogin,
        LoginTaken,
        InvalidPassword,
        PasswordMismatch
    }
    public enum CompareProfileSelector : byte
    {
        Name,
        NickName,
        DateCreation
    }
}
