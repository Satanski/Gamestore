using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class ProductRepository(IMongoDatabase database) : IProductRepository
{
    private const string CollectionName = "products";
    private readonly IMongoCollection<MongoProduct> _collection = database.GetCollection<MongoProduct>(CollectionName);

    public Task<List<MongoProduct>> GetAllAsync()
    {
        var products = _collection.Find(_ => true).ToListAsync();
        return products;
    }

    public Task<MongoProduct> GetByIdAsync(int id)
    {
        var product = _collection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
        return product;
    }

    public IQueryable<MongoProduct> GetProductsAsQueryable()
    {
        return database.GetCollection<MongoProduct>("products").AsQueryable();
    }

    public Task<MongoProduct> GetByNameAsync(string key)
    {
        var products = _collection.Find(x => x.ProductName == key).FirstOrDefaultAsync();
        return products;
    }

    public Task<List<MongoProduct>> GetBySupplierIdAsync(int supplierID)
    {
        var products = _collection.Find(x => x.SupplierID == supplierID).ToListAsync();
        return products;
    }

    public Task<List<MongoProduct>> GetByCategoryIdAsync(int categoryId)
    {
        var products = _collection.Find(x => x.CategoryID == categoryId).ToListAsync();
        return products;
    }
}
