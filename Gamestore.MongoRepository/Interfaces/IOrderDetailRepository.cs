using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface IOrderDetailRepository
{
    Task<List<MongoOrderDetail>> GetByOrderIdAsync(int id);
}
