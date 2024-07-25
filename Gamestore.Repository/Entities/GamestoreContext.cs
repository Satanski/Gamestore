using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Entities;

public class GamestoreContext(DbContextOptions<GamestoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; }

    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Publisher> Publishers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderGame> OrderGames { get; set; }

    public DbSet<GameGenres> GameGenres { get; set; }

    public DbSet<GamePlatform> GamePlatforms { get; set; }

    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
