using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.DAL.EntityConfigurations;

internal class GamePlatformConfiguration : IEntityTypeConfiguration<ProductPlatform>
{
    public void Configure(EntityTypeBuilder<ProductPlatform> builder)
    {
        builder.HasOne(x => x.Product).WithMany(x => x.ProductPlatforms).OnDelete(DeleteBehavior.Restrict);
    }
}
