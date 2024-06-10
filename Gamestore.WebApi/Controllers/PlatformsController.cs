using Gamestore.BLL.Exceptions;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class PlatformsController([FromServices] IPlatformService platformService) : ControllerBase
{
    private readonly IPlatformService _platformService = platformService;

    // GET: platforms/GUID/games
    [HttpGet("{id}/games")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetGamesByPlatformIdAsync(Guid id)
    {
        IEnumerable<GameModelDto> games;

        try
        {
            games = await _platformService.GetGamesByPlatformIdAsync(id);
        }
        catch (GamestoreException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok(games);
    }

    // POST: platforms
    [HttpPost]
    public async Task<IActionResult> AddPlatformAsync([FromBody] PlatformModel platformModel)
    {
        try
        {
            await _platformService.AddPlatformAsync(platformModel);
        }
        catch (GamestoreException)
        {
            return BadRequest();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok();
    }

    // GET: platforms/GUID
    [HttpGet("{id}")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetPlatformByIdAsync(Guid id)
    {
        var platform = await _platformService.GetPlatformByIdAsync(id);

        return platform == null ? NotFound() : Ok(platform);
    }

    // GET: platforms
    [HttpGet]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetPlatformsAsync()
    {
        var platforms = await _platformService.GetAllPlatformsAsync();

        return platforms.Any() ? Ok(platforms) : NotFound();
    }

    // PUT: platforms
    [HttpPut]
    public async Task<IActionResult> UpdatePlatformAsync([FromBody] PlatformModel platformModel)
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
