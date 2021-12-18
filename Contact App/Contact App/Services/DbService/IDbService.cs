using Contact_App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contact_App.Services.DbService
{
    public interface IDbService
    {
        Task<int> InsertDataAsync<T>(T entity) where T : IEntityBase, new();
        Task<int> UpdateDataAsync<T>(T entity) where T : IEntityBase, new();
        Task<int> DeleteDataAsync<T>(T entity) where T : IEntityBase, new();
        Task<List<T>> GetAllDataAsync<T>() where T : IEntityBase, new();
        Task<List<ProfileModel>> GetOwnersProfilesAsync(string owner);
    }
}
