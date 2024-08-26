using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Gamestore.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Gamestore.Services.Interfaces;

public interface IGameService
{
    Task AddGameAsync(GameDtoWrapper gameModel, IConfiguration configuration);

    Task AddGameToCartAsync(Guid customerId, string gameKey, int quantity);

    Task DeleteGameByIdAsync(Guid gameId, IConfiguration configuration);

    Task DeleteGameByKeyAsync(string gameKey, IConfiguration configuration);

    Task<List<GameModelDto>> GetAllGamesAsync(bool canSeeDeletedGames);

    Task<FilteredGamesDto> GetFilteredGamesAsync(GameFiltersDto gameFilters, bool canSeeDeletedGames);

    Task<GameModelDto> GetGameByIdAsync(Guid gameId);

    Task<GameModelDto> GetGameByKeyAsync(string key);

    Task<IEnumerable<GenreModelDto>> GetGenresByGameKeyAsync(string gameKey);

    Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameKeyAsync(string gameKey);

    Task<PublisherModelDto> GetPublisherByGameKeyAsync(string gameKey);

    Task SoftDeleteGameByIdAsync(Guid gameId);

    Task SoftDeleteGameByKeyAsync(string gameKey);

    Task UpdateGameAsync(GameDtoWrapper gameModel, IConfiguration configuration);

    Task<string> AddCommentToGameAsync(string userName, string gameKey, CommentModelDto comment, UserManager<AppUser> userManager);

    Task<IEnumerable<CommentModel>> GetCommentsByGameKeyAsync(string gameKey);

    Task DeleteCommentAsync(string userName, string gameKey, Guid commentId, bool canModerate);

    List<string> GetPaginationOptions();

    List<string> GetPublishDateOptions();

    List<string> GetSortingOptions();

    Task<byte[]> GetPictureByGameKeyAsync(string key, IConfiguration configuration);
}
