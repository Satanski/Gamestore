using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGameService
{
    Task AddGameAsync(GameModelDto gameModel);

    Task DeleteGameByIdAsync(Guid gameId);

    Task<IEnumerable<GameModel>> GetAllGamesAsync();

    Task<GameModel> GetGameByIdAsync(Guid gameId);

    Task<GameModel> GetGameByKeyAsync(string key);

    Task<IEnumerable<GenreModelDto>> GetGenresByGameIdAsync(Guid gameId);

    Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameIdAsync(Guid gameId);

    Task UpdateGameAsync(GameModelDto gameModel);
}
