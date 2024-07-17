using Microsoft.AspNetCore.Identity;

namespace Gamestore.WebApi.Identity;

public class AppUser : IdentityUser
{
    public ICollection<IdentityUserRole<string>> UserRoles { get; set; }
}
