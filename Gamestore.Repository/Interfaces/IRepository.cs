namespace Gamestore.DAL.Interfaces;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);

    Task UpdateAsync(T entity);
}
