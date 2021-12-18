using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Contact_App.Models
{
    public class ProfileModel : IEntityBase
    { 
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Owner { get; set; }
        public DateTime CreationTime { get; set; }
        public string Description { get; set; }
}
}