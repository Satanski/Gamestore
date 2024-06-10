using System.Text.Json;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.WebApi.Stubs;
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
        IEnumerable<GameModelDto> games;

        games = await _gameService.GetAllGamesAsync();

        return games.Any() ? Ok(games) : NotFound();
    }

    // GET: games/find/GUID
    [HttpGet("find/{id}")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetGameByIdAsync(Guid id)
    {
        GameModelDto game;

        game = await _gameService.GetGameByIdAsync(id);

        return game == null ? NotFound() : Ok(game);
    }

    // GET: games/STRING
    [HttpGet("{key}")]
    public async Task<IActionResult> GetGameByKeyAsync(string key)
    {
        GameModelDto game;

        game = await _gameService.GetGameByKeyAsync(key);

        return game == null ? NotFound() : Ok(game);
    }

    // GET: games/GUID/genres
    [HttpGet("{id}/genres")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetGenresByGameIdAsync(Guid id)
    {
        IEnumerable<GenreModelDto> genres;

        genres = await _gameService.GetGenresByGameIdAsync(id);

        return genres.Any() ? Ok(genres) : NotFound();
    }

    // GET: games/GUID/platforms
    [HttpGet("{id}/platforms")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetPlatformsByGameIdAsync(Guid id)
    {
        IEnumerable<PlatformModelDto> platforms;

        platforms = await _gameService.GetPlatformsByGameIdAsync(id);

        return platforms.Any() ? Ok(platforms) : NotFound();
    }

    // GET: games/GUID/publisher
    [HttpGet("{id}/publisher")]
    public async Task<IActionResult> GetPublisherByGameIdAsync(Guid id)
    {
        var publisher = await _gameService.GetPublisherByGameIdAsync(id);

        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(publisher);
    }

    // GET: games/STRING/file
    [HttpGet("{key}/file")]
    [ResponseCache(Duration = 1)]
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
    public async Task<IActionResult> AddGameAsync([FromBody] GameModel gameModel)
    {
        await _gameService.AddGameAsync(gameModel);

        return Ok();
    }

    // POST: games/STRING/buy
    [HttpPost("{key}/buy")]
    public async Task<IActionResult> AddGameToCartAsync(string key)
    {
        await _gameService.AddGameToCartAsync(CustomerStub.Id, key, 1);

        return Ok();
    }

    // PUT: games
    [HttpPut]
    public async Task<IActionResult> UpdateGameAsync([FromBody] GameUpdateModel gameModel)
    {
        await _gameService.UpdateGameAsync(gameModel);

        return Ok();
    }

    // DELETE: games
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGameByIdAsync(Guid id)
    {
        await _gameService.DeleteGameByIdAsync(id);

        return Ok();
    }
}
