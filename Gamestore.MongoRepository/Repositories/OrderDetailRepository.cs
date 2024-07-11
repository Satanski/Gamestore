using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;
internal class OrderDetailRepository(IMongoDatabase database) : IOrderDetailRepository
{
    public Task<List<MongoOrderDetail>> GetByOrderIdAsync(int id)
    {
        var collection = database.GetCollection<MongoOrderDetail>("order-details");
        var order = collection.Find(x => x.OrderId == id).ToListAsync();
        return order;
    }
}
