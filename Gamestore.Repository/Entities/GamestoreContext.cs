using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Entities;

public class GamestoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Category> Genres { get; set; }

    public DbSet<Supplier> Publishers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderProduct> OrderProducts { get; set; }

    public DbSet<ProductCategory> ProductGenres { get; set; }

    public DbSet<ProductPlatform> ProductPlatforms { get; set; }

    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
