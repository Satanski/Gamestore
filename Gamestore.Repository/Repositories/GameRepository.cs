using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GameRepository(GamestoreContext context) : RepositoryBase<Game>(context), IGameRepository
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

    public async Task<Publisher?> GetPublisherByGameAsync(Guid gameId)
    {
        return await _context.Games.Include(x => x.Publisher).Where(x => x.Id == gameId).Select(x => x.Publisher).FirstOrDefaultAsync();
    }

    public async Task<Game?> GetGameByKeyAsync(string key)
    {
        return await _context.Games
            .Include(x => x.GameGenres).ThenInclude(x => x.Genre)
            .Include(x => x.GamePlatforms).ThenInclude(x => x.Platform)
            .Include(x => x.Publisher)
            .Where(x => x.Key == key)
            .AsSplitQuery()
            .FirstOrDefaultAsync();
    }

    public async Task<Game?> GetByIdAsync(Guid id)
    {
        return await _context.Games
            .Include(x => x.GameGenres).ThenInclude(x => x.Genre)
            .Include(x => x.GamePlatforms).ThenInclude(x => x.Platform)
            .Include(x => x.Publisher)
            .Where(x => x.Id == id)
            .AsSplitQuery()
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Game entity)
    {
        var g = await _context.Games.Include(x => x.GameGenres).Include(x => x.GamePlatforms).Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }

    public async Task<List<Game>> GetAllAsync()
    {
        return await _context.Games
            .Include(x => x.GameGenres).ThenInclude(x => x.Genre)
            .Include(x => x.GamePlatforms).ThenInclude(x => x.Platform)
            .Include(x => x.Publisher)
            .AsSplitQuery()
            .ToListAsync();
    }
}
