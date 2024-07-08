using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface IShipperRepository
{
    Task<List<Shipper>?> GetAllAsync();
}
