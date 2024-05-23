using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGameService
{
    Task AddGameAsync(DetailedGameModel gameModel);

    Task DeleteGameAsync(Guid gameId);

    Task<IEnumerable<GameModel>> GetAllGamesAsync();

    Task<GameModel> GetGameByIdAsync(Guid gameId);

    Task<GameModel> GetGameByKeyAsync(string key);

    Task<IEnumerable<DetailedGenreModel>> GetGenresByGameAsync(Guid gameId);

    Task<IEnumerable<DetailedPlatformModel>> GetPlatformsByGameAsync(Guid gameId);

    Task UpdateGameAsync(DetailedGameModel gameModel);
}
