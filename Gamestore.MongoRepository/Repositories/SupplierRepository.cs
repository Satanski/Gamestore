using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class SupplierRepository(IMongoDatabase database) : ISupplierRepository
{
    public async Task<List<Supplier>> GetAllAsync()
    {
        var collection = database.GetCollection<Supplier>("suppliers");
        var supplier = await collection.Find(_ => true).ToListAsync();
        return supplier;
    }

    public async Task<Supplier> GetByIdAsync(int id)
    {
        var collection = database.GetCollection<Supplier>("suppliers");
        var supplier = await collection.Find(x => x.SupplierID == id).FirstOrDefaultAsync();
        return supplier;
    }

    public async Task<Supplier> GetByNameAsync(string companyName)
    {
        var collection = database.GetCollection<Supplier>("suppliers");
        var supplier = await collection.Find(x => x.CompanyName == companyName).FirstOrDefaultAsync();
        return supplier;
    }
}
