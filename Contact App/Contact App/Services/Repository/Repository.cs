using Contact_App.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Contact_App.Services.Repository
{
    class Repository : IRepository
    {
        public Repository()
        {
            database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "profilebook.db3");
                SQLiteAsyncConnection _database = new SQLiteAsyncConnection(path);

                _database.CreateTableAsync<UserModel>().Wait();
                _database.CreateTableAsync<ProfileModel>().Wait();
                return _database;
            });
        }

        #region Private fields

        private Lazy<SQLiteAsyncConnection> database;

        #endregion


        #region Public methods


        public Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new() => database.Value.DeleteAsync(entity);


        public Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new() => database.Value.Table<T>().ToListAsync();


        public Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new() => database.Value.InsertAsync(entity);


        public Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new() => database.Value.UpdateAsync(entity);
        public Task<List<ProfileModel>> GetProfilesAsync(string owner) => database.Value.Table<ProfileModel>()
                                                                                       .Where(p => p.Owner == owner)
                                                                                       .ToListAsync();

        #endregion
    }
}

