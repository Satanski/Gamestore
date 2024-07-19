using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;
internal class OrderDetailRepository(IMongoDatabase database) : IOrderDetailRepository
{
    private const string CollectionName = "order-details";
    private readonly IMongoCollection<MongoOrderDetail> _collection = database.GetCollection<MongoOrderDetail>(CollectionName);

    public Task<List<MongoOrderDetail>> GetByOrderIdAsync(int id)
    {
        var order = _collection.Find(x => x.OrderId == id).ToListAsync();
        return order;
    }
}
