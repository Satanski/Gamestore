using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface IProductRepository
{
    Task<List<Product>?> GetAllAsync();
}
