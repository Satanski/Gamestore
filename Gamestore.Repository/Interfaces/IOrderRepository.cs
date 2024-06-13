﻿using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IOrderRepository : IRepository<Order>, IRepositoryBase<Order>
{
    Task<Order?> GetByCustomerIdAsync(Guid id);
}
