using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IPlatformService
{
    Task AddPlatformAsync(PlatformAddDto receivedPlatform);

    Task<IEnumerable<GameModelDto>> GetGamesByPlatformIdAsync(Guid platformId);

    Task<PlatformModel> GetPlatformByIdAsync(Guid platformId);

    Task<IEnumerable<PlatformModel>> GetAllPlatformsAsync();

    Task UpdatePlatformAsync(PlatformUpdateDto platformModel);

    Task DeletePlatformByIdAsync(Guid platformId);
}
