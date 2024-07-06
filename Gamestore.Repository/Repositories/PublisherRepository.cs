using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class PublisherRepository(GamestoreContext context) : RepositoryBase<Publisher>(context), IPublisherRepository
{
    private readonly GamestoreContext _context = context;

    public Task<Publisher?> GetByIdAsync(Guid id)
    {
        return _context.Publishers.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<Publisher?> GetByCompanyNameAsync(string companyName)
    {
        return _context.Publishers.Where(x => x.CompanyName == companyName).FirstOrDefaultAsync();
    }

    public Task<List<Game>> GetGamesByPublisherIdAsync(Guid id)
    {
        return _context.Games.Where(x => x.PublisherId == id && !x.IsDeleted).ToListAsync();
    }

    public Task<List<Game>> GetGamesByPublisherNameAsync(string name)
    {
        return _context.Games.Where(x => x.Publisher.CompanyName == name && !x.IsDeleted).ToListAsync();
    }

    public async Task UpdateAsync(Publisher entity)
    {
        var g = await _context.Publishers.Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }

    public Task<List<Publisher>> GetAllAsync()
    {
        return _context.Publishers.ToListAsync();
    }
}
