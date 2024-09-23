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
        builder.Entity<UserNotificationMethod>().HasKey(x => new { x.UserId, x.NotificationType });
        builder.Entity<UserNotificationMethod>()
            .HasOne(x => x.User)
            .WithMany(x => x.NotificationMethods)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<AppUser>()
        .Navigation(u => u.NotificationMethods)
        .AutoInclude();
        builder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
        builder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
        builder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
