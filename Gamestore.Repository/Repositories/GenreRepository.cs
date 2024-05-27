using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GenreRepository(GamestoreContext context) : IGenreRepository
{
    private readonly GamestoreContext _context = context;

    public async Task<List<Game>> GetGamesByGenreAsync(Guid id)
    {
        return await _context.GameGenres.Where(x => x.GenreId == id).Include(x => x.Game).Select(x => x.Game).ToListAsync();
    }

    public async Task<List<Genre>> GetGenresByParentGenreAsync(Guid id)
    {
        return await _context.Genres.Where(x => x.ParentGenreId == id).ToListAsync();
    }

    public async Task AddAsync(Genre entity)
    {
        await _context.Genres.AddAsync(entity);
    }

    public void Delete(Genre entity)
    {
        _context.Genres.Remove(entity);
    }

    public async Task<List<Genre>> GetAllAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<Genre?> GetByIdAsync(Guid id)
    {
        return await _context.Genres.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Genre entity)
    {
        var g = await _context.Genres.Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }
}
