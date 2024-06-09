using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IPlatformService
{
    Task AddPlatformAsync(PlatformModelDto platformModel);

    Task<IEnumerable<GameModelDto>> GetGamesByPlatformIdAsync(Guid platformId);

    Task<PlatformModelDto> GetPlatformByIdAsync(Guid platformId);

    Task<IEnumerable<PlatformModelDto>> GetAllPlatformsAsync();

    Task UpdatePlatformAsync(PlatformModel platformModel);

    Task DeletePlatformByIdAsync(Guid platformId);
}
