using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IPublisherRepository : IRepository<Supplier>, IRepositoryBase<Supplier>
{
    Task<Supplier?> GetByCompanyNameAsync(string companyName);

    Task<List<Product>> GetGamesByPublisherIdAsync(Guid id);

    Task<List<Product>> GetGamesByPublisherNameAsync(string name);
}
