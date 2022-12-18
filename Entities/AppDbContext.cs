using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}