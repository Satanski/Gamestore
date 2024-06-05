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
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetAsync()
    {
        IEnumerable<GameModel> games;

        games = await _gameService.GetAllGamesAsync();

        if (games.Any())
        {
            return Ok(games);
        }

        return NotFound();
    }

    // GET: games/find/GUID
    [HttpGet("find/{id}")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        GameModel game;

        game = await _gameService.GetGameByIdAsync(id);

        if (game == null)
        {
            return NotFound();
        }

        return Ok(game);
    }

    // GET: games/STRING
    [HttpGet("{key}")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetAsync(string key)
    {
        GameModel game;

        game = await _gameService.GetGameByKeyAsync(key);

        if (game == null)
        {
            return NotFound();
        }

        return Ok(game);
    }

    // GET: games/GUID/genres
    [HttpGet("{id}/genres")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetGenresByGameAsync(Guid id)
    {
        IEnumerable<GenreModelDto> genres;

        genres = await _gameService.GetGenresByGameIdAsync(id);

        if (genres.Any())
        {
            return Ok(genres);
        }

        return NotFound();
    }

    // GET: games/GUID/platforms
    [HttpGet("{id}/platforms")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetPlatformsByGameAsync(Guid id)
    {
        IEnumerable<PlatformModelDto> platforms;

        platforms = await _gameService.GetPlatformsByGameIdAsync(id);

        if (platforms.Any())
        {
            return Ok(platforms);
        }

        return NotFound();
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
    public async Task<IActionResult> AddAsync([FromBody] GameModelDto gameModel)
    {
        await _gameService.AddGameAsync(gameModel);

        return Ok();
    }

    // PUT: games
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] GameModelDto gameModel)
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
