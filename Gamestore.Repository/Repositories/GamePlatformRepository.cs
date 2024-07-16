using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GamePlatformRepository(GamestoreContext context) : RepositoryBase<ProductPlatform>(context), IGamePlatformRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<ProductPlatform>> GetByGameIdAsync(Guid id)
    {
        return _context.ProductPlatforms.Include(x => x.Platform).Where(x => x.ProductId == id).ToListAsync();
    }

    public Task<List<ProductPlatform>> GetAllAsync()
    {
        return _context.ProductPlatforms.Include(x => x.Platform).ToListAsync();
    }
}
