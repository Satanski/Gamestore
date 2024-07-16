using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.Repositories;

public class LogRepository(IMongoDatabase database) : ILogRepository
{
    private const string LogsCollectionName = "Logs";

    public async Task Add(LogEntry entry)
    {
        await EnsureLogsCollectionExists(database);

        var collection = database.GetCollection<LogEntry>(LogsCollectionName);
        await collection.InsertOneAsync(entry);
    }

    public async Task LogGame(GameUpdateLogEntry entry)
    {
        await EnsureLogsCollectionExists(database);

        var collection = database.GetCollection<GameUpdateLogEntry>(LogsCollectionName);
        await collection.InsertOneAsync(entry);
    }

    public async Task LogGame(GameDeleteLogEntry entry)
    {
        await EnsureLogsCollectionExists(database);

        var collection = database.GetCollection<GameDeleteLogEntry>(LogsCollectionName);
        await collection.InsertOneAsync(entry);
    }

    public async Task LogGame(GameAddLogEntry entry)
    {
        await EnsureLogsCollectionExists(database);

        var collection = database.GetCollection<GameAddLogEntry>(LogsCollectionName);
        await collection.InsertOneAsync(entry);
    }

    private static async Task EnsureLogsCollectionExists(IMongoDatabase database)
    {
        var collectionNames = await database.ListCollectionNames().ToListAsync();
        if (!collectionNames.Contains(LogsCollectionName))
        {
            await database.CreateCollectionAsync(LogsCollectionName);
        }
    }
}
