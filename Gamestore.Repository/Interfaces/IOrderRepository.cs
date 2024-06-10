using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IOrderRepository : IRepositoryBase<Order>
{
    Task<Order> GetByCustomerId(Guid id);
}
