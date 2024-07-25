using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IOrderGameRepository : IRepository<OrderGame>, IRepositoryBase<OrderGame>
{
    Task<OrderGame?> GetByIdAsync(Guid id);

    Task<OrderGame?> GetByOrderIdAndProductIdAsync(Guid orderId, Guid productId);

    Task<List<OrderGame>> GetOrderGamesByOrderIdAsync(Guid id);
}
