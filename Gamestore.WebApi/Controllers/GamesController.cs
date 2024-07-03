using System.Text.Json;
using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Models;
using Gamestore.Services.Interfaces;
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
    public async Task<IActionResult> GetGamesAsync([FromQuery] GameFiltersDto filters)
    {
        var games = await _gameService.GetFilteredGamesAsync(filters);

        return Ok(games);
    }

    // GET: games/find/GUID
    [HttpGet("find/{id}")]
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

    // GET: games/STRING/comments
    [HttpGet("{key}/comments")]
    public async Task<IActionResult> GetGameCommentsAsync(string key)
    {
        var comments = await _gameService.GetCommentsByGameKeyAsync(key);

        return Ok(comments);
    }

    // GET: games/pagination-options
    [HttpGet("pagination-options")]
    public IActionResult GetPaginationOptions()
    {
        var paginationOptions = _gameService.GetPaginationOptions();

        return Ok(paginationOptions);
    }

    // GET: games/publish-date-options
    [HttpGet("publish-date-options")]
    public IActionResult GetPublishDateOptions()
    {
        var publishDateOptions = _gameService.GetPublishDateOptions();

        return Ok(publishDateOptions);
    }

    // GET: games/sorting-options
    [HttpGet("sorting-options")]
    public IActionResult GetSortingOptions()
    {
        var sortingOptions = _gameService.GetSortingOptions();

        return Ok(sortingOptions);
    }

    // POST: games
    [HttpPost]
    public async Task<IActionResult> AddGameAsync([FromBody] GameDtoWrapper gameModel)
    {
        await _gameService.AddGameAsync(gameModel);

        return Ok();
    }

    // POST: games/STRING/buy
    [HttpPost("{key}/buy")]
    public async Task<IActionResult> AddGameToCartAsync(string key)
    {
        var customerStub = new CustomerStub();
        await _gameService.AddGameToCartAsync(customerStub.Id, key, 1);

        return Ok();
    }

    // POST: games/STRING/comments
    [HttpPost("{key}/comments")]
    public async Task<IActionResult> AddCommentToGameAsync([FromBody] CommentModelDto comment, string key)
    {
        var result = await _gameService.AddCommentToGameAsync(key, comment);

        return Ok(result);
    }

    // PUT: games
    [HttpPut]
    public async Task<IActionResult> UpdateGameAsync([FromBody] GameDtoWrapper gameModel)
    {
        await _gameService.UpdateGameAsync(gameModel);

        return Ok();
    }

    // DELETE: games
    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteGameByKeyAsync(string key)
    {
        await _gameService.SoftDeleteGameByKeyAsync(key);

        return Ok();
    }

    // DELETE: games
    [HttpDelete("{key}/comments/{id}")]
    public async Task<IActionResult> DeleteCommentAsync(string key, Guid id)
    {
        await _gameService.DeleteCommentAsync(key, id);

        return Ok();
    }
}
