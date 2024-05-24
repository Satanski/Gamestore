namespace Gamestore.DAL.Interfaces;

public interface IRepository<T>
{
    Task AddAsync(T entity);

    Task Delete(Guid id);

    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task UpdateAsync(T entity);
}
