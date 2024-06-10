using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class OrderRepository(GamestoreContext context) : RepositoryBase<Order>(context), IOrderRepository
{
    private readonly GamestoreContext _context = context;

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order> GetByCustomerId(Guid id)
    {
        return await _context.Orders.Where(x => x.CustomerId == id).FirstOrDefaultAsync();
    }
}
