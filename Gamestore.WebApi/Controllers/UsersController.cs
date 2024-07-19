using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController(IUserService userService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        var customers = userService.GetAllUsers(userManager);

        return Ok(customers);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDto login)
    {
        var token = await userService.LoginAsync(userManager, roleManager, configuration, login);
        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(new { Token = token });
    }

    [HttpPost("access")]
    public IActionResult Access(AccessModelDto access)
    {
        return Ok(access);
    }
}
