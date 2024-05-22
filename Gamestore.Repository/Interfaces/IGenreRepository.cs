using Gamestore.Repository.Entities;

namespace Gamestore.Repository.Interfaces;

public interface IGenreRepository
{
    Task AddGenreAsync(Genre genre);

    Task<IEnumerable<Game>> GetGamesByGenreAsync(Guid id);

    Task<Genre?> GetGenreByIdAsync(Guid id);

    Task<IEnumerable<Genre>> GetAllGenresAsync();

    Task UpdateGenreAsync(Genre genre);

    Task DeleteGenreAsync(Guid id);

    Task<IEnumerable<Genre>> GetGenresByParentGenreAsync(Guid id);
}
