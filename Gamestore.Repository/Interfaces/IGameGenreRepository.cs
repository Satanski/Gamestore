using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGameGenreRepository
{
    Task AddAsync(GameGenre entity);

    void Delete(GameGenre entity);

    Task<List<GameGenre>> GetAllAsync();

    Task<List<GameGenre>> GetByGameIdAsync(Guid id);
}
