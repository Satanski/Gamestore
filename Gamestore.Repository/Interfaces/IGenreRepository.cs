using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGenreRepository : IRepository<Genre>
{
    Task<List<Game>> GetGamesByGenreAsync(Guid id);

    Task<List<Genre>> GetGenresByParentGenreAsync(Guid id);
}
