using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.DAL.EntityConfigurations;

internal class GameConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Baldurs Gate",
            Description = "Rpg game",
            Key = "BG",
            Discount = 10,
            Price = 250,
            UnitInStock = 15,
            PublisherId = new Guid("22222222-2222-2222-2222-222222222222"),
        });

        builder.HasData(new Product()
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
