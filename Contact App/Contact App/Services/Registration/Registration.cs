using Contact_App.ServiceData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contact_App.Services.DbService;
using Contact_App.Validators;
using Contact_App.Models;
using System.Linq;

namespace Contact_App.Services.Registration
{
    public class Registration : IRegistration
    {
        private readonly IDbService dbService;

        public Registration(IDbService _dbService) => dbService = _dbService;

        public async Task<CodeUserAuthResult> IsRegistration(string login, string password, string confirmPassword)
        {
            bool result = await Task.Run(() => UserDataValidators.IsDataValid(login.ToUpper(), CheckedItem.Login));
            if (!result)
                return CodeUserAuthResult.InvalidLogin;

            result = password == confirmPassword;
            if (!result)
                return CodeUserAuthResult.PasswordMismatch;

            result = await Task.Run(() => UserDataValidators.IsDataValid(password, CheckedItem.Password));
            if (!result)
                return CodeUserAuthResult.InvalidPassword;

            List<UserModel> users = await dbService.GetAllDataAsync<UserModel>();
            result = users.Any(s => s.Login.ToUpper() == login.ToUpper());
            if (result)
                return CodeUserAuthResult.LoginTaken;

            return CodeUserAuthResult.Passed;
        }
    }
}
