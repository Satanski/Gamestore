using System.Text.Json;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class GamesController([FromServices] IGameService gameService) : ControllerBase
{
    private readonly IGameService _gameService = gameService;

    // GET: games
    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<IEnumerable<GameModel>>> Get()
    {
        IEnumerable<GameModel> games;

        try
        {
            games = await _gameService.GetAllGamesAsync();
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

    // GET: games/find/GUID
    [HttpGet("find/{id}")]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<GameModel>> Get(Guid id)
    {
        GameModel game;

        try
        {
            game = await _gameService.GetGameByIdAsync(id);
        }
        catch (GamestoreException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok(game);
    }

    // GET: games/STRING
    [HttpGet("{key}")]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<GameModel>> Get(string key)
    {
        GameModel game;

        try
        {
            game = await _gameService.GetGameByKeyAsync(key);
        }
        catch (GamestoreException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok(game);
    }

    // GET: games/GUID/genres
    [HttpGet("{id}/genres")]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<IEnumerable<DetailedGenreModel>>> GetGenresByGame(Guid id)
    {
        IEnumerable<DetailedGenreModel> genres;

        try
        {
            genres = await _gameService.GetGenresByGameAsync(id);
        }
        catch (GamestoreException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok(genres);
    }

    // GET: games/GUID/platforms
    [HttpGet("{id}/platforms")]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<IEnumerable<DetailedPlatformModel>>> GetPlatformsByGame(Guid id)
    {
        IEnumerable<DetailedPlatformModel> platforms;

        try
        {
            platforms = await _gameService.GetPlatformsByGameAsync(id);
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

    // https://localhost:44394/games/baldursgate/file
    // GET: games/STRING/file
    [HttpGet("{key}/file")]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult> Download(string key)
    {
        string fileName;
        byte[] serialized;

        try
        {
            var game = await _gameService.GetGameByKeyAsync(key);

            fileName = $"{game.Name}_{DateTime.Now}";
            serialized = JsonSerializer.SerializeToUtf8Bytes(game);
        }
        catch (GamestoreException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return File(serialized, "application/octet-stream", fileName);
    }

    // POST: games
    [HttpPost]
    public async Task<ActionResult> Add([FromBody] DetailedGameModel gameModel)
    {
        try
        {
            await _gameService.AddGameAsync(gameModel);
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

    // PUT: games
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] DetailedGameModel gameModel)
    {
        try
        {
            await _gameService.UpdateGameAsync(gameModel);
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

    // DELETE: games
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _gameService.DeleteGameAsync(id);
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
