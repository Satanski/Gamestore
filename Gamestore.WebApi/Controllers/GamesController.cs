using System.Text.Json;
using Gamestore.BLL.Models;
using Gamestore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class GamesController([FromServices] IGameService gameService) : ControllerBase
{
    private readonly IGameService _gameService = gameService;

    // GET: games
    [HttpGet]
    public async Task<IActionResult> GetGamesAsync()
    {
        var games = await _gameService.GetAllGamesAsync();

        return games.Any() ? Ok(games) : NotFound();
    }

    // GET: games/find/GUID
    [HttpGet("find/{id}")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetGameByIdAsync(Guid id)
    {
        var game = await _gameService.GetGameByIdAsync(id);

        return game == null ? NotFound() : Ok(game);
    }

    // GET: games/STRING
    [HttpGet("{key}")]
    public async Task<IActionResult> GetGameByKeyAsync(string key)
    {
        var game = await _gameService.GetGameByKeyAsync(key);

        return game == null ? NotFound() : Ok(game);
    }

    // GET: games/GUID/genres
    [HttpGet("{key}/genres")]
    public async Task<IActionResult> GetGenresByGameKeyAsync(string key)
    {
        var genres = await _gameService.GetGenresByGameKeyAsync(key);

        return genres.Any() ? Ok(genres) : NotFound();
    }

    // GET: games/GUID/platforms
    [HttpGet("{key}/platforms")]
    public async Task<IActionResult> GetPlatformsByGameIdAsync(string key)
    {
        var platforms = await _gameService.GetPlatformsByGameKeyAsync(key);

        return platforms.Any() ? Ok(platforms) : NotFound();
    }

    // GET: games/GUID/publisher
    [HttpGet("{key}/publisher")]
    public async Task<IActionResult> GetPublisherByGameKeyAsync(string key)
    {
        var publisher = await _gameService.GetPublisherByGameKeyAsync(key);

        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(publisher);
    }

    // GET: games/STRING/file
    [HttpGet("{key}/file")]
    public async Task<IActionResult> DownloadGameAsync(string key)
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
    public async Task<IActionResult> AddGameAsync([FromBody] GameAddDto gameModel)
    {
        await _gameService.AddGameAsync(gameModel);

        return Ok();
    }

    // PUT: games
    [HttpPut]
    public async Task<IActionResult> UpdateGameAsync([FromBody] GameUpdateDto gameModel)
    {
        await _gameService.UpdateGameAsync(gameModel);

        return Ok();
    }

    // DELETE: games
    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteGameByKeyAsync(string key)
    {
        await _gameService.DeleteGameByKeyAsync(key);

        return Ok();
    }
}
