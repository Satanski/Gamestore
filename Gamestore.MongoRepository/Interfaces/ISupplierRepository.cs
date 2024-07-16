using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface ISupplierRepository
{
    Task<List<MongoSupplier>> GetAllAsync();

    Task<MongoSupplier> GetByIdAsync(int id);

    Task<MongoSupplier> GetByNameAsync(string companyName);

    Task UpdateAsync(MongoSupplier entity);

    Task AddAsync(MongoSupplier entity);

    Task DeleteAsync(MongoSupplier entity);
}
