using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.Tests.EntityConfigurations;

internal class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.HasMany(x => x.GamePlatforms).WithOne(x => x.Platform).OnDelete(DeleteBehavior.Restrict);
        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<Platform> builder)
    {
        builder.HasData(new Platform() { Id = Guid.NewGuid(), Type = "Mobile" });
        builder.HasData(new Platform() { Id = Guid.NewGuid(), Type = "Browser" });
        builder.HasData(new Platform() { Id = Guid.NewGuid(), Type = "Desktop" });
        builder.HasData(new Platform() { Id = Guid.NewGuid(), Type = "Console" });
    }
}
