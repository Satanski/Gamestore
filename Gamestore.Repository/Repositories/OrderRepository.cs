using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Gamestore.DAL.Repositories;

public class OrderRepository(GamestoreContext context) : RepositoryBase<Order>(context), IOrderRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<Order>> GetAllAsync()
    {
        return _context.Orders.ToListAsync();
    }

    public Task<Order?> GetByCustomerIdAsync(Guid id)
    {
        var query = OrderIncludes();
        return query.Where(x => x.Status == Enums.OrderStatus.Open && x.CustomerId == id).FirstOrDefaultAsync();
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        return _context.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startD, DateTime endD)
    {
        return _context.Orders.Where(x => x.OrderDate >= startD && x.OrderDate <= endD).OrderByDescending(x => x.OrderDate).ToListAsync();
    }

    public Task<Order?> GetWithDetailsByIdAsync(Guid id)
    {
        var query = OrderIncludes();
        return query.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Order entity)
    {
        var g = await _context.Orders.Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }

    private IIncludableQueryable<Order, Product> OrderIncludes()
    {
        return _context.Orders
            .Include(x => x.OrderProducts)
            .ThenInclude(x => x.Product);
    }
}
