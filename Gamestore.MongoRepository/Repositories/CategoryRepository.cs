using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class CategoryRepository(IMongoDatabase database) : ICategoryRepository
{
    public async Task<List<Category>> GetAllAsync()
    {
        var collection = database.GetCollection<Category>("categories");
        var category = await collection.Find(_ => true).ToListAsync();
        return category;
    }

    public async Task<Category> GetCategoryById(int id)
    {
        var collection = database.GetCollection<Category>("categories");
        var category = await collection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
        return category;
    }
}
