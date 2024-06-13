using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class OrderRepository(GamestoreContext context) : RepositoryBase<Order>(context), IOrderRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetByCustomerIdAsync(Guid id)
    {
        return _context.Orders.Where(x => x.CustomerId == id).FirstOrDefaultAsync();
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        return _context.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }
}
