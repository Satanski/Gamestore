using Gamestore.Repository.Entities;

namespace Gamestore.Repository.Interfaces;

public interface IGameRepository
{
    Task AddGameAsync(Game game);

    Task DeleteGameAsync(Guid id);

    Task<IEnumerable<Game>> GetAllGamesAsync();

    Task<Game?> GetGameByIdAsync(Guid id);

    Task<Game?> GetGameByKeyAsync(string key);

    Task<IEnumerable<Genre>> GetGenresByGameAsync(Guid id);

    Task<IEnumerable<Platform>> GetPlatformsByGameAsync(Guid id);

    Task UpdateGameAsync(Game game);
}
