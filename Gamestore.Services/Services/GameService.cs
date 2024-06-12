using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Models;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.Extensions.Logging;

namespace Gamestore.Services.Services;

public class GameService(IUnitOfWork unitOfWork, IMapper automapper, ILogger<GameService> logger) : IGameService
{
    private readonly GameAddDtoValidator _gameAddDtoValidator = new();
    private readonly GameUpdateDtoValidator _gameUpdateDtoValidator = new();
    private readonly GameAddValidator _gameAddlValidator = new(unitOfWork);
    private readonly GameUpdateValidator _gameUpdateValidator = new(unitOfWork);

    public async Task<IEnumerable<GameModelDto>> GetAllGamesAsync()
    {
        logger.LogInformation("Getting all games");
        var games = await unitOfWork.GameRepository.GetAllAsync();
        List<GameModelDto> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(automapper.Map<GameModelDto>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task<IEnumerable<GenreModel>> GetGenresByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting genres by game Id: {gameKey}", gameKey);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
        var genres = await unitOfWork.GameRepository.GetGenresByGameAsync(game.Id);
        List<GenreModel> genreModels = [];

        foreach (var genre in genres)
        {
            genreModels.Add(automapper.Map<GenreModel>(genre));
        }

        return genreModels.AsEnumerable();
    }

    public async Task<IEnumerable<PlatformModel>> GetPlatformsByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting platforms by game Key: {gameKey}", gameKey);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
        var platforms = await unitOfWork.GameRepository.GetPlatformsByGameAsync(game.Id);
        List<PlatformModel> platformModels = [];

        foreach (var platform in platforms)
        {
            platformModels.Add(automapper.Map<PlatformModel>(platform));
        }

        return platformModels.AsEnumerable();
    }

    public async Task<PublisherModel> GetPublisherByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting publisher by game Key: {gameKey}", gameKey);

        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
        var publisher = await unitOfWork.GameRepository.GetPublisherByGameAsync(game.Id);

        return automapper.Map<PublisherModel>(publisher);
    }

    public async Task<GameModelDto> GetGameByIdAsync(Guid gameId)
    {
        logger.LogInformation("Getting game by Id: {gameId}", gameId);
        var game = await unitOfWork.GameRepository.GetByIdAsync(gameId);

        return game == null ? throw new GamestoreException($"No game found with given id: {gameId}") : automapper.Map<GameModelDto>(game);
    }

    public async Task<GameModel> GetGameByKeyAsync(string key)
    {
        logger.LogInformation("Getting game by Key: {key}", key);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(key);

        return game == null ? throw new GamestoreException($"No game found with given key: {key}") : automapper.Map<GameModel>(game);
    }

    public async Task AddGameAsync(GameAddDto gameModel)
    {
        logger.LogInformation("Adding game {@gameModel}", gameModel);
        var result = await _gameAddDtoValidator.ValidateAsync(gameModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        result = await _gameAddlValidator.ValidateAsync(gameModel.Game);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var game = automapper.Map<Game>(gameModel.Game);
        game.PublisherId = gameModel.Publisher;

        var addedGame = await unitOfWork.GameRepository.AddAsync(game);
        await unitOfWork.SaveAsync();

        var genres = gameModel.Genres;
        foreach (var genreId in genres)
        {
            await unitOfWork.GameGenreRepository.AddAsync(new GameGenre() { GameId = addedGame.Id, GenreId = genreId });
        }

        var platforms = gameModel.Platforms;
        foreach (var platformId in platforms)
        {
            await unitOfWork.GamePlatformRepository.AddAsync(new GamePlatform() { GameId = addedGame.Id, PlatformId = platformId });
        }

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateGameAsync(GameUpdateDto gameModel)
    {
        logger.LogInformation("Updating game {@gameModel}", gameModel);

        var result = await _gameUpdateDtoValidator.ValidateAsync(gameModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        result = await _gameUpdateValidator.ValidateAsync(gameModel.Game);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        await DeleteGameGenresFromRepository(unitOfWork, gameModel.Game.Id);
        await DeleteGamePlatformsFromRepository(unitOfWork, gameModel.Game.Id);

        await unitOfWork.SaveAsync();

        var game = automapper.Map<Game>(gameModel.Game);
        game.PublisherId = gameModel.Publisher;
        await unitOfWork.GameRepository.UpdateAsync(game);
        await unitOfWork.SaveAsync();

        var genres = gameModel.Genres;
        foreach (var genreId in genres)
        {
            await unitOfWork.GameGenreRepository.AddAsync(new GameGenre() { GameId = gameModel.Game.Id, GenreId = genreId });
        }

        var platforms = gameModel.Platforms;
        foreach (var platformId in platforms)
        {
            await unitOfWork.GamePlatformRepository.AddAsync(new GamePlatform() { GameId = gameModel.Game.Id, PlatformId = platformId });
        }

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteGameByIdAsync(Guid gameId)
    {
        logger.LogInformation("Deleting game by Id: {gameId}", gameId);
        var game = await unitOfWork.GameRepository.GetByIdAsync(gameId);

        if (game != null)
        {
            var genres = await unitOfWork.GameGenreRepository.GetByGameIdAsync(game.Id);
            if (genres != null)
            {
                foreach (var item in genres)
                {
                    unitOfWork.GameGenreRepository.Delete(item);
                }
            }

            var platforms = await unitOfWork.GamePlatformRepository.GetByGameIdAsync(game.Id);
            if (platforms != null)
            {
                foreach (var item in platforms)
                {
                    unitOfWork.GamePlatformRepository.Delete(item);
                }
            }

            await unitOfWork.SaveAsync();

            unitOfWork.GameRepository.Delete(game);

            await unitOfWork.SaveAsync();
        }
        else
        {
            throw new GamestoreException($"No game found with given id: {gameId}");
        }
    }

    public async Task DeleteGameByKeyAsync(string gameKey)
    {
        logger.LogInformation("Deleting game by Key: {gameKey}", gameKey);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);

        if (game != null)
        {
            var genres = await unitOfWork.GameGenreRepository.GetByGameIdAsync(game.Id);
            if (genres != null)
            {
                foreach (var item in genres)
                {
                    unitOfWork.GameGenreRepository.Delete(item);
                }
            }

            var platforms = await unitOfWork.GamePlatformRepository.GetByGameIdAsync(game.Id);
            if (platforms != null)
            {
                foreach (var item in platforms)
                {
                    unitOfWork.GamePlatformRepository.Delete(item);
                }
            }

            await unitOfWork.SaveAsync();

            unitOfWork.GameRepository.Delete(game);

            await unitOfWork.SaveAsync();
        }
        else
        {
            throw new GamestoreException($"No game found with given key: {gameKey}");
        }
    }

    private static async Task DeleteGamePlatformsFromRepository(IUnitOfWork unitOfWork, Guid gameId)
    {
        var gamePlatforms = await unitOfWork.GamePlatformRepository.GetByGameIdAsync(gameId);
        foreach (var item in gamePlatforms)
        {
            unitOfWork.GamePlatformRepository.Delete(item);
        }
    }

    private static async Task DeleteGameGenresFromRepository(IUnitOfWork unitOfWork, Guid gameId)
    {
        var gameGenres = await unitOfWork.GameGenreRepository.GetByGameIdAsync(gameId);
        foreach (var item in gameGenres)
        {
            unitOfWork.GameGenreRepository.Delete(item);
        }
    }
}
