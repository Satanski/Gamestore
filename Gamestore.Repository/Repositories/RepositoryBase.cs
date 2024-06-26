using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class RepositoryBase<T>(GamestoreContext context)
    where T : class
{
    private readonly DbSet<T> _entities = context.Set<T>();

    protected GamestoreContext Context { get; } = context;

    public async Task<T> AddAsync(T entity)
    {
        var added = await _entities.AddAsync(entity);

        return added.Entity;
    }

    public void Delete(T entity)
    {
        _entities.Remove(entity);
    }
}
