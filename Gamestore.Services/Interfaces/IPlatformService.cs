using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IPlatformService
{
    Task AddPlatformAsync(PlatformModel platformModel);

    Task<IEnumerable<GameModel>> GetGamesByPlatformIdAsync(Guid platformId);

    Task<PlatformModel> GetPlatformByIdAsync(Guid platformId);

    Task<IEnumerable<PlatformModel>> GetAllPlatformsAsync();

    Task UpdatePlatformAsync(PlatformModelDto platformModel);

    Task DeletePlatformByIdAsync(Guid platformId);
}
