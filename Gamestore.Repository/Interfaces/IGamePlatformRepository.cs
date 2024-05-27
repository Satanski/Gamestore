using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGamePlatformRepository
{
    Task AddAsync(GamePlatform entity);

    void Delete(GamePlatform entity);

    Task<List<GamePlatform>> GetAllAsync();

    Task<List<GamePlatform>> GetByGameIdAsync(Guid id);
}
