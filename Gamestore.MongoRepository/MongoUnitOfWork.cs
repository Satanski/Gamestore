using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.MongoRepository;

public class MongoUnitOfWork(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    ISupplierRepository supplierRepository,
    IShipperRepository shipperRepository,
    IOrderRepository orderRepository) : IMongoUnitOfWork
{
    public IProductRepository ProductRepository => productRepository;

    public ICategoryRepository CategoryRepository => categoryRepository;

    public ISupplierRepository SupplierRepository => supplierRepository;

    public IShipperRepository ShipperRepository => shipperRepository;

    public IOrderRepository OrderRepository => orderRepository;
}
