using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingMap
{
    public class User
    {
        public int id { get; set; }
        string login, password;
        public string Login
        {
            get { return this.login; }
            set { this.login = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public User() { }
        public User(int id, string login, string password)
        {
            this.id = id;
            this.login = login;
            this.password = password;
        }

    }
}
