using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GameGenreRepository(GamestoreContext context) : RepositoryBase<GameGenre>(context), IGameGenreRepository
{
    private readonly GamestoreContext _context = context;

    public async Task<List<GameGenre>> GetByGameIdAsync(Guid id)
    {
        return await _context.GameGenres.Where(x => x.GameId == id).ToListAsync();
    }

    public async Task<List<GameGenre>> GetAllAsync()
    {
        return await _context.GameGenres.ToListAsync();
    }
}
