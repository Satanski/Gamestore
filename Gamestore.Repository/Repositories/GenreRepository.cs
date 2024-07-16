using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GenreRepository(GamestoreContext context) : RepositoryBase<Category>(context), IGenreRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<Product>> GetGamesByGenreAsync(Guid id)
    {
        return _context.Products
            .Include(x => x.ProductCategories).ThenInclude(x => x.Category)
            .Include(x => x.ProductPlatforms).ThenInclude(x => x.Platform)
            .Include(x => x.Publisher)
            .Include(x => x.Comments)
            .Where(x => x.ProductCategories.Any(gg => gg.CategoryId == id && gg.ProductId == x.Id) && !x.IsDeleted)
            .AsSplitQuery()
            .ToListAsync();
    }

    public Task<List<Category>> GetGenresByParentGenreAsync(Guid id)
    {
        return _context.Genres.Where(x => x.ParentCategoryId == id).ToListAsync();
    }

    public Task<Category?> GetByIdAsync(Guid id)
    {
        return _context.Genres.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        var g = await _context.Genres.Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }

    public Task<List<Category>> GetAllAsync()
    {
        return _context.Genres.ToListAsync();
    }
}
