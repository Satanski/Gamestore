using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class CommentsController([FromServices] ICommentService commentService) : ControllerBase
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
    public IActionResult BanCustomer([FromBody] BanDto ban)
    {
        commentService.BanCustomerFromCommenting(ban);

        return Ok();
    }
}
