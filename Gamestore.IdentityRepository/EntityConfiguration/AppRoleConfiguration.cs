using Gamestore.IdentityRepository.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.IdentityRepository.EntityConfigurations;

internal class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.HasOne(r => r.ParentRole).WithMany().HasForeignKey(r => r.ParentRoleId).OnDelete(DeleteBehavior.Restrict);
    }
}
