using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Gamestore.DAL.Repositories;

public class GameRepository(GamestoreContext context) : RepositoryBase<Game>(context), IGameRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<Genre>> GetGenresByGameAsync(Guid id)
    {
        return _context.GameGenres.Where(x => x.GameId == id).Include(x => x.Category).Select(x => x.Category).ToListAsync();
    }

    public Task<List<Platform>> GetPlatformsByGameAsync(Guid id)
    {
        return _context.GamePlatforms.Where(x => x.GameId == id).Include(x => x.Platform).Select(x => x.Platform).ToListAsync();
    }

    public Task<Publisher?> GetPublisherByGameAsync(Guid gameId)
    {
        return _context.Games.Include(x => x.Publisher).Where(x => x.Id == gameId).Select(x => x.Publisher).FirstOrDefaultAsync();
    }

    public Task<Game?> GetGameByKeyAsync(string key)
    {
        var query = GameIncludes();
        return query.Where(x => x.Key == key).AsSplitQuery().FirstOrDefaultAsync();
    }

    public Task<Game?> GetByIdAsync(Guid id)
    {
        var query = GameIncludes();
        return query.Where(x => x.Id == id).AsSplitQuery().FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Game entity)
    {
        var g = await _context.Games.Include(x => x.ProductCategories).Include(x => x.ProductPlatforms).Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
        g.ProductCategories = entity.ProductCategories;
        g.ProductPlatforms = entity.ProductPlatforms;
    }

    public Task<List<Game>> GetAllAsync()
    {
        var query = GameIncludes();
        return query.Where(x => !x.IsDeleted).AsSplitQuery().ToListAsync();
    }

    public IQueryable<Game> GetGamesAsQueryable()
    {
        var includes = GameIncludes();
        return includes.Where(x => !x.IsDeleted);
    }

    public async Task SoftDelete(Game game)
    {
        var g = await _context.Games.Where(x => x.Id == game.Id).FirstAsync();
        g.IsDeleted = true;
    }

    private IIncludableQueryable<Game, List<Comment>> GameIncludes()
    {
        return _context.Games
            .Include(x => x.ProductCategories).ThenInclude(x => x.Category)
            .Include(x => x.ProductPlatforms).ThenInclude(x => x.Platform)
            .Include(x => x.Publisher)
            .Include(x => x.Comments);
    }
}
