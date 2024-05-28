using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGameService
{
    Task AddGameAsync(GameModelDto gameModel);

    Task DeleteGameAsync(Guid gameId);

    Task<IEnumerable<GameModel>> GetAllGamesAsync();

    Task<GameModel> GetGameByIdAsync(Guid gameId);

    Task<GameModel> GetGameByKeyAsync(string key);

    Task<IEnumerable<GenreModelDto>> GetGenresByGameAsync(Guid gameId);

    Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameAsync(Guid gameId);

    Task UpdateGameAsync(GameModelDto gameModel);
}
