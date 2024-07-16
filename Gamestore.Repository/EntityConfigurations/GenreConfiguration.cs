using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.DAL.EntityConfigurations;

internal class GenreConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasMany(x => x.ProductGenres).WithOne(x => x.Category).OnDelete(DeleteBehavior.Restrict);
        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<Category> builder)
    {
        var strategyGuid = Guid.NewGuid();
        builder.HasData(new Category() { Id = strategyGuid, Name = "Strategy" });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "RTS", ParentCategoryId = strategyGuid });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "TBS", ParentCategoryId = strategyGuid });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "RPG" });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "Sports" });
        var racesGuid = Guid.NewGuid();
        builder.HasData(new Category() { Id = racesGuid, Name = "Races" });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "Rally", ParentCategoryId = racesGuid });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "Arcade", ParentCategoryId = racesGuid });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "Formula", ParentCategoryId = racesGuid });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "Off-road", ParentCategoryId = racesGuid });
        var actionGuid = Guid.NewGuid();
        builder.HasData(new Category() { Id = actionGuid, Name = "Action" });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "FPS", ParentCategoryId = actionGuid });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "TPS", ParentCategoryId = actionGuid });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "Adventure" });
        builder.HasData(new Category() { Id = Guid.NewGuid(), Name = "Puzzle & Skill" });
    }
}
