using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class RolesController(IRoleService roleService, RoleManager<AppRole> roleManager) : ControllerBase
{
    [HttpGet]
    public IActionResult GetRoles()
    {
        var roles = roleService.GetAllRoles(roleManager);

        return Ok(roles);
    }

    [HttpGet("{roleId}")]
    public async Task<IActionResult> GetRole(Guid roleId)
    {
        var result = await roleService.GetRoleByIdAsync(roleManager, roleId);

        return Ok(result);
    }

    [HttpGet("permissions")]
    public IActionResult GetAllPermissions()
    {
        var roles = roleService.GetAllPermissions();

        return Ok(roles);
    }

    [HttpGet("{roleId}/permissions")]
    public async Task<IActionResult> GetRolePermissions(Guid roleId)
    {
        var permissions = await roleService.GetPermissionsByRoleIdAsync(roleManager, roleId);

        return Ok(permissions);
    }

    [HttpDelete("{roleId}")]
    public async Task<IdentityResult> DeleteRole(Guid roleId)
    {
        var result = await roleService.DeleteRoleByIdAsync(roleManager, roleId);

        return result;
    }

    [HttpPost]
    public async Task<IdentityResult> AddRole(RoleModelDto role)
    {
        var result = await roleService.AddRoleAsync(roleManager, role);

        return result;
    }

    [HttpPut]
    public async Task<IdentityResult> UpdateRole(RoleModelDto role)
    {
        var result = await roleService.UpdateRoleAsync(roleManager, role);

        return result;
    }
}
