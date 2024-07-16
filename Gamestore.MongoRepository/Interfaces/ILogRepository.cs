using Gamestore.MongoRepository.Entities;

namespace Gamestore.MongoRepository.Interfaces;

public interface ILogRepository
{
    Task Add(LogEntry entry);

    Task LogGame(GameUpdateLogEntry entry);

    Task LogGame(GameAddLogEntry entry);

    Task LogGame(GameDeleteLogEntry entry);
}
