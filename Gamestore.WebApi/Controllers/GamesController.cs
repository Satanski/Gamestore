using System.Text.Json;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class GamesController([FromServices] IGameService gameService) : ControllerBase
{
    private readonly IGameService _gameService = gameService;

    // GET: games
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        IEnumerable<GameModelDto> games;

        games = await _gameService.GetAllGamesAsync();

        return games.Any() ? Ok(games) : NotFound();
    }

    // GET: games/find/GUID
    [HttpGet("find/{id}")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        GameModelDto game;

        game = await _gameService.GetGameByIdAsync(id);

        return game == null ? NotFound() : Ok(game);
    }

    // GET: games/STRING
    [HttpGet("{key}")]
    public async Task<IActionResult> GetAsync(string key)
    {
        GameModelDto game;

        game = await _gameService.GetGameByKeyAsync(key);

        return game == null ? NotFound() : Ok(game);
    }

    // GET: games/GUID/genres
    [HttpGet("{id}/genres")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetGenresByGameAsync(Guid id)
    {
        IEnumerable<GenreModelDto> genres;

        genres = await _gameService.GetGenresByGameIdAsync(id);

        return genres.Any() ? Ok(genres) : NotFound();
    }

    // GET: games/GUID/platforms
    [HttpGet("{id}/platforms")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetPlatformsByGameAsync(Guid id)
    {
        IEnumerable<PlatformModelDto> platforms;

        platforms = await _gameService.GetPlatformsByGameIdAsync(id);

        return platforms.Any() ? Ok(platforms) : NotFound();
    }

    // GET: games/GUID/publisher
    [HttpGet("{id}/publisher")]
    public async Task<IActionResult> GetPublisherByGameAsync(Guid id)
    {
        var publisher = await _gameService.GetPublisherByGameIdAsync(id);

        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(publisher);
    }

    // https://localhost:44394/games/baldursgate/file
    // GET: games/STRING/file
    [HttpGet("{key}/file")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> DownloadAsync(string key)
    {
        string fileName;
        byte[] serialized;

        var game = await _gameService.GetGameByKeyAsync(key);

        if (game == null)
        {
            return NotFound();
        }

        fileName = $"{game.Name}_{DateTime.Now}.txt";
        serialized = JsonSerializer.SerializeToUtf8Bytes(game);

        return File(serialized, "txt/json", fileName);
    }

    // POST: games
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] GameModel gameModel)
    {
        await _gameService.AddGameAsync(gameModel);

        return Ok();
    }

    // PUT: games
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] GameUpdateModel gameModel)
    {
        await _gameService.UpdateGameAsync(gameModel);

        return Ok();
    }

    // DELETE: games
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _gameService.DeleteGameByIdAsync(id);

        return Ok();
    }
}
