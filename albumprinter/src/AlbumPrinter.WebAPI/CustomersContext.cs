using Microsoft.EntityFrameworkCore;

namespace AlbumPrinter.WebAPI
{
    // TODO: Extract data access from web API. Either take direct dependency
    // to EF Core or abstract over custom repository interface.
    public class CustomersContext : DbContext
    {
        public CustomersContext(DbContextOptions<CustomersContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerID);
        }
    }
}
