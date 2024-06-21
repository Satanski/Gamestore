using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IOrderRepository : IRepository<Order>, IRepositoryBase<Order>
{
    Task<Order?> GetByCustomerIdAsync(Guid id);

    Task<Order?> GetOrderByCustomerIdAsync(Guid id);

    Task<Order?> GetWithDetailsByIdAsync(Guid id);
}
