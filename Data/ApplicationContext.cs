using Microsoft.EntityFrameworkCore;
using TEST.Models;

namespace TEST.Data
{
    public class ApplicationContext:DbContext
    {
      public  ApplicationContext(DbContextOptions<ApplicationContext>options):base(options) { }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Employee> Employees { get;set; }
    }
}
