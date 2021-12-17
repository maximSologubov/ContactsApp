using Contact_App.Models;
using Contact_App.Services.DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_App.Validators;
using Contact_App.ServiceData;

namespace Contact_App.Services.Authorization
{
    public class Authorization : IAuthorization
    {
        private readonly IDbService dbService;

        public Authorization(IDbService _dbService) => dbService = _dbService;


        public async Task<bool> IsAuthorization(string login, string password)
        {

           
            Task<bool> t2 = Task.Run(() => UserDataValidators.IsDataValid(password, CheckedItem.Password));
            Task<bool> t1 = Task.Run(() => UserDataValidators.IsDataValid(login.ToUpper(), CheckedItem.Login));

            await Task.WhenAll(new[] { t1, t2 });

            if (!(t1.Result && t2.Result))
                return false;

            List<UserModel> users = await dbService.GetAllDataAsync<UserModel>();
            return users.Any(s => s.Login.ToUpper() == login.ToUpper() && s.Password == password);
        }
    }
}
