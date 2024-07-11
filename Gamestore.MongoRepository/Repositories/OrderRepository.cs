using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Gamestore.MongoRepository.Repositories;

public class OrderRepository(IMongoDatabase database) : IOrderRepository
{
    public Task<List<MongoOrder>> GetAllAsync()
    {
        var collection = database.GetCollection<MongoOrder>("orders");
        var orders = collection.Find(_ => true).ToListAsync();
        return orders;
    }

    public Task<MongoOrder> GetByIdAsync(int id)
    {
        var collection = database.GetCollection<MongoOrder>("orders");
        var order = collection.Find(x => x.OrderId == id).FirstOrDefaultAsync();
        return order;
    }

    public MongoOrder GetWithDetailsByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
