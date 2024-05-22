using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGameService
{
    Task AddGameAsync(DetailedGameModel gameModel);

    Task DeleteGameAsync(Guid id);

    Task<IEnumerable<GameModel>> GetAllGamesAsync();

    Task<GameModel> GetGameByIdAsync(Guid id);

    Task<GameModel> GetGameByKeyAsync(string key);

    Task UpdateGameAsync(DetailedGameModel gameModel);
}
