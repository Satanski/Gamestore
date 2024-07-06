using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.MongoRepository;

public class MongoUnitOfWork(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository) : IMongoUnitOfWork
{
    public IProductRepository ProductRepository => productRepository;

    public ICategoryRepository CategoryRepository => categoryRepository;

    public ISupplierRepository SupplierRepository => supplierRepository;
}
