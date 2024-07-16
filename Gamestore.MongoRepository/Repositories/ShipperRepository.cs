using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class ShipperRepository(IMongoDatabase database) : IShipperRepository
{
    public async Task<List<MongoShipper>> GetAllAsync()
    {
        var collection = database.GetCollection<MongoShipper>("shippers");
        var shippers = await collection.Find(_ => true).ToListAsync();
        return shippers;
    }
}
