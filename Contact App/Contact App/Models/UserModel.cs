using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contact_App.Models
{
    class UserModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
