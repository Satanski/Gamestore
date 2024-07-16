using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGamePlatformRepository : IRepositoryBase<ProductPlatform>
{
    Task<List<ProductPlatform>> GetByGameIdAsync(Guid id);
}
