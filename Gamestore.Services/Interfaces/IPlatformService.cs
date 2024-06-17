using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IPlatformService
{
    Task AddPlatformAsync(PlatformDtoWrapper platformModel);

    Task<IEnumerable<GameModelDto>> GetGamesByPlatformIdAsync(Guid platformId);

    Task<PlatformModelDto> GetPlatformByIdAsync(Guid platformId);

    Task<IEnumerable<PlatformModelDto>> GetAllPlatformsAsync();

    Task UpdatePlatformAsync(PlatformDtoWrapper platformModel);

    Task DeletePlatformByIdAsync(Guid platformId);
}
