using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGameRepository : IRepository<Game>, IRepositoryBase<Game>
{
    Task<Game?> GetGameByKeyAsync(string key);

    Task<List<Genre>> GetGenresByGameAsync(Guid id);

    Task<List<Platform>> GetPlatformsByGameAsync(Guid id);

    Task<Publisher?> GetPublisherByGameAsync(Guid gameId);

    Task SoftDelete(Game game);
}
