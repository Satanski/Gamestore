using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface ICategoryRepository
{
    Task<List<MongoCategory>> GetAllAsync();

    Task<MongoCategory> GetCategoryById(int id);
}
