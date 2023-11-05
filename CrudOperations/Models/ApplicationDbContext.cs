using Microsoft.EntityFrameworkCore;

namespace CrudOperations.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Brand> Brands { get; set; } 
    }
}
