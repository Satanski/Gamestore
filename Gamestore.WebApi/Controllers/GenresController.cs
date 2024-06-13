using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class GenresController([FromServices] IGenreService genreService) : ControllerBase
{
    private readonly IGenreService _genreService = genreService;

    // GET: genres/GUID/games
    [HttpGet("{id}/games")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetGamesByGenreIdAsync(Guid id)
    {
        IEnumerable<GameModelDto> games;

        games = await _genreService.GetGamesByGenreAsync(id);

        return games.Any() ? Ok(games) : NotFound();
    }

    // GET: genres/GUID/games
    [HttpGet("{id}/genres")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetGenresByParentGenreIdAsync(Guid id)
    {
        IEnumerable<GenreModelDto> genres;

        genres = await _genreService.GetGenresByParentGenreAsync(id);

        return genres.Any() ? Ok(genres) : NotFound();
    }

    // POST: genres
    [HttpPost]
    public async Task<IActionResult> AddGenreAsync([FromBody] GenreModelDto genreModel)
    {
        await _genreService.AddGenreAsync(genreModel);

        return Ok();
    }

    // GET: genres/GUID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenreByIdAsync(Guid id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);

        return genre == null ? NotFound() : Ok(genre);
    }

    // GET: genres
    [HttpGet]
    public async Task<IActionResult> GetGenresAsync()
    {
        var genres = await _genreService.GetAllGenresAsync();

        return genres.Any() ? Ok(genres) : NotFound();
    }

    // PUT: genres
    [HttpPut]
    public async Task<IActionResult> UpdateGenreAsync([FromBody] GenreModel genreModel)
    {
        await _genreService.UpdateGenreAsync(genreModel);

        return Ok();
    }

    // DELETE: genres
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenreByIdAsync(Guid id)
    {
        await _genreService.DeleteGenreAsync(id);

        return Ok();
    }
}
