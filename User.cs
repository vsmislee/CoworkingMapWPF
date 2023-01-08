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
        public string login, password;
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Password
        {
            get { return login; }
            set { login = value; }
        }
        public User() { }
        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

    }
}
