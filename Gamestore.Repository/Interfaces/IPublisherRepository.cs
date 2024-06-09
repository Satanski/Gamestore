using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IPublisherRepository : IRepository<Publisher>, IRepositoryBase<Publisher>
{
    Task<Publisher?> GetByCompanyNameAsync(string companyName);

    Task<List<Game>> GetGamesByPublisherAsync(Guid id);
}
