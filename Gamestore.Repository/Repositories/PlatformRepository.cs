using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class PlatformRepository(GamestoreContext context) : IPlatformRepository
{
    private readonly GamestoreContext _context = context;

    public async Task<List<Game>> GetGamesByPlatformAsync(Guid id)
    {
        return await _context.GamePlatforms.Where(x => x.PlatformId == id).Include(x => x.Game).Select(x => x.Game).ToListAsync();
    }

    public async Task AddAsync(Platform entity)
    {
        await _context.Platforms.AddAsync(entity);
    }

    public void Delete(Platform entity)
    {
        _context.Platforms.Remove(entity);
    }

    public async Task<List<Platform>> GetAllAsync()
    {
        return await _context.Platforms.ToListAsync();
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
}
