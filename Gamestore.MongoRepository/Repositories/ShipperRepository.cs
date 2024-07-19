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

    public async Task<MongoShipper> GetByIdAsync(int id)
    {
        var shipper = await _collection.Find(x => x.ShipperID == id).FirstOrDefaultAsync();
        return shipper;
    }
}
