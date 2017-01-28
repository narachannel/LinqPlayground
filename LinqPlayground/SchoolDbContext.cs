using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace LinqPlayground
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<School> Schols { get; set; }
    }
}
