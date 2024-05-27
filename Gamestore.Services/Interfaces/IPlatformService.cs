using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IPlatformService
{
    Task AddPlatformAsync(PlatformModel platformModel);

    Task<IEnumerable<GameModel>> GetGamesByPlatformAsync(Guid platformId);

    Task<PlatformModel> GetPlatformByIdAsync(Guid platformId);

    Task<IEnumerable<PlatformModel>> GetAllPlatformsAsync();

    Task UpdatePlatformAsync(PlatformModelDto platformModel);

    Task DeletePlatformAsync(Guid platformId);
}
