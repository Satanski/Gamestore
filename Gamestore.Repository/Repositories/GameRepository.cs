using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Gamestore.DAL.Repositories;

public class GameRepository(GamestoreContext context) : RepositoryBase<Product>(context), IGameRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<Category>> GetGenresByGameAsync(Guid id)
    {
        return _context.ProductGenres.Where(x => x.ProductId == id).Include(x => x.Category).Select(x => x.Category).ToListAsync();
    }

    public Task<List<Platform>> GetPlatformsByGameAsync(Guid id)
    {
        return _context.ProductPlatforms.Where(x => x.ProductId == id).Include(x => x.Platform).Select(x => x.Platform).ToListAsync();
    }

    public Task<Supplier?> GetPublisherByGameAsync(Guid gameId)
    {
        return _context.Products.Include(x => x.Publisher).Where(x => x.Id == gameId).Select(x => x.Publisher).FirstOrDefaultAsync();
    }

    public Task<Product?> GetGameByKeyAsync(string key)
    {
        var query = GameIncludes();
        return query.Where(x => x.Key == key).AsSplitQuery().FirstOrDefaultAsync();
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        var query = GameIncludes();
        return query.Where(x => x.Id == id).AsSplitQuery().FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Product entity)
    {
        var g = await _context.Products.Include(x => x.ProductCategories).Include(x => x.ProductPlatforms).Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
        g.ProductCategories = entity.ProductCategories;
        g.ProductPlatforms = entity.ProductPlatforms;
    }

    public Task<List<Product>> GetAllAsync()
    {
        var query = GameIncludes();
        return query.Where(x => !x.IsDeleted).AsSplitQuery().ToListAsync();
    }

    public IQueryable<Product> GetGamesAsQueryable()
    {
        var includes = GameIncludes();
        return includes.Where(x => !x.IsDeleted);
    }

    public async Task SoftDelete(Product game)
    {
        var g = await _context.Products.Where(x => x.Id == game.Id).FirstAsync();
        g.IsDeleted = true;
    }

    private IIncludableQueryable<Product, List<Comment>> GameIncludes()
    {
        return _context.Products
            .Include(x => x.ProductCategories).ThenInclude(x => x.Category)
            .Include(x => x.ProductPlatforms).ThenInclude(x => x.Platform)
            .Include(x => x.Publisher)
            .Include(x => x.Comments);
    }
}
