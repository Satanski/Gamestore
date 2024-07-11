using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class ProductRepository(IMongoDatabase database) : IProductRepository
{
    public Task<List<Product>> GetAllAsync()
    {
        var collection = database.GetCollection<Product>("products");
        var products = collection.Find(_ => true).ToListAsync();
        return products;
    }

    public Task<Product> GetByIdAsync(int id)
    {
        var collection = database.GetCollection<Product>("products");
        var product = collection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
        return product;
    }

    public IQueryable<Product> GetProductsAsQueryable()
    {
        return database.GetCollection<Product>("products").AsQueryable();
    }

    public Task<Product> GetByNameAsync(string key)
    {
        var collection = database.GetCollection<Product>("products");
        var products = collection.Find(x => x.ProductName == key).FirstOrDefaultAsync();
        return products;
    }

    public Task<List<Product>> GetBySupplierIdAsync(int supplierID)
    {
        var collection = database.GetCollection<Product>("products");
        var products = collection.Find(x => x.SupplierID == supplierID).ToListAsync();
        return products;
    }

    public Task<List<Product>> GetByCategoryIdAsync(int categoryId)
    {
        var collection = database.GetCollection<Product>("products");
        var products = collection.Find(x => x.CategoryID == categoryId).ToListAsync();
        return products;
    }
}
