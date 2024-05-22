using Microsoft.EntityFrameworkCore;

namespace Gamestore.Repository.Entities;

public class GamestoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; }

    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<GameGenre> GameGenres { get; set; }

    public DbSet<GamePlatform> GamePlatforms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Genres
        Guid strategyGuid = Guid.NewGuid();
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = strategyGuid, Name = "Strategy" });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "RTS", ParentGenreId = strategyGuid });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "TBS", ParentGenreId = strategyGuid });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "RPG" });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "Sports" });
        Guid racesGuid = Guid.NewGuid();
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = racesGuid, Name = "Races" });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "Rally", ParentGenreId = racesGuid });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "Arcade", ParentGenreId = racesGuid });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "Formula", ParentGenreId = racesGuid });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "Off-road", ParentGenreId = racesGuid });
        Guid actionGuid = Guid.NewGuid();
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = actionGuid, Name = "Action" });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "FPS", ParentGenreId = actionGuid });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "TPS", ParentGenreId = actionGuid });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "Adventure" });
        modelBuilder.Entity<Genre>().HasData(new Genre() { Id = Guid.NewGuid(), Name = "Puzzle & Skill" });

        // Platforms
        modelBuilder.Entity<Platform>().HasData(new Platform() { Id = Guid.NewGuid(), Type = "Mobile" });
        modelBuilder.Entity<Platform>().HasData(new Platform() { Id = Guid.NewGuid(), Type = "Browser" });
        modelBuilder.Entity<Platform>().HasData(new Platform() { Id = Guid.NewGuid(), Type = "Desktop" });
        modelBuilder.Entity<Platform>().HasData(new Platform() { Id = Guid.NewGuid(), Type = "Console" });

        // Games
        modelBuilder.Entity<Game>().HasData(new Game() { Id = Guid.NewGuid(), Name = "Gra testowa nazwa", Description = "Desc", Key = "Key" });
    }
}
