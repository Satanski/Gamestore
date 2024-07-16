using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface IProductRepository
{
    Task<List<MongoProduct>> GetAllAsync();

    IQueryable<MongoProduct> GetProductsAsQueryable();

    Task<MongoProduct> GetByNameAsync(string key);

    Task<List<MongoProduct>> GetBySupplierIdAsync(int supplierID);

    Task<List<MongoProduct>> GetByCategoryIdAsync(int categoryId);

    Task<MongoProduct> GetByIdAsync(int id);
}
