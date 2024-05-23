using Gamestore.Repository.Entities;

namespace Gamestore.Repository.Interfaces;

public interface IGenreRepository
{
    Task AddGenreAsync(Genre genre);

    Task<IEnumerable<Game>> GetGamesByGenreAsync(Guid genreId);

    Task<Genre?> GetGenreByIdAsync(Guid genreId);

    Task<IEnumerable<Genre>> GetAllGenresAsync();

    Task UpdateGenreAsync(Genre genre);

    Task DeleteGenreAsync(Guid genreId);

    Task<IEnumerable<Genre>> GetGenresByParentGenreAsync(Guid genreId);
}
