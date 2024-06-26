using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface ICommentRepository : IRepository<Comment>, IRepositoryBase<Comment>
{
    Task<List<Comment>> GetByGameKeyAsync(string key);

    Task<List<Comment>> GetChildCommentsByCommentIdAsync(Guid id);
}
