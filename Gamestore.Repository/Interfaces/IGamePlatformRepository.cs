using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGamePlatformRepository : IRepositoryBase<GamePlatform>
{
    Task BulkInsert(List<GamePlatform> gamePlatforms);

    Task<List<GamePlatform>> GetByGameIdAsync(Guid id);
}
