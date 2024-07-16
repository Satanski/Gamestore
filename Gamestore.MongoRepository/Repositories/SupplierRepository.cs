using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class SupplierRepository(IMongoDatabase database) : ISupplierRepository
{
    public async Task<List<MongoSupplier>> GetAllAsync()
    {
        var collection = database.GetCollection<MongoSupplier>("suppliers");
        var supplier = await collection.Find(_ => true).ToListAsync();
        return supplier;
    }

    public async Task<MongoSupplier> GetByIdAsync(int id)
    {
        var collection = database.GetCollection<MongoSupplier>("suppliers");
        var supplier = await collection.Find(x => x.SupplierID == id).FirstOrDefaultAsync();
        return supplier;
    }

    public async Task<MongoSupplier> GetByNameAsync(string companyName)
    {
        var collection = database.GetCollection<MongoSupplier>("suppliers");
        var supplier = await collection.Find(x => x.CompanyName == companyName).FirstOrDefaultAsync();
        return supplier;
    }
}
