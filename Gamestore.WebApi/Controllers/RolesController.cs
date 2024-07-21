using Gamestore.BLL.Identity.Models;
using Gamestore.BLL.Interfaces;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class RolesController(IRoleService roleService, RoleManager<AppRole> roleManager) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "ManageRoles")]
    public IActionResult GetRoles()
    {
        var roles = roleService.GetAllRoles(roleManager);

        return Ok(roles);
    }

    [HttpGet("{roleId}")]
    [Authorize(Policy = "ManageRoles")]
    public async Task<IActionResult> GetRoleAsync(Guid roleId)
    {
        var result = await roleService.GetRoleByIdAsync(roleManager, roleId);

        return Ok(result);
    }

    [HttpGet("permissions")]
    [Authorize(Policy = "ManageRoles")]
    public IActionResult GetAllPermissions()
    {
        var roles = roleService.GetAllPermissions();

        return Ok(roles);
    }

    [HttpGet("{roleId}/permissions")]
    [Authorize(Policy = "ManageRoles")]
    public async Task<IActionResult> GetRolePermissionsAsync(Guid roleId)
    {
        var permissions = await roleService.GetPermissionsByRoleIdAsync(roleManager, roleId);

        return Ok(permissions);
    }

    [HttpDelete("{roleId}")]
    [Authorize(Policy = "ManageRoles")]
    public async Task<IdentityResult> DeleteRoleAsync(Guid roleId)
    {
        var result = await roleService.DeleteRoleByIdAsync(roleManager, roleId);

        return result;
    }

    [HttpPost]
    [Authorize(Policy = "ManageRoles")]
    public async Task<IdentityResult> AddRoleAsync(RoleModelDto role)
    {
        var result = await roleService.AddRoleAsync(roleManager, role);

        return result;
    }

    [HttpPut]
    [Authorize(Policy = "ManageRoles")]
    public async Task<IdentityResult> UpdateRoleAsync(RoleModelDto role)
    {
        var result = await roleService.UpdateRoleAsync(roleManager, role);

        return result;
    }
}
