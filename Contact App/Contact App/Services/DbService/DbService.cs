using Contact_App.Models;
using Contact_App.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contact_App.Services.DbService
{
    public class DbService : IDbService
    {
        public DbService(IRepository _repository) => repository = _repository;


        #region Private fields

        private IRepository repository;

        #endregion

        #region Public methods

        public Task<int> DeleteDataAsync<T>(T entity) where T : IEntityBase, new() => repository.DeleteAsync(entity);


        public Task<List<T>> GetAllDataAsync<T>() where T : IEntityBase, new() => repository.GetAllAsync<T>();


        public Task<int> InsertDataAsync<T>(T entity) where T : IEntityBase, new() => repository.InsertAsync(entity);


        public Task<int> UpdateDataAsync<T>(T entity) where T : IEntityBase, new() => repository.UpdateAsync(entity);

        public Task<List<ProfileModel>> GetOwnersProfilesAsync(string owner) => repository.GetProfilesAsync(owner);

        #endregion
    }
}
