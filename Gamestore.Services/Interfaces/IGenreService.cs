using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGenreService
{
    Task AddGenreAsync(GenreModel genreModel);

    Task<IEnumerable<GameModel>> GetGamesByGenreAsync(Guid genreId);

    Task<GenreModel> GetGenreByIdAsync(Guid genreId);

    Task<IEnumerable<GenreModel>> GetAllGenresAsync();

    Task UpdateGenreAsync(DetailedGenreModel genreModel);

    Task DeleteGenreAsync(Guid genreId);

    Task<IEnumerable<GenreModel>> GetGenresByParentGenreAsync(Guid genreId);
}
