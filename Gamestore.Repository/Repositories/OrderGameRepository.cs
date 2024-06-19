using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class OrderGameRepository(GamestoreContext context) : RepositoryBase<OrderGame>(context), IOrderGameRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<OrderGame>> GetAllAsync()
    {
        return _context.OrderGames.ToListAsync();
    }

    public Task<OrderGame?> GetByIdAsync(Guid id)
    {
        return _context.OrderGames.FirstOrDefaultAsync(x => x.OrderId == id);
    }

    public Task<List<OrderGame>> GetByOrderIdAsync(Guid id)
    {
        return _context.OrderGames.Where(x => x.OrderId == id).ToListAsync();
    }

    public Task<OrderGame?> GetByOrderIdAndProductIdAsync(Guid orderId, Guid productId)
    {
        return _context.OrderGames.Where(x => x.OrderId == orderId && x.ProductId == productId).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(OrderGame entity)
    {
        var g = await _context.OrderGames.Where(x => x.ProductId == entity.ProductId && x.OrderId == entity.OrderId).FirstOrDefaultAsync();
        if (g != null)
        {
            _context.Entry(g).CurrentValues.SetValues(entity);
        }
    }
}
