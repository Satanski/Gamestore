using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IPlatformRepository
{
    Task AddPlatformAsync(Platform platform);

    Task<IEnumerable<Game>> GetGamesByPlatformAsync(Guid platformId);

    Task<Platform?> GetPlatformByIdAsync(Guid platformId);

    Task<IEnumerable<Platform>> GetAllPlatformsAsync();

    Task UpdatePlatformAsync(Platform platform);

    Task DeletePlatformAsync(Guid platformId);
}
