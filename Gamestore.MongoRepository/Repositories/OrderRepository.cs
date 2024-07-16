using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Gamestore.MongoRepository.Repositories;

public class OrderRepository(IMongoDatabase database) : IOrderRepository
{
    private const string CollectionName = "orders";
    private readonly IMongoCollection<MongoOrder> _collection = database.GetCollection<MongoOrder>(CollectionName);

    public Task<List<MongoOrder>> GetAllAsync()
    {
        var orders = _collection.Find(_ => true).ToListAsync();
        return orders;
    }

    public Task<MongoOrder> GetByIdAsync(int id)
    {
        var order = _collection.Find(x => x.OrderId == id).FirstOrDefaultAsync();
        return order;
    }

    public async Task UpdateAsync(MongoOrder entity)
    {
        var filter = Builders<MongoOrder>.Filter.Eq("_id", entity.ObjectId);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task AddAsync(MongoOrder entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(MongoOrder entity)
    {
        var filter = Builders<MongoOrder>.Filter.Eq("_id", entity.ObjectId);
        await _collection.DeleteOneAsync(filter);
    }
}
