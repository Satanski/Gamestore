using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGameGenreRepository : IRepositoryBase<GameGenres>
{
    Task<List<GameGenres>> GetByGameIdAsync(Guid id);
}
