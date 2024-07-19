using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GenreRepository(GamestoreContext context) : RepositoryBase<Genre>(context), IGenreRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<Game>> GetGamesByGenreAsync(Guid id)
    {
        return _context.Games
            .Include(x => x.ProductCategories).ThenInclude(x => x.Category)
            .Include(x => x.ProductPlatforms).ThenInclude(x => x.Platform)
            .Include(x => x.Publisher)
            .Include(x => x.Comments)
            .Where(x => x.ProductCategories.Any(gg => gg.GenreId == id && gg.GameId == x.Id) && !x.IsDeleted)
            .AsSplitQuery()
            .ToListAsync();
    }

    public Task<List<Genre>> GetGenresByParentGenreAsync(Guid id)
    {
        return _context.Genres.Where(x => x.ParentGenreId == id).ToListAsync();
    }

    public Task<Genre?> GetByIdAsync(Guid id)
    {
        return _context.Genres.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Genre entity)
    {
        var g = await _context.Genres.Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }

    public Task<List<Genre>> GetAllAsync()
    {
        return _context.Genres.ToListAsync();
    }
}
