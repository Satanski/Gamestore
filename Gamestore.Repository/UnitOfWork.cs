using Gamestore.Repository.Entities;
using Gamestore.Repository.Interfaces;
using Gamestore.Repository.Repositories;

namespace Gamestore.Repository;

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
