using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.DAL;

public class UnitOfWork(GamestoreContext context, IGameRepository gameRepository, IGenreRepository genreRepository,
    IPlatformRepository platformRepository, IGameGenreRepository gameGenreRepository, IGamePlatformRepository gamePlatformRepository) : IUnitOfWork
{
    private readonly GamestoreContext _context = context;

    public IGameRepository GameRepository { get; } = gameRepository;

    public IGenreRepository GenreRepository { get; } = genreRepository;

    public IPlatformRepository PlatformRepository { get; } = platformRepository;

    public IGameGenreRepository GameGenreRepository { get; } = gameGenreRepository;

    public IGamePlatformRepository GamePlatformRepository { get; } = gamePlatformRepository;

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
