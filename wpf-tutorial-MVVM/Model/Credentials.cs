using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_tutorial_MVVM.Model
{
    public class Credentials
    {
        public Credentials()
        {

        }

        public string Username { get; set; }
        public string Password { get; set; }

        public void Save(string username, string password)
        {
            Username = username;
            Password = password;
        }

        internal void Load()
        {
            Username = "danodotnet";
            Password = "prova1";
        }
    }
}
