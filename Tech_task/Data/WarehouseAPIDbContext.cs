using Microsoft.EntityFrameworkCore;
using Tech_task.Models;

namespace Tech_task.Data
{
    public class WarehouseAPIDbContext : DbContext
    {
        public WarehouseAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Worker> Worker { get; set; }

    }
}
