using Contact_App.ServiceData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contact_App.Services.Registration
{
    public interface IRegistration
    {
        Task<CodeUserAuthResult> IsRegistration(string login, string password, string confirmPassword);
    }
}
