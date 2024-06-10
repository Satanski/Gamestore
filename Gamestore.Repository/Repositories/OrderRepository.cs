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

    public async Task<Order> GetByCustomerId(Guid id)
    {
        return await _context.Orders.Where(x => x.CustomerId == id).FirstOrDefaultAsync();
    }
}
