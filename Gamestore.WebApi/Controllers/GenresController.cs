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
    public async Task<IActionResult> GetGamesByGenreAsync(Guid id)
    {
        IEnumerable<GameModel> games;

        games = await _genreService.GetGamesByGenreAsync(id);

        if (games.Any())
        {
            return Ok(games);
        }

        return NotFound();
    }

    // GET: genres/GUID/games
    [HttpGet("{id}/genres")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetGenresByParentGenreAsync(Guid id)
    {
        IEnumerable<GenreModel> genres;

        genres = await _genreService.GetGenresByParentGenreAsync(id);

        if (genres.Any())
        {
            return Ok(genres);
        }

        return NotFound();
    }

    // POST: genres
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] GenreModel genreModel)
    {
        await _genreService.AddGenreAsync(genreModel);

        return Ok();
    }

    // GET: genres/GUID
    [HttpGet("{id}")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        GenreModel genre;

        genre = await _genreService.GetGenreByIdAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    // GET: genres
    [HttpGet]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetAsync()
    {
        IEnumerable<GenreModel> genres;

        genres = await _genreService.GetAllGenresAsync();

        if (genres.Any())
        {
            return Ok(genres);
        }

        return NotFound();
    }

    // PUT: genres
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] GenreModelDto genreModel)
    {
        await _genreService.UpdateGenreAsync(genreModel);

        return Ok();
    }

    // DELETE: genres
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _genreService.DeleteGenreAsync(id);

        return Ok();
    }
}
