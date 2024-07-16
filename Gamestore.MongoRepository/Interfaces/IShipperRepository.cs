using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface IShipperRepository
{
    Task<List<MongoShipper>> GetAllAsync();

    Task<MongoShipper> GetByIdAsync(int id);

    Task UpdateAsync(MongoShipper entity);

    Task AddAsync(MongoShipper entity);

    Task DeleteAsync(MongoShipper entity);
}
