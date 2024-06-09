using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.DAL.EntityConfigurations;

internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<Game> builder)
    {
        builder.HasData(new Game()
        {
            Id = Guid.NewGuid(),
            Name = "Baldurs Gate",
            Description = "Rpg game",
            Key = "BG",
            Discount = 10,
            Price = 250,
            UnitInStock = 15,
            PublisherId = new Guid("00000000-0000-0000-0000-000000000000"),
        });

        builder.HasData(new Game()
        {
            Id = Guid.NewGuid(),
            Name = "Tedt Drive",
            Description = "Racing game",
            Key = "TD",
            Discount = 0,
            Price = 150,
            UnitInStock = 2,
            PublisherId = new Guid("11111111-1111-1111-1111-111111111111"),
        });
    }
}
