using Microsoft.EntityFrameworkCore;

namespace PlaywrightTestDemo.Utilities
{
    public class ProductDbContext: DbContext
    {

        public DbSet<Product> Products { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=E:\\source\\EAAppBrila\\ProductAPI\\Product.db");
        }
    }
}
