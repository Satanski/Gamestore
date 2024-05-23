using Gamestore.Repository.Entities;

namespace Gamestore.Repository.Interfaces;

public interface IGameRepository
{
    Task AddGameAsync(Game game);

    Task DeleteGameAsync(Guid gameId);

    Task<IEnumerable<Game>> GetAllGamesAsync();

    Task<Game?> GetGameByIdAsync(Guid gameId);

    Task<Game?> GetGameByKeyAsync(string key);

    Task<IEnumerable<Genre>> GetGenresByGameAsync(Guid gameId);

    Task<IEnumerable<Platform>> GetPlatformsByGameAsync(Guid gameId);

    Task UpdateGameAsync(Game game);
}
