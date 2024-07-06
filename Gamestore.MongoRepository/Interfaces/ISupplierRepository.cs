using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface ISupplierRepository
{
    Task<List<Supplier>> GetAllAsync();

    Task<Supplier> GetSupplierByIdAsync(int id);

    Task<Supplier> GetSupplierByNameAsync(string companyName);
}
