using Microsoft.AspNetCore.Identity;

namespace Gamestore.IdentityRepository.Identity;

public class AppRole : IdentityRole
{
    public string? ParentRoleId { get; set; }

    public virtual AppRole? ParentRole { get; set; }
}
