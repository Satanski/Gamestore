using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IPublisherRepository : IRepository<Publisher>, IRepositoryBase<Publisher>
{
    Task<Publisher?> GetByCompanyNameAsync(string companyName);

    Task<List<Game>> GetGamesByPublisherIdAsync(Guid id);

    Task<List<Game>> GetGamesByPublisherNameAsync(string name);
}
