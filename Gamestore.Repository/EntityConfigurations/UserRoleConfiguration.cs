using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.DAL.EntityConfigurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasOne(x => x.User).WithMany(x => x.UserRoles).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Role).WithMany(x => x.UserRoles).OnDelete(DeleteBehavior.Restrict);
    }
}
