using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.DAL.EntityConfigurations;

internal class PublisherConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasMany(x => x.Products).WithOne(x => x.Publisher).OnDelete(DeleteBehavior.Restrict);
        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasData(new Supplier() { Id = new Guid("22222222-2222-2222-2222-222222222222"), CompanyName = "Elecrtonic Arts", HomePage = "www.ea.com" });
        builder.HasData(new Supplier() { Id = new Guid("11111111-1111-1111-1111-111111111111"), CompanyName = "Blizzard", HomePage = "www.blizzard.com" });
    }
}
