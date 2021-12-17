using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contact_App.Services.Authorization
{
    public interface IAuthorization
    {
        Task<bool> IsAuthorization(string login, string password);
    }
}
