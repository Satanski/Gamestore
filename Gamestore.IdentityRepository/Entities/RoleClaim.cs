using Microsoft.AspNetCore.Identity;

namespace Gamestore.IdentityRepository;

public class RoleClaim : IdentityRoleClaim<string>
{
    public virtual IdentityRole Role { get; set; }
}
