using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IOrderGameRepository : IRepository<OrderProduct>, IRepositoryBase<OrderProduct>
{
    Task<OrderProduct?> GetByOrderIdAndProductIdAsync(Guid orderId, Guid productId);

    Task<List<OrderProduct>> GetByOrderIdAsync(Guid id);
}
