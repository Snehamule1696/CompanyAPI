using CompanyAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Data
{
    public class CompanyDbContext:DbContext
    {
        public CompanyDbContext()
        {
        }
        public CompanyDbContext(DbContextOptions<CompanyDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
    }
}
