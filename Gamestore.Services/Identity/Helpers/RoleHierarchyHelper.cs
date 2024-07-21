using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.BLL.Identity.Helpers;

internal class RoleHierarchyHelper
{
    private readonly RoleManager<AppRole> _roleManager;

    internal RoleHierarchyHelper(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    internal async Task<List<string>> GetEffectiveRolesAsync(AppUser user, UserManager<AppUser> userManager)
    {
        var roles = await userManager.GetRolesAsync(user);
        var allRoles = new HashSet<string>(roles);

        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                await AddParentRolesAsync(role, allRoles);
            }
        }

        return [.. allRoles];
    }

    private async Task AddParentRolesAsync(AppRole role, HashSet<string> allRoles)
    {
        while (role.ParentRoleId != null)
        {
            var parentRole = await _roleManager.FindByIdAsync(role.ParentRoleId);
            if (parentRole == null)
            {
                break;
            }

            if (!allRoles.Contains(parentRole.Name!))
            {
                allRoles.Add(parentRole.Name!);
            }

            role = parentRole;
        }
    }
}
