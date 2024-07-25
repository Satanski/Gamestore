namespace Gamestore.DAL.Interfaces;

public interface IRepository<T>
{
    Task<T?> GetByOrderIdAsync(Guid id);

    Task UpdateAsync(T entity);
}
