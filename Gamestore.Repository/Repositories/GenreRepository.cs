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
            .Include(x => x.Publisher)
            .Include(x => x.GameGenres).ThenInclude(x => x.Genre)
            .Include(x => x.GamePlatforms).ThenInclude(x => x.Platform)
            .Where(x => x.GameGenres.Contains(new GameGenre { GameId = x.Id, GenreId = id }) && !x.IsDeleted)
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
