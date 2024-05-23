using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Validation;
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
    public async Task<ActionResult<GameModel>> GetGamesByPlatform(Guid id)
    {
        IEnumerable<GameModel> games;

        try
        {
            games = await _platformService.GetGamesByPlatformAsync(id);
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
    public async Task<ActionResult> Add([FromBody] PlatformModel platformModel)
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
    public async Task<ActionResult<PlatformModel>> Get(Guid id)
    {
        PlatformModel platform;

        try
        {
            platform = await _platformService.GetPlatformByIdAsync(id);
        }
        catch (GamestoreException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok(platform);
    }

    // GET: platforms
    [HttpGet]
    [ResponseCache(Duration = 1)]
    public async Task<ActionResult<IEnumerable<PlatformModel>>> Get()
    {
        IEnumerable<PlatformModel> platforms;

        try
        {
            platforms = await _platformService.GetAllPlatformsAsync();
        }
        catch (GamestoreException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok(platforms);
    }

    // PUT: platforms
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] DetailedPlatformModel platformModel)
    {
        try
        {
            await _platformService.UpdatePlatformAsync(platformModel);
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

    // DELETE: platforms
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _platformService.DeletePlatformAsync(id);
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
}
