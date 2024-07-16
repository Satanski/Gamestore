using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class OrderGameRepository(GamestoreContext context) : RepositoryBase<OrderProduct>(context), IOrderGameRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<OrderProduct>> GetAllAsync()
    {
        return _context.OrderProducts.ToListAsync();
    }

    public Task<OrderProduct?> GetByIdAsync(Guid id)
    {
        return _context.OrderProducts.FirstOrDefaultAsync(x => x.OrderId == id);
    }

    public Task<List<OrderProduct>> GetByOrderIdAsync(Guid id)
    {
        return _context.OrderProducts.Where(x => x.OrderId == id).ToListAsync();
    }

    public Task<OrderProduct?> GetByOrderIdAndProductIdAsync(Guid orderId, Guid productId)
    {
        return _context.OrderProducts.Where(x => x.OrderId == orderId && x.ProductId == productId).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(OrderProduct entity)
    {
        var g = await _context.OrderProducts.Where(x => x.ProductId == entity.ProductId && x.OrderId == entity.OrderId).FirstOrDefaultAsync();
        if (g != null)
        {
            _context.Entry(g).CurrentValues.SetValues(entity);
        }
    }
}
