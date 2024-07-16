using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class ShipperRepository(IMongoDatabase database) : IShipperRepository
{
    private const string CollectionName = "shippers";
    private readonly IMongoCollection<MongoShipper> _collection = database.GetCollection<MongoShipper>(CollectionName);

    public async Task<List<MongoShipper>> GetAllAsync()
    {
        var shippers = await _collection.Find(_ => true).ToListAsync();
        return shippers;
    }

    public async Task UpdateAsync(MongoShipper entity)
    {
        var filter = Builders<MongoShipper>.Filter.Eq("_id", entity.ObjectId);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task AddAsync(MongoShipper entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(MongoShipper entity)
    {
        var filter = Builders<MongoShipper>.Filter.Eq("_id", entity.ObjectId);
        await _collection.DeleteOneAsync(filter);
    }
}
