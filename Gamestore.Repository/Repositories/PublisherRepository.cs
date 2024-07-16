using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class PublisherRepository(GamestoreContext context) : RepositoryBase<Supplier>(context), IPublisherRepository
{
    private readonly GamestoreContext _context = context;

    public Task<Supplier?> GetByIdAsync(Guid id)
    {
        return _context.Publishers.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<Supplier?> GetByCompanyNameAsync(string companyName)
    {
        return _context.Publishers.Where(x => x.CompanyName == companyName).FirstOrDefaultAsync();
    }

    public Task<List<Product>> GetGamesByPublisherIdAsync(Guid id)
    {
        return _context.Products.Where(x => x.PublisherId == id && !x.IsDeleted).ToListAsync();
    }

    public Task<List<Product>> GetGamesByPublisherNameAsync(string name)
    {
        return _context.Products.Where(x => x.Publisher.CompanyName == name && !x.IsDeleted).ToListAsync();
    }

    public async Task UpdateAsync(Supplier entity)
    {
        var g = await _context.Publishers.Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }

    public Task<List<Supplier>> GetAllAsync()
    {
        return _context.Publishers.ToListAsync();
    }
}
