using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.Extensions.Logging;

namespace Gamestore.Services.Services;

public class GameService(IUnitOfWork unitOfWork, IMapper automapper, ILogger<GameService> logger) : IGameService
{
    private readonly GameModelDtoValidator _gameModelDtoValidator = new(unitOfWork);
    private readonly GameModelDtoUpdateValidator _gameModelDtoUpdateValidator = new(unitOfWork);

    public async Task<IEnumerable<GameModel>> GetAllGamesAsync()
    {
        logger.LogInformation("Getting all games");
        var games = await unitOfWork.GameRepository.GetAllAsync();
        List<GameModel> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(automapper.Map<GameModel>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task<IEnumerable<GenreModelDto>> GetGenresByGameIdAsync(Guid gameId)
    {
        logger.LogInformation("Getting genres by game Id: {gameId}", gameId);
        var genres = await unitOfWork.GameRepository.GetGenresByGameAsync(gameId);
        List<GenreModelDto> genreModels = [];

        foreach (var genre in genres)
        {
            genreModels.Add(automapper.Map<GenreModelDto>(genre));
        }

        return genreModels.AsEnumerable();
    }

    public async Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameIdAsync(Guid gameId)
    {
        logger.LogInformation("Getting platforms by game Id: {gameId}", gameId);
        var platforms = await unitOfWork.GameRepository.GetPlatformsByGameAsync(gameId);
        List<PlatformModelDto> platformModels = [];

        foreach (var platform in platforms)
        {
            platformModels.Add(automapper.Map<PlatformModelDto>(platform));
        }

        return platformModels.AsEnumerable();
    }

    public async Task<GameModel> GetGameByIdAsync(Guid gameId)
    {
        logger.LogInformation("Getting game by Id: {gameId}", gameId);
        var game = await unitOfWork.GameRepository.GetByIdAsync(gameId);

        return game == null ? throw new GamestoreException($"No game found with given id: {gameId}") : automapper.Map<GameModel>(game);
    }

    public async Task<GameModel> GetGameByKeyAsync(string key)
    {
        logger.LogInformation("Getting game by Key: {key}", key);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(key);

        return game == null ? throw new GamestoreException($"No game found with given key: {key}") : automapper.Map<GameModel>(game);
    }

    public async Task AddGameAsync(GameModelDto gameModel)
    {
        logger.LogInformation("Adding game {@gameModel}", gameModel);
        var result = await _gameModelDtoValidator.ValidateAsync(gameModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var game = automapper.Map<Game>(gameModel);

        await unitOfWork.GameRepository.AddAsync(game);
        await unitOfWork.SaveAsync();
    }

    public async Task UpdateGameAsync(GameModelDto gameModel)
    {
        logger.LogInformation("Updating game {@gameModel}", gameModel);
        var result = await _gameModelDtoUpdateValidator.ValidateAsync(gameModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var gameGenres = await unitOfWork.GameGenreRepository.GetByGameIdAsync(gameModel.Id);
        foreach (var item in gameGenres)
        {
            unitOfWork.GameGenreRepository.Delete(item);
        }

        var gamePlatforms = await unitOfWork.GamePlatformRepository.GetByGameIdAsync(gameModel.Id);
        foreach (var item in gamePlatforms)
        {
            unitOfWork.GamePlatformRepository.Delete(item);
        }

        await unitOfWork.SaveAsync();

        var game = automapper.Map<Game>(gameModel);

        foreach (var item in game.GameGenres)
        {
            item.GameId = gameModel.Id;
            await unitOfWork.GameGenreRepository.AddAsync(item);
        }

        foreach (var item in game.GamePlatforms)
        {
            item.GameId = gameModel.Id;
            await unitOfWork.GamePlatformRepository.AddAsync(item);
        }

        await unitOfWork.GameRepository.UpdateAsync(game);

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteGameByIdAsync(Guid gameId)
    {
        logger.LogInformation("Deleting game by Id: {gameId}", gameId);
        var game = await unitOfWork.GameRepository.GetByIdAsync(gameId);

        if (game != null)
        {
            unitOfWork.GameRepository.Delete(game);

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
        }
        else
        {
            throw new GamestoreException($"No game found with given id: {gameId}");
        }
    }
}
