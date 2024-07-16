using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface ICategoryRepository
{
    Task<List<MongoCategory>> GetAllAsync();

    Task<MongoCategory> GetCategoryById(int id);

    Task UpdateAsync(MongoCategory entity);

    Task AddAsync(MongoCategory entity);

    Task DeleteAsync(MongoCategory entity);
}
