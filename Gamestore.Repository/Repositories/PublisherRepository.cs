using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
public class PublisherRepository(GamestoreContext context) : RepositoryBase<Publisher>(context), IPublisherRepository
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    public Task<Publisher?> GetByIdAsync(Guid id)
    {
        return context.Publishers.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<Publisher?> GetByCompanyNameAsync(string companyName)
    {
        return context.Publishers.Where(x => x.CompanyName == companyName).FirstOrDefaultAsync();
    }

    public Task<List<Game>> GetGamesByPublisherIdAsync(Guid id)
    {
        return context.Games.Where(x => x.PublisherId == id && !x.IsDeleted).ToListAsync();
    }

    public Task<List<Game>> GetGamesByPublisherNameAsync(string name)
    {
        return context.Games.Where(x => x.Name == name && !x.IsDeleted).ToListAsync();
    }

    public async Task UpdateAsync(Publisher entity)
    {
        var g = await context.Publishers.Where(p => p.Id == entity.Id).FirstAsync();
        context.Entry(g).CurrentValues.SetValues(entity);
    }

    public Task<List<Publisher>> GetAllAsync()
    {
        return context.Publishers.ToListAsync();
    }
}
