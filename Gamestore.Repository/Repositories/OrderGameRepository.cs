using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class OrderGameRepository(GamestoreContext context) : RepositoryBase<OrderGame>(context), IOrderGameRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<OrderGame>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderGame?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderGame> GetByOrderIdAndProductIdAsync(Guid orderId, Guid productId)
    {
        return await _context.OrderGames.Where(x => x.OrderId == orderId && x.ProductId == productId).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(OrderGame entity)
    {
        var g = await _context.OrderGames.Where(x => x.ProductId == entity.ProductId && x.OrderId == entity.Order.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }
}
