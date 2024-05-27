using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGamePlatformRepository : IRepository<GamePlatform>
{
    Task<List<GamePlatform>> GetByGameIdAsync(Guid id);
}
