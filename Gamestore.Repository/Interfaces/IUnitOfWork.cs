namespace Gamestore.DAL.Interfaces;

public interface IUnitOfWork
{
    IGameRepository GameRepository { get; }

    IPlatformRepository PlatformRepository { get; }

    IGenreRepository GenreRepository { get; }

    IGameGenreRepository GameGenreRepository { get; }

    IGamePlatformRepository GamePlatformRepository { get; }

    IPublisherRepository PublisherRepository { get; }

    Task SaveAsync();
}
