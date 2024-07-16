using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IGameGenreRepository : IRepositoryBase<ProductCategory>
{
    Task<List<ProductCategory>> GetByGameIdAsync(Guid id);
}
