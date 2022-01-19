using Microsoft.EntityFrameworkCore;
using webApi.Models;

namespace webApi.Data
{
    public class SelectCrsContext: DbContext
    {
        public SelectCrsContext(DbContextOptions<SelectCrsContext> opt): base(opt)
        {

        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

    }
}