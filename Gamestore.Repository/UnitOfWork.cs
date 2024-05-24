using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.DAL.Repositories;

namespace Gamestore.DAL;

public class UnitOfWork(GamestoreContext context) : IUnitOfWork
{
    private readonly GamestoreContext _context = context;

    public IGameRepository GameRepository { get; } = new GameRepository(context);

    public IPlatformRepository PlatformRepository { get; } = new PlatformRepository(context);

    public IGenreRepository GenreRepository { get; } = new GenreRepository(context);

    public Task SaveAsync()
    {
        Task t = Task.Run(() => _context.SaveChangesAsync());

        return t;
    }
}
