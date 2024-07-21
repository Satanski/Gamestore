using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGameRepository : IRepository<Game>, IRepositoryBase<Game>
{
    Task<List<Game>> GetAllWithDeletedAsync();

    Task<Game?> GetGameByKeyAsync(string key);

    IQueryable<Game> GetGamesAsQueryable();

    IQueryable<Game> GetGamesWithDeletedAsQueryable();

    Task<List<Genre>> GetGenresByGameAsync(Guid id);

    Task<List<Platform>> GetPlatformsByGameAsync(Guid id);

    Task<Publisher?> GetPublisherByGameAsync(Guid gameId);

    Task SoftDelete(Game game);
}
