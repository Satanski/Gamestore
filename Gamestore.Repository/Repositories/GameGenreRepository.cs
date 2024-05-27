using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GameGenreRepository(GamestoreContext context) : IGameGenreRepository
{
    private readonly GamestoreContext _context = context;

    public async Task AddAsync(GameGenre entity)
    {
        await _context.GameGenres.AddAsync(entity);
    }

    public void Delete(GameGenre entity)
    {
        _context.GameGenres.Remove(entity);
    }

    public async Task<List<GameGenre>> GetAllAsync()
    {
        return await _context.GameGenres.ToListAsync();
    }

    public async Task<List<GameGenre>> GetByGameIdAsync(Guid id)
    {
        return await _context.GameGenres.Where(x => x.GameId == id).ToListAsync();
    }
}
