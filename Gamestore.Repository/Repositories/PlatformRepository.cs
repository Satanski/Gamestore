using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class PlatformRepository(GamestoreContext context) : RepositoryBase<Platform>(context), IPlatformRepository
{
    private readonly GamestoreContext _context = context;

    public async Task<List<Game>> GetGamesByPlatformAsync(Guid id)
    {
        return await _context.Games
           .Include(x => x.Publisher)
           .Include(x => x.GameGenres).ThenInclude(x => x.Genre)
           .Include(x => x.GamePlatforms).ThenInclude(x => x.Platform)
           .Where(x => x.GamePlatforms.Contains(new GamePlatform { GameId = x.Id, PlatformId = id }))
           .ToListAsync();
    }

    public async Task<Platform?> GetByIdAsync(Guid id)
    {
        return await _context.Platforms.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Platform entity)
    {
        var g = await _context.Platforms.Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }

    public async Task<List<Platform>> GetAllAsync()
    {
        return await _context.Platforms.ToListAsync();
    }
}
