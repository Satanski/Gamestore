using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.MongoRepository;

public class MongoUnitOfWork(IProductRepository productRepository) : IMongoUnitOfWork
{
    public IProductRepository ProductRepository => productRepository;
}
