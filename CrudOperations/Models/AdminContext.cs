using Microsoft.EntityFrameworkCore;

namespace CrudOperations.Models
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options):base(options)
        {
            
        }
        public DbSet<Admin> Admins { get; set; }
    }
}
