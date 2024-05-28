using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class RepositoryBase<T>(GamestoreContext context)
    where T : class
{
    private readonly DbSet<T> _entities = context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _entities.Remove(entity);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }
}
