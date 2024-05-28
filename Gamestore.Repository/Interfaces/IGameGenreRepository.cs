using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGameGenreRepository : IRepositoryBase<GameGenre>
{
    Task<List<GameGenre>> GetByGameIdAsync(Guid id);
}
