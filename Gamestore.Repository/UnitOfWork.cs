using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.DAL;

public class UnitOfWork(GamestoreContext context, IGameRepository gameRepository, IGenreRepository genreRepository, IPlatformRepository platformRepository) : IUnitOfWork
{
    private readonly GamestoreContext _context = context;

    public IGameRepository GameRepository { get; } = gameRepository;

    public IGenreRepository GenreRepository { get; } = genreRepository;

    public IPlatformRepository PlatformRepository { get; } = platformRepository;

    public Task SaveAsync()
    {
        Task t = Task.Run(() => _context.SaveChangesAsync());

        return t;
    }
}
