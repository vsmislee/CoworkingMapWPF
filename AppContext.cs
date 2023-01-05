using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CoworkingMap;
using System.Runtime.Remoting.Contexts;

namespace CoworkingMap
{
    internal class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppContext() : base("DefaultConnection") { }
    }
}
