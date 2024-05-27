using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Helpers;

internal static class EntitiesConfigurator
{
    internal static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameGenre>().HasOne(x => x.Game).WithMany(x => x.GameGenres).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<GamePlatform>().HasOne(x => x.Game).WithMany(x => x.GamePlatforms).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Platform>().HasMany(x => x.GamePlatforms).WithOne(x => x.Platform).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Genre>().HasMany(x => x.GameGenres).WithOne(x => x.Genre).OnDelete(DeleteBehavior.Restrict);
    }
}
