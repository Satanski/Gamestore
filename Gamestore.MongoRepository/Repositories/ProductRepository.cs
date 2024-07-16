using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class ProductRepository(IMongoDatabase database) : IProductRepository
{
    public Task<List<MongoProduct>> GetAllAsync()
    {
        var collection = database.GetCollection<MongoProduct>("products");
        var products = collection.Find(_ => true).ToListAsync();
        return products;
    }

    public Task<MongoProduct> GetByIdAsync(int id)
    {
        var collection = database.GetCollection<MongoProduct>("products");
        var product = collection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
        return product;
    }

    public IQueryable<MongoProduct> GetProductsAsQueryable()
    {
        return database.GetCollection<MongoProduct>("products").AsQueryable();
    }

    public Task<MongoProduct> GetByNameAsync(string key)
    {
        var collection = database.GetCollection<MongoProduct>("products");
        var products = collection.Find(x => x.ProductName == key).FirstOrDefaultAsync();
        return products;
    }

    public Task<List<MongoProduct>> GetBySupplierIdAsync(int supplierID)
    {
        var collection = database.GetCollection<MongoProduct>("products");
        var products = collection.Find(x => x.SupplierID == supplierID).ToListAsync();
        return products;
    }

    public Task<List<MongoProduct>> GetByCategoryIdAsync(int categoryId)
    {
        var collection = database.GetCollection<MongoProduct>("products");
        var products = collection.Find(x => x.CategoryID == categoryId).ToListAsync();
        return products;
    }
}
