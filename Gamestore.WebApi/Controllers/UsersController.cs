using Gamestore.BLL.Identity.Models;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController(IUserService userService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "ManageUsers")]
    public IActionResult GetUsers()
    {
        var customers = userService.GetAllUsers(userManager);

        return Ok(customers);
    }

    [HttpGet("{userId}")]
    [Authorize(Policy = "ManageUsers")]
    public async Task<IActionResult> GetUserAsync(string userId)
    {
        var customers = await userService.GetUserByIdAsync(userManager, userId);

        return Ok(customers);
    }

    [HttpGet("{userId}/roles")]
    [Authorize(Policy = "ManageUsers")]
    public async Task<IActionResult> GetUserRolesAsync(string userId)
    {
        var roles = await userService.GetUserRolesByUserId(userManager, roleManager, userId);

        return Ok(roles);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModelDto login)
    {
        var token = await userService.LoginAsync(userManager, roleManager, configuration, login);

        return Ok(new { Token = token });
    }

    [HttpPost("access")]
    public IActionResult Access(AccessModelDto access)
    {
        return Ok(access);
    }

    [HttpPost]
    [Authorize(Policy = "ManageUsers")]
    public async Task<IActionResult> AddUserAsync(UserDto user)
    {
        await userService.AddUserAsync(userManager, roleManager, user);

        return Ok();
    }

    [HttpPut]
    [Authorize(Policy = "ManageUsers")]
    public async Task<IdentityResult> UpdateUserAsync(UserDto user)
    {
        var result = await userService.UpdateUserAsync(userManager, roleManager, user);

        return result;
    }

    [HttpDelete("{userId}")]
    [Authorize(Policy = "ManageUsers")]
    public async Task<IActionResult> DeleteUserAsync(string userId)
    {
        await userService.DeleteUserAsync(userManager, userId);

        return Ok();
    }
}
