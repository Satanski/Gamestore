using Gamestore.BLL.Identity.Models;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Notifications;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController(IUserService userService, UserManager<AppUser> userManager) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = Permissions.PermissionValueManageUsers)]
    public IActionResult GetUsers()
    {
        var customers = userService.GetAllUsers();

        return Ok(customers);
    }

    [HttpGet("{userId}")]
    [Authorize(Policy = Permissions.PermissionValueManageUsers)]
    public async Task<IActionResult> GetUserAsync(string userId)
    {
        var customers = await userService.GetUserByIdAsync(userId);

        return Ok(customers);
    }

    [HttpGet("{userId}/roles")]
    [Authorize(Policy = Permissions.PermissionValueManageUsers)]
    public async Task<IActionResult> GetUserRolesAsync(string userId)
    {
        var roles = await userService.GetUserRolesByUserId(userId);

        return Ok(roles);
    }

    [HttpGet("notifications")]
    public IActionResult GetNotificationMethods()
    {
        return Ok(userService.GetNotificationMethods());
    }

    [HttpGet("my/notifications")]
    public async Task<IActionResult> GetUserNotificationMethodsAsync()
    {
        var user = await userManager.GetUserAsync(User);
        return Ok(userService.GetUserNotificationMethods(user!));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModelDto login)
    {
        var token = await userService.LoginAsync(login);

        return Ok(new { Token = token });
    }

    [HttpPost("access")]
    public IActionResult Access(AccessModelDto access)
    {
        return Ok(access);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.PermissionValueManageUsers)]
    public async Task<IActionResult> AddUserAsync(UserDto user)
    {
        await userService.AddUserAsync(user);

        return Ok();
    }

    [HttpPut]
    [Authorize(Policy = Permissions.PermissionValueManageUsers)]
    public async Task<IdentityResult> UpdateUserAsync(UserDto user)
    {
        var result = await userService.UpdateUserAsync(user);

        return result;
    }

    [HttpPut("notifications")]
    public async Task<IActionResult> SetUserNotificationMethodsAsync(NotificationsDto notifications)
    {
        var user = await userManager.GetUserAsync(User);
        await userService.SetUserNotificationMethodsAsync(notifications, user!);

        return Ok();
    }

    [HttpDelete("{userId}")]
    [Authorize(Policy = Permissions.PermissionValueManageUsers)]
    public async Task<IActionResult> DeleteUserAsync(string userId)
    {
        await userService.DeleteUserAsync(userId);

        return Ok();
    }
}
