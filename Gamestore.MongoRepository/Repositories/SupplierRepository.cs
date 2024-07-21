using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class SupplierRepository(IMongoDatabase database) : ISupplierRepository
{
    private const string CollectionName = "suppliers";
    private readonly IMongoCollection<MongoSupplier> _collection = database.GetCollection<MongoSupplier>(CollectionName);

    public async Task<List<MongoSupplier>> GetAllAsync()
    {
        var supplier = await _collection.Find(_ => true).ToListAsync();
        return supplier;
    }

    public async Task<MongoSupplier> GetByIdAsync(int id)
    {
        var supplier = await _collection.Find(x => x.SupplierID == id).FirstOrDefaultAsync();
        return supplier;
    }

    public async Task<MongoSupplier> GetByNameAsync(string companyName)
    {
        var supplier = await _collection.Find(x => x.CompanyName == companyName).FirstOrDefaultAsync();
        return supplier;
    }
}
