namespace Gamestore.DAL.Interfaces;

public interface IRepositoryBase<T>
{
    Task<T> AddAsync(T entity);

    void Delete(T entity);

    Task<List<T>> GetAllAsync();
}
