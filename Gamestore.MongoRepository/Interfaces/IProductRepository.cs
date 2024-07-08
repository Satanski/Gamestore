using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    IQueryable<Product> GetProductsAsQueryable();

    Task<Product?> GetProductByNameAsync(string key);

    Task<List<Product>> GetProductBySupplierIdAsync(int supplierID);

    Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
}
