using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GamePlatformRepository(GamestoreContext context) : RepositoryBase<GamePlatform>(context), IGamePlatformRepository
{
    private readonly GamestoreContext _context = context;

    public async Task<List<GamePlatform>> GetByGameIdAsync(Guid id)
    {
        return await _context.GamePlatforms.Include(x => x.Platform).Where(x => x.GameId == id).ToListAsync();
    }

    public async Task<List<GamePlatform>> GetAllAsync()
    {
        return await _context.GamePlatforms.Include(x => x.Platform).ToListAsync();
    }
}
