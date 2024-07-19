using Gamestore.BLL.Models;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.MongoRepository.LoggingModels;
using Gamestore.Services.Models;

namespace Gamestore.BLL.MongoLogging;

public class MongoLoggingService(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork) : IMongoLoggingService
{
    private readonly string _gameEntityType = "Game";

    private readonly string _updateAction = "Update";

    private readonly string _addAction = "Add";

    private readonly string _deleteAction = "Delete";

    public async Task LogGameUpdateAsync(GameModelDto oldValue, GameDtoWrapper newValue)
    {
        List<MongoGenre> oldGenres = [];
        List<MongoGenre> newGenres = [];
        List<MongoPlatform> oldPlatforms = [];
        List<MongoPlatform> newPlatforms = [];

        var oldPublisherId = oldValue.Publisher.Id.ToString();
        var newPublisherId = newValue.Publisher.ToString();

        AddGenresToList(oldValue, oldGenres);
        await AddGenresToList(unitOfWork, newValue, newGenres);
        AddPlatformsToList(oldValue, oldPlatforms);
        await AddPlatformsToList(unitOfWork, newValue, newPlatforms);

        var mongoLogEntry = new GameUpdateLogEntry()
        {
            Date = DateTime.Now,
            Action = $"{_updateAction}",
            EntityType = $"{_gameEntityType}",
            NewValue = $"{oldValue}",
            OldValue = $"{newValue}",
            OldPublisherId = oldPublisherId!,
            OldGenres = oldGenres,
            OldPlatforms = oldPlatforms,
            NewPublisherId = newPublisherId,
            NewGenres = newGenres,
            NewPlatforms = newPlatforms,
        };

        await mongoUnitOfWork.LogRepository.LogGame(mongoLogEntry);
    }

    public async Task LogGameAddAsync(GameDtoWrapper value)
    {
        List<MongoGenre> genres = [];
        List<MongoPlatform> platforms = [];

        var publisherId = value.Publisher.ToString();

        await AddGenresToList(unitOfWork, value, genres);
        await AddPlatformsToList(unitOfWork, value, platforms);

        GameAddLogEntry mongoLogEntry = new GameAddLogEntry()
        {
            Date = DateTime.Now,
            Action = $"{_addAction}",
            EntityType = $"{_gameEntityType}",
            Value = $"{value}",
            PublisherId = publisherId!,
            Genres = genres,
            Platforms = platforms,
        };

        await mongoUnitOfWork.LogRepository.LogGame(mongoLogEntry);
    }

    public async Task LogGameDeleteAsync(Guid gameId)
    {
        var game = await unitOfWork.GameRepository.GetByIdAsync(gameId);

        GameDeleteLogEntry mongoLogEntry = new GameDeleteLogEntry()
        {
            Date = DateTime.Now,
            Action = $"{_deleteAction}",
            EntityType = $"{_gameEntityType}",
            Value = $"{game}",
        };

        await mongoUnitOfWork.LogRepository.LogGame(mongoLogEntry);
    }

    private static void AddGenresToList(GameModelDto source, List<MongoGenre> destination)
    {
        foreach (var genre in source.Genres)
        {
            destination.Add(new() { Id = genre.Id.ToString()!, ParentGenreId = genre.ParentGenreId.ToString(), Name = genre.Name });
        }
    }

    private static async Task AddGenresToList(IUnitOfWork unitOfWork, GameDtoWrapper source, List<MongoGenre> destination)
    {
        foreach (var genreId in source.Genres)
        {
            var name = (await unitOfWork.GenreRepository.GetByIdAsync(genreId)).Name;
            destination.Add(new() { Id = genreId.ToString(), Name = name });
        }
    }

    private static void AddPlatformsToList(GameModelDto source, List<MongoPlatform> destination)
    {
        foreach (var platform in source.Platforms)
        {
            destination.Add(new() { Id = platform.Id.ToString()!, Type = platform.Type });
        }
    }

    private static async Task AddPlatformsToList(IUnitOfWork unitOfWork, GameDtoWrapper source, List<MongoPlatform> destination)
    {
        foreach (var platformId in source.Platforms)
        {
            var type = (await unitOfWork.PlatformRepository.GetByIdAsync(platformId)).Type;
            destination.Add(new() { Id = platformId.ToString(), Type = type });
        }
    }
}
