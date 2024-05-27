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

    public async Task<List<GamePlatform>> GetAllAsync()
    {
        return await _context.GamePlatforms.ToListAsync();
    }

    public async Task<List<GamePlatform>> GetByGameIdAsync(Guid id)
    {
        return await _context.GamePlatforms.Where(x => x.GameId == id).ToListAsync();
    }
}
