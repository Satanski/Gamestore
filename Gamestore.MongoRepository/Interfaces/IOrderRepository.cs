using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface IOrderRepository
{
    Task<List<MongoOrder>> GetAllAsync();

    Task<MongoOrder> GetByIdAsync(int id);

    Task UpdateAsync(MongoOrder entity);

    Task AddAsync(MongoOrder entity);

    Task DeleteAsync(MongoOrder entity);
}
