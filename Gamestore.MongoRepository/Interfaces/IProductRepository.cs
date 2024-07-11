using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    IQueryable<Product> GetProductsAsQueryable();

    Task<Product> GetByNameAsync(string key);

    Task<List<Product>> GetBySupplierIdAsync(int supplierID);

    Task<List<Product>> GetByCategoryIdAsync(int categoryId);

    Task<Product> GetByIdAsync(int id);
}
