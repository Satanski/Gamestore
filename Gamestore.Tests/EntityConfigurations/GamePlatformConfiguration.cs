using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.Tests.EntityConfigurations;

internal class GamePlatformConfiguration : IEntityTypeConfiguration<GamePlatform>
{
    public void Configure(EntityTypeBuilder<GamePlatform> builder)
    {
        builder.HasOne(x => x.Game).WithMany(x => x.GamePlatforms).OnDelete(DeleteBehavior.Restrict);
    }
}
