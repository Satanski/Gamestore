using Microsoft.AspNetCore.Identity;

namespace Gamestore.DAL.Entities;

public class AppUser : IdentityUser
{
    public ICollection<IdentityUserRole<string>> UserRoles { get; set; }
}
