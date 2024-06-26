using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class CommentRepository(GamestoreContext context) : RepositoryBase<Comment>(context), ICommentRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<Comment>> GetAllAsync()
    {
        return _context.Comments.ToListAsync();
    }

    public Task<Comment?> GetByIdAsync(Guid id)
    {
        return _context.Comments.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<Comment>> GetChildCommentsByCommentIdAsync(Guid id)
    {
        return _context.Comments.Where(x => x.ParentCommentId == id).ToListAsync();
    }

    public Task<List<Comment>> GetByGameKeyAsync(string key)
    {
        return _context.Comments.Where(x => x.Game.Key == key).ToListAsync();
    }

    public async Task UpdateAsync(Comment entity)
    {
        var g = await _context.Comments.Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }
}
