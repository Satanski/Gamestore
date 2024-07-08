using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface IOrderRepository
{
    Task<List<MongoOrder>?> GetAllAsync();
}
