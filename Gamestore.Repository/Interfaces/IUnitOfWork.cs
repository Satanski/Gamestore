namespace Gamestore.Repository.Interfaces;

public interface IUnitOfWork
{
    IGameRepository GameRepository { get; }

    IPlatformRepository PlatformRepository { get; }

    Task SaveAsync();
}
