using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
public class PublisherRepository(GamestoreContext context) : RepositoryBase<Publisher>(context), IPublisherRepository
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    public async Task<Publisher?> GetByIdAsync(Guid id)
    {
        return await context.Publishers.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Publisher?> GetByCompanyNameAsync(string companyName)
    {
        return await context.Publishers.Where(x => x.CompanyName == companyName).FirstOrDefaultAsync();
    }

    public async Task<List<Game>> GetGamesByPublisherAsync(Guid id)
    {
        return await context.Games.Where(x => x.PublisherId == id).ToListAsync();
    }

    public async Task UpdateAsync(Publisher entity)
    {
        var g = await context.Publishers.Where(p => p.Id == entity.Id).FirstAsync();
        context.Entry(g).CurrentValues.SetValues(entity);
    }

    public async Task<List<Publisher>> GetAllAsync()
    {
        return await context.Publishers.ToListAsync();
    }
}
