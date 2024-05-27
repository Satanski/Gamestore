using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GamePlatformRepository(GamestoreContext context) : IGamePlatformRepository
{
    private readonly GamestoreContext _context = context;

    public async Task AddAsync(GamePlatform entity)
    {
        await _context.GamePlatforms.AddAsync(entity);
    }

    public void Delete(GamePlatform entity)
    {
        _context.GamePlatforms.Remove(entity);
    }

    public Task<List<GamePlatform>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<GamePlatform>> GetByGameIdAsync(Guid id)
    {
        return await _context.GamePlatforms.Where(x => x.GameId == id).ToListAsync();
    }

    public Task<GamePlatform?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(GamePlatform entity)
    {
        throw new NotImplementedException();
    }
}
