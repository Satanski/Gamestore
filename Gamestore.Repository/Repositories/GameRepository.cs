using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GameRepository(GamestoreContext context) : IGameRepository
{
    private readonly GamestoreContext _context = context;

    public async Task<List<Genre>> GetGenresByGameAsync(Guid id)
    {
        return await _context.GameGenres.Where(x => x.GameId == id).Include(x => x.Genre).Select(x => x.Genre).ToListAsync();
    }

    public async Task<List<Platform>> GetPlatformsByGameAsync(Guid id)
    {
        return await _context.GamePlatforms.Where(x => x.GameId == id).Include(x => x.Platform).Select(x => x.Platform).ToListAsync();
    }

    public async Task<Game?> GetGameByKeyAsync(string key)
    {
        return await _context.Games.Where(x => x.Key == key).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Game entity)
    {
        await _context.Games.AddAsync(entity);
    }

    public void Delete(Game entity)
    {
        _context.Games.Remove(entity);
    }

    public async Task<List<Game>> GetAllAsync()
    {
        return await _context.Games.Include(x => x.GameGenres).ToListAsync();
    }

    public async Task<Game?> GetByIdAsync(Guid id)
    {
        return await _context.Games.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Game entity)
    {
        var g = await _context.Games.Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }
}
