using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGenreService
{
    Task AddGenreAsync(GenreModel genreModel);

    Task<IEnumerable<GameModel>> GetGamesByGenreAsync(Guid id);

    Task<GenreModel> GetGenreByIdAsync(Guid id);

    Task<IEnumerable<GenreModel>> GetAllGenresAsync();

    Task UpdateGenreAsync(DetailedGenreModel genreModel);

    Task DeleteGenreAsync(Guid id);

    Task<IEnumerable<GenreModel>> GetGenresByParentGenreAsync(Guid id);
}
