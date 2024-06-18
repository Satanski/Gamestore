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
        return query.Where(x => x.CustomerId == id).FirstOrDefaultAsync();
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        return _context.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<Order?> GetOrderByCustomerIdAsync(Guid id)
    {
        var query = OrderIncludes();
        return query.Where(x => x.Status == Enums.OrderStatus.Open && x.CustomerId == id).FirstOrDefaultAsync();
    }

    public Task<Order?> GetWithDetailsByIdAsync(Guid id)
    {
        var query = OrderIncludes();
        return query.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    private IIncludableQueryable<Order, Game> OrderIncludes()
    {
        return _context.Orders
            .Include(x => x.OrderGames)
            .ThenInclude(x => x.Product);
    }
}
