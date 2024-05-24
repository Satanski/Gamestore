using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IPlatformRepository : IRepository<Platform>
{
    Task<List<Game>> GetGamesByPlatformAsync(Guid id);
}
