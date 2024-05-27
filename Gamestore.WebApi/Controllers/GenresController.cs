using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Validation;
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

        try
        {
            games = await _genreService.GetGamesByGenreAsync(id);
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

    // GET: genres/GUID/games
    [HttpGet("{id}/genres")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetGenresByParentGenreAsync(Guid id)
    {
        IEnumerable<GenreModel> genres;

        try
        {
            genres = await _genreService.GetGenresByParentGenreAsync(id);
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

    // POST: genres
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] GenreModel genreModel)
    {
        try
        {
            await _genreService.AddGenreAsync(genreModel);
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

    // GET: genres/GUID
    [HttpGet("{id}")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        GenreModel genre;

        try
        {
            genre = await _genreService.GetGenreByIdAsync(id);
        }
        catch (GamestoreException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok(genre);
    }

    // GET: genres
    [HttpGet]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetAsync()
    {
        IEnumerable<GenreModel> genres;

        try
        {
            genres = await _genreService.GetAllGenresAsync();
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

    // PUT: genres
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] GenreModelDto genreModel)
    {
        try
        {
            await _genreService.UpdateGenreAsync(genreModel);
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

    // DELETE: genres
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _genreService.DeleteGenreAsync(id);
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
