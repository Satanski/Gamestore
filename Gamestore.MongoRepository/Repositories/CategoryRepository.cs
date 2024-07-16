using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class CategoryRepository(IMongoDatabase database) : ICategoryRepository
{
    public Task<List<MongoCategory>> GetAllAsync()
    {
        var collection = database.GetCollection<MongoCategory>("categories");
        var category = collection.Find(_ => true).ToListAsync();
        return category;
    }

    public Task<MongoCategory> GetCategoryById(int id)
    {
        var collection = database.GetCollection<MongoCategory>("categories");
        var category = collection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
        return category;
    }
}
