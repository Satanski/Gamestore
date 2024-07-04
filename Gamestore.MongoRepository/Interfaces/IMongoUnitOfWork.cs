namespace Gamestore.MongoRepository.Interfaces;

public interface IMongoUnitOfWork
{
    IProductRepository ProductRepository { get; }
}
