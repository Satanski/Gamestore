namespace Gamestore.DAL.Interfaces;

public interface IRepositoryBase<T>
{
    Task AddAsync(T entity);

    void Delete(T entity);

    Task<List<T>> GetAllAsync();
}
