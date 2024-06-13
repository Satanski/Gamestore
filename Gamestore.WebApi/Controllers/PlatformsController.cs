using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Models;
using Gamestore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class PlatformsController([FromServices] IPlatformService platformService) : ControllerBase
{
    private readonly IPlatformService _platformService = platformService;

    // GET: platforms/GUID/games
    [HttpGet("{id}/games")]
    public async Task<IActionResult> GetGamesByPlatformIdAsync(Guid id)
    {
        var games = await _platformService.GetGamesByPlatformIdAsync(id);
        return games == null ? Ok(games) : BadRequest();
    }

    // POST: platforms
    [HttpPost]
    public async Task<IActionResult> AddPlatformAsync([FromBody] PlatformDtoWrapper platform)
    {
        await _platformService.AddPlatformAsync(platform);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlatformByIdAsync(Guid id)
    {
        var platform = await _platformService.GetPlatformByIdAsync(id);

        return platform != null ? Ok(platform) : NotFound();
    }

    // GET: platforms
    [HttpGet]
    public async Task<IActionResult> GetPlatformsAsync()
    {
        var platforms = await _platformService.GetAllPlatformsAsync();

        return platforms.Any() ? Ok(platforms) : NotFound();
    }

    // PUT: platforms
    [HttpPut]
    public async Task<IActionResult> UpdatePlatformAsync([FromBody] PlatformDtoWrapper platformModel)
    {
        await _platformService.UpdatePlatformAsync(platformModel);

        return Ok();
    }

    // DELETE: platforms
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlatformByIdAsync(Guid id)
    {
        await _platformService.DeletePlatformByIdAsync(id);

        return Ok();
    }
}
