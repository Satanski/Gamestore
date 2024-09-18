using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Gamestore.Services.Models;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.Services.Interfaces;

public interface IGameService
{
    Task AddGameAsync(GameDtoWrapper gameModel);

    Task AddGameToCartAsync(Guid customerId, string gameKey, int quantity);

    Task DeleteGameByIdAsync(Guid gameId);

    Task DeleteGameByKeyAsync(string gameKey);

    Task<List<GameModelDto>> GetAllGamesAsync(bool canSeeDeletedGames);

    Task<FilteredGamesDto> GetFilteredGamesAsync(GameFiltersDto gameFilters, bool canSeeDeletedGames);

    Task<GameModelDto> GetGameByIdAsync(Guid gameId);

    Task<GameModelDto> GetGameByKeyAsync(string key);

    Task<IEnumerable<GenreModelDto>> GetGenresByGameKeyAsync(string gameKey);

    Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameKeyAsync(string gameKey);

    Task<PublisherModelDto> GetPublisherByGameKeyAsync(string gameKey);

    Task SoftDeleteGameByIdAsync(Guid gameId);

    Task SoftDeleteGameByKeyAsync(string gameKey);

    Task UpdateGameAsync(GameDtoWrapper gameModel);

    Task<string> AddCommentToGameAsync(string userName, string gameKey, CommentModelDto comment, UserManager<AppUser> userManager);

    Task<IEnumerable<CommentModel>> GetCommentsByGameKeyAsync(string gameKey);

    Task DeleteCommentAsync(string userName, string gameKey, Guid commentId, bool canModerate);

    List<string> GetPaginationOptions();

    List<string> GetPublishDateOptions();

    List<string> GetSortingOptions();

    Task<(byte[] ImageBytes, string MimeType)> GetPictureByGameKeyAsync(string key);

    Task AddHundredThousendGamesAsync();
}
