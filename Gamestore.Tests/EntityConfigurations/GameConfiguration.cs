using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.Tests.EntityConfigurations;

internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<Game> builder)
    {
        builder.HasData(new Game() { Id = Guid.NewGuid(), Name = "Test Drive", Description = "Racing game", Key = "TD" });
    }
}
