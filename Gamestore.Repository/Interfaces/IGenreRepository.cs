using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGenreRepository : IRepository<Category>, IRepositoryBase<Category>
{
    Task<List<Product>> GetGamesByGenreAsync(Guid id);

    Task<List<Category>> GetGenresByParentGenreAsync(Guid id);
}
