using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IOrderGameRepository : IRepository<OrderGame>, IRepositoryBase<OrderGame>
{
    Task<OrderGame?> GetByOrderIdAndProductIdAsync(Guid orderId, Guid productId);
}
