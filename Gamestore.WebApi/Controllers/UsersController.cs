using Gamestore.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModelDto login)
    {
        if (login == null)
        {
            return BadRequest();
        }

        return Ok(new TokenModelDto() { Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" });
    }

    [HttpPost("access")]
    public IActionResult Access(AccessModelDto access)
    {
        return Ok(access);
    }
}
