namespace Gamestore.MongoRepository.Interfaces;

public interface IMongoUnitOfWork
{
    IProductRepository ProductRepository { get; }

    ICategoryRepository CategoryRepository { get; }

    ISupplierRepository SupplierRepository { get; }

    IShipperRepository ShipperRepository { get; }

    IOrderRepository OrderRepository { get; }

    IOrderDetailRepository OrderDetailRepository { get; }
}
