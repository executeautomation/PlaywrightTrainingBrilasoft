using Microsoft.EntityFrameworkCore;

namespace PlaywrightTestDemo.Utilities
{
    public class ProductDbContext: DbContext
    {

        public DbSet<Product> Products { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=E:\\EAApp_LocalMachine-6\\ProductAPI\\Product.db");
        }
    }
}
