using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface ISupplierRepository
{
    Task<List<Supplier>> GetAllAsync();

    Task<Supplier> GetByIdAsync(int id);

    Task<Supplier> GetByNameAsync(string companyName);
}
