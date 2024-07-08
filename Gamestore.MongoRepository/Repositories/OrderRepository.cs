using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class OrderRepository(IMongoDatabase database) : IOrderRepository
{
    public async Task<List<MongoOrder>?> GetAllAsync()
    {
        var collection = database.GetCollection<MongoOrder>("orders");
        var products = await collection.Find(_ => true).ToListAsync();
        return products;
    }
}
