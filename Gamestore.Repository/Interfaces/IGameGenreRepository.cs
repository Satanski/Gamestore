using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGameGenreRepository : IRepository<GameGenre>
{
    Task<List<GameGenre>> GetByGameIdAsync(Guid id);
}
