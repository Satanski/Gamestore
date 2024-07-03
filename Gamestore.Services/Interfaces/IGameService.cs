using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGameService
{
    Task AddGameAsync(GameDtoWrapper gameModel);

    Task AddGameToCartAsync(Guid customerId, string gameKey, int quantity);

    Task DeleteGameByIdAsync(Guid gameId);

    Task DeleteGameByKeyAsync(string gameKey);

    Task<IEnumerable<GameModelDto>> GetAllGamesAsync();

    Task<FilteredGamesDto> GetFilteredGamesAsync(GameFiltersDto gameFilters);

    Task<GameModelDto> GetGameByIdAsync(Guid gameId);

    Task<GameModelDto> GetGameByKeyAsync(string key);

    Task<IEnumerable<GenreModelDto>> GetGenresByGameKeyAsync(string gameKey);

    Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameKeyAsync(string gameKey);

    Task<PublisherModelDto> GetPublisherByGameKeyAsync(string gameKey);

    Task SoftDeleteGameByIdAsync(Guid gameId);

    Task SoftDeleteGameByKeyAsync(string gameKey);

    Task UpdateGameAsync(GameDtoWrapper gameModel);

    Task<string> AddCommentToGameAsync(string gameKey, CommentModelDto comment);

    Task<IEnumerable<CommentModel>> GetCommentsByGameKeyAsync(string gameKey);

    Task DeleteCommentAsync(string gameKey, Guid commentId);

    List<string> GetPaginationOptions();

    List<string> GetPublishDateOptions();

    List<string> GetSortingOptions();
}
