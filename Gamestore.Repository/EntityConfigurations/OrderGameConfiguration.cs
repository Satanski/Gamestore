using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.DAL.EntityConfigurations;

internal class OrderGameConfiguration : IEntityTypeConfiguration<OrderGame>
{
    public void Configure(EntityTypeBuilder<OrderGame> builder)
    {
        builder.HasOne(x => x.Order).WithMany(x => x.OrderGames).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Product).WithMany(x => x.OrderGames).OnDelete(DeleteBehavior.Restrict);
    }
}
