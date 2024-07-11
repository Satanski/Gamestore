using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class CategoryRepository(IMongoDatabase database) : ICategoryRepository
{
    public Task<List<Category>> GetAllAsync()
    {
        var collection = database.GetCollection<Category>("categories");
        var category = collection.Find(_ => true).ToListAsync();
        return category;
    }

    public Task<Category> GetCategoryById(int id)
    {
        var collection = database.GetCollection<Category>("categories");
        var category = collection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
        return category;
    }
}
