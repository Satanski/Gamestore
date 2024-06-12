using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGameService
{
    Task AddGameAsync(GameAddDto gameModel);

    Task DeleteGameByIdAsync(Guid gameId);

    Task DeleteGameByKeyAsync(string gameKey);

    Task<IEnumerable<GameModelDto>> GetAllGamesAsync();

    Task<GameModelDto> GetGameByIdAsync(Guid gameId);

    Task<GameModel> GetGameByKeyAsync(string key);

    Task<IEnumerable<GenreModel>> GetGenresByGameKeyAsync(string gameKey);

    Task<IEnumerable<PlatformModel>> GetPlatformsByGameKeyAsync(string gameKey);

    Task<PublisherModel> GetPublisherByGameKeyAsync(string gameKey);

    Task UpdateGameAsync(GameUpdateDto gameModel);
}
