namespace Gamestore.DAL.Interfaces;

public interface IRepository<T>
{
    Task AddAsync(T entity);

    void Delete(T entity);

    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task UpdateAsync(T entity);
}
