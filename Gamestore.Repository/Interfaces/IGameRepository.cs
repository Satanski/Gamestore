using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGameRepository : IRepository<Product>, IRepositoryBase<Product>
{
    Task<Product?> GetGameByKeyAsync(string key);

    IQueryable<Product> GetGamesAsQueryable();

    Task<List<Category>> GetGenresByGameAsync(Guid id);

    Task<List<Platform>> GetPlatformsByGameAsync(Guid id);

    Task<Supplier?> GetPublisherByGameAsync(Guid gameId);

    Task SoftDelete(Product game);
}
