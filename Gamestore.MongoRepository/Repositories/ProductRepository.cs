using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class ProductRepository(IMongoDatabase database) : IProductRepository
{
    public async Task<List<Product>?> GetAllAsync()
    {
        var collection = database.GetCollection<Product>("products");
        var products = await collection.Find(_ => true).ToListAsync();
        return products;
    }
}
