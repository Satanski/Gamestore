using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.DAL.Repositories;

public class OrderGameRepository(GamestoreContext context) : RepositoryBase<OrderGame>(context), IOrderGameRepository
{
    public Task<List<OrderGame>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderGame?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(OrderGame entity)
    {
        throw new NotImplementedException();
    }
}
