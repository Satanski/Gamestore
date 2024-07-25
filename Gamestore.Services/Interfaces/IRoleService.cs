using Gamestore.BLL.Identity.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.BLL.Interfaces;

public interface IRoleService
{
    List<RoleModel> GetAllRoles(RoleManager<AppRole> roleManager);

    List<string> GetAllPermissions();

    Task<IdentityResult> AddRoleAsync(RoleManager<AppRole> roleManager, RoleModelDto role);

    Task<IdentityResult> DeleteRoleByIdAsync(RoleManager<AppRole> roleManager, Guid roleId);

    Task<RoleModel> GetRoleByIdAsync(RoleManager<AppRole> roleManager, Guid roleId);

    Task<List<string>> GetPermissionsByRoleIdAsync(RoleManager<AppRole> roleManager, Guid roleId);

    Task<IdentityResult> UpdateRoleAsync(RoleManager<AppRole> roleManager, RoleModelDto roleDto);
}
