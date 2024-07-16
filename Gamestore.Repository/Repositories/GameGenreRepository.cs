using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GameGenreRepository(GamestoreContext context) : RepositoryBase<ProductCategory>(context), IGameGenreRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<ProductCategory>> GetByGameIdAsync(Guid id)
    {
        return _context.ProductGenres.Where(x => x.ProductId == id).ToListAsync();
    }

    public Task<List<ProductCategory>> GetAllAsync()
    {
        return _context.ProductGenres.ToListAsync();
    }
}
