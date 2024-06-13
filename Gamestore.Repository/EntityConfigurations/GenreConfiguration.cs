using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.DAL.EntityConfigurations;

internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasMany(x => x.GameGenres).WithOne(x => x.Genre).OnDelete(DeleteBehavior.Restrict);
        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<Genre> builder)
    {
        var strategyGuid = Guid.NewGuid();
        builder.HasData(new Genre() { Id = strategyGuid, Name = "Strategy" });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "RTS", ParentGenreId = strategyGuid });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "TBS", ParentGenreId = strategyGuid });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "RPG" });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "Sports" });
        var racesGuid = Guid.NewGuid();
        builder.HasData(new Genre() { Id = racesGuid, Name = "Races" });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "Rally", ParentGenreId = racesGuid });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "Arcade", ParentGenreId = racesGuid });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "Formula", ParentGenreId = racesGuid });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "Off-road", ParentGenreId = racesGuid });
        var actionGuid = Guid.NewGuid();
        builder.HasData(new Genre() { Id = actionGuid, Name = "Action" });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "FPS", ParentGenreId = actionGuid });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "TPS", ParentGenreId = actionGuid });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "Adventure" });
        builder.HasData(new Genre() { Id = Guid.NewGuid(), Name = "Puzzle & Skill" });
    }
}
