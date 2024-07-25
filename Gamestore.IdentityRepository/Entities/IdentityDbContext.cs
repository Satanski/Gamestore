using System.Reflection;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.IdentityRepository.Entities;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<AppUser, AppRole, string>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
        builder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
        builder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
