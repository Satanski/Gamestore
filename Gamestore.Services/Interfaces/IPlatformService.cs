using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IPlatformService
{
    Task AddPlatformAsync(PlatformModel platformModel);

    Task<IEnumerable<GameModelDto>> GetGamesByPlatformIdAsync(Guid platformId);

    Task<PlatformModelDto> GetPlatformByIdAsync(Guid platformId);

    Task<IEnumerable<PlatformModelDto>> GetAllPlatformsAsync();

    Task UpdatePlatformAsync(PlatformModel platformModel);

    Task DeletePlatformByIdAsync(Guid platformId);
}
