using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Android2048
{
    internal class User
    {
        public User() { }
        public User(string name, string password)
        {
            this.Name = name;
            this.Password = password;
            this.HighestTile = 2;
            this.BestScore = 0;
        }
        [MaxLength(50), Unique, PrimaryKey]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        public int BestScore { get; set; }
        public int HighestTile { get; set; }
    }
}