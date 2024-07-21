using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IPlatformRepository : IRepository<Platform>, IRepositoryBase<Platform>
{
    Task<Platform?> GetByTypeAsync(string type);

    Task<List<Game>> GetGamesByPlatformAsync(Guid id);
}
