using System.Text.Json;
using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Identity.Extensions;
using Gamestore.BLL.Identity.Models;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class GamesController([FromServices] IGameService gameService, UserManager<AppUser> userManager, IConfiguration configuration) : ControllerBase
{
    private readonly IGameService _gameService = gameService;

    // GET: games
    [HttpGet]
    public async Task<IActionResult> GetGamesAsync([FromQuery] GameFiltersDto filters)
    {
        bool canSeeDeletedGames = false;
        if (User.Claims.Any(x => x.Value == Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionDeletedGames)))
        {
            canSeeDeletedGames = true;
        }

        var games = await _gameService.GetFilteredGamesAsync(filters, canSeeDeletedGames);

        return Ok(games);
    }

    [HttpGet("all")]
    [Authorize(Policy = Permissions.PermissionValueManageEntitiesOrDeletedGames)]
    public async Task<IActionResult> GetAllGamesAsync()
    {
        List<GameModelDto> games;
        bool canSeeDeletedGames = false;
        if (User.Claims.Any(x => x.Value == Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionDeletedGames)))
        {
            canSeeDeletedGames = true;
        }

        games = await _gameService.GetAllGamesAsync(canSeeDeletedGames);

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

    // GET: games/Key/image
    [HttpGet("{key}/image")]
    public async Task<IActionResult> GetPictureByGameKeyAsync(string key)
    {
        var picture = await _gameService.GetPictureByGameKeyAsync(key, configuration);
        var mimeType = MimeTypeHelpers.GetMimeTypeFromBytes(picture);

        return File(picture, mimeType);
    }

    // GET: games/GUID/platforms
    [HttpGet("{key}/platforms")]
    public async Task<IActionResult> GetPlatformsByGameIdAsync(string key)
    {
        var platforms = await _gameService.GetPlatformsByGameKeyAsync(key);

        return Ok(platforms);
    }

    // GET: games/GUID/publisher
    [HttpGet("{key}/publisher")]
    public async Task<IActionResult> GetPublisherByGameKeyAsync(string key)
    {
        var publisher = await _gameService.GetPublisherByGameKeyAsync(key);

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
    [Authorize(Policy = Permissions.PermissionValueManageEntities)]
    public async Task<IActionResult> AddGameAsync([FromBody] GameDtoWrapper gameModel)
    {
        await _gameService.AddGameAsync(gameModel, configuration);

        return Ok();
    }

    // POST: games/STRING/buy
    [HttpPost("{key}/buy")]
    [Authorize(Policy = Permissions.PermissionValueBuyGame)]
    public async Task<IActionResult> AddGameToCartAsync(string key)
    {
        var userId = new Guid(User.GetJwtSubjectId());
        await _gameService.AddGameToCartAsync(userId, key, 1);

        return Ok();
    }

    // POST: games/STRING/comments
    [HttpPost("{key}/comments")]
    public async Task<IActionResult> AddCommentToGameAsync([FromBody] CommentModelDto comment, string key)
    {
        var userName = User.GetJwtSubject();
        var result = await _gameService.AddCommentToGameAsync(userName, key, comment, userManager);

        return Ok(result);
    }

    // PUT: games
    [HttpPut]
    [Authorize(Policy = Permissions.PermissionValueManageEntities)]
    public async Task<IActionResult> UpdateGameAsync([FromBody] GameDtoWrapper gameModel)
    {
        await _gameService.UpdateGameAsync(gameModel, configuration);

        return Ok();
    }

    // DELETE: games
    [HttpDelete("{key}")]
    [Authorize(Policy = Permissions.PermissionValueManageEntities)]
    public async Task<IActionResult> DeleteGameByKeyAsync(string key)
    {
        await _gameService.DeleteGameByKeyAsync(key, configuration);

        return Ok();
    }

    // DELETE: games
    [HttpDelete("{key}/comments/{id}")]
    [Authorize(Policy = Permissions.PermissionValueDeleteComment)]
    public async Task<IActionResult> DeleteCommentAsync(string key, Guid id)
    {
        bool canModerate = false;
        var userName = User.GetJwtSubject();
        if (User.Claims.Any(x => x.Value == Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionModerateComments)))
        {
            canModerate = true;
        }

        await _gameService.DeleteCommentAsync(userName, key, id, canModerate);

        return Ok();
    }
}
