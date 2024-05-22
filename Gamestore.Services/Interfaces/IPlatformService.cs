using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IPlatformService
{
    Task AddPlatformAsync(PlatformModel platformModel);

    Task<IEnumerable<GameModel>> GetGamesByPlatformAsync(Guid platformId);

    Task<PlatformModel> GetPlatformByIdAsync(Guid id);

    Task<IEnumerable<PlatformModel>> GetAllPlatformsAsync();

    Task UpdatePlatformAsync(DetailedPlatformModel platformModel);

    Task DeletePlatformAsync(Guid id);
}
