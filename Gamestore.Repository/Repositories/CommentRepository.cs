using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
public class CommentRepository(GamestoreContext context) : RepositoryBase<Comment>(context), ICommentRepository
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    public Task<List<Comment>> GetAllAsync()
    {
        return context.Comments.ToListAsync();
    }

    public Task<Comment?> GetByIdAsync(Guid id)
    {
        return context.Comments.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<Comment>> GetChildCommentsByCommentIdAsync(Guid id)
    {
        return context.Comments.Where(x => x.ParentCommentId == id).ToListAsync();
    }

    public Task<List<Comment>> GetByGameKeyAsync(string key)
    {
        return context.Comments.Where(x => x.Game.Key == key).ToListAsync();
    }

    public async Task UpdateAsync(Comment entity)
    {
        var g = await context.Comments.Where(p => p.Id == entity.Id).FirstAsync();
        context.Entry(g).CurrentValues.SetValues(entity);
    }
}
