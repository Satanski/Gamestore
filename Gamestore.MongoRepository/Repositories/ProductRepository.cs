using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class ProductRepository(IMongoDatabase database) : IProductRepository
{
    public async Task<List<Product>> GetAllAsync()
    {
        var collection = database.GetCollection<Product>("products");
        var products = await collection.Find(_ => true).ToListAsync();
        return products;
    }

    public IQueryable<Product> GetProductsAsQueryable()
    {
        return database.GetCollection<Product>("products").AsQueryable();
    }

    public async Task<Product?> GetProductByNameAsync(string key)
    {
        var collection = database.GetCollection<Product>("products");
        var products = await collection.Find(x => x.ProductName == key).FirstOrDefaultAsync();
        return products;
    }

    public async Task<List<Product>> GetProductBySupplierIdAsync(int supplierID)
    {
        var collection = database.GetCollection<Product>("products");
        var products = await collection.Find(x => x.SupplierID == supplierID).ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        var collection = database.GetCollection<Product>("products");
        var products = await collection.Find(x => x.CategoryID == categoryId).ToListAsync();
        return products;
    }
}
