using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IPlatformRepository : IRepository<Platform>, IRepositoryBase<Platform>
{
    Task<Platform?> GetByTypeAsync(string type);

    Task<List<Product>> GetGamesByPlatformAsync(Guid id);
}
