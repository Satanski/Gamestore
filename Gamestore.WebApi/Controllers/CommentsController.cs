using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class CommentsController([FromServices] ICommentService commentService, UserManager<AppUser> userManager) : ControllerBase
{
    // GET: comments/ban/durations
    [HttpGet("ban/durations")]
    public IActionResult GetBanDurations()
    {
        var banDurations = commentService.GetBanDurations();

        return Ok(banDurations);
    }

    // POST: comments/ban
    [HttpPost("ban")]
    [Authorize(Policy = "BanUsers")]
    public async Task<IActionResult> BanCustomerAsync([FromBody] BanDto ban)
    {
        await commentService.BanCustomerFromCommentingAsync(ban, userManager);

        return Ok();
    }
}
