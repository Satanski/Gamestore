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
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _automapper = automapper;
    private readonly ILogger<GameService> _logger = logger;
    private readonly GameModelDtoValidator _gameModelDtoValidator = new(unitOfWork);
    private readonly GameModelDtoUpdateValidator _gameModelDtoUpdateValidator = new(unitOfWork);

    public async Task<IEnumerable<GameModel>> GetAllGamesAsync()
    {
        _logger.LogInformation("Getting all games");
        var games = await _unitOfWork.GameRepository.GetAllAsync();
        List<GameModel> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(_automapper.Map<GameModel>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task<IEnumerable<GenreModelDto>> GetGenresByGameAsync(Guid gameId)
    {
        _logger.LogInformation($"Getting genres by game Id: {gameId}");
        var genres = await _unitOfWork.GameRepository.GetGenresByGameAsync(gameId);
        List<GenreModelDto> genreModels = [];

        foreach (var genre in genres)
        {
            genreModels.Add(_automapper.Map<GenreModelDto>(genre));
        }

        return genreModels.AsEnumerable();
    }

    public async Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameAsync(Guid gameId)
    {
        _logger.LogInformation($"Getting platforms by game Id: {gameId}");
        var platforms = await _unitOfWork.GameRepository.GetPlatformsByGameAsync(gameId);
        List<PlatformModelDto> platformModels = [];

        foreach (var platform in platforms)
        {
            platformModels.Add(_automapper.Map<PlatformModelDto>(platform));
        }

        return platformModels.AsEnumerable();
    }

    public async Task<GameModel> GetGameByIdAsync(Guid gameId)
    {
        _logger.LogInformation($"Getting game by Id: {gameId}");
        var game = await _unitOfWork.GameRepository.GetByIdAsync(gameId);

        return game == null ? throw new GamestoreException($"No game found with given id: {gameId}") : _automapper.Map<GameModel>(game);
    }

    public async Task<GameModel> GetGameByKeyAsync(string key)
    {
        _logger.LogInformation($"Getting game by Key: {key}");
        var game = await _unitOfWork.GameRepository.GetGameByKeyAsync(key);

        return game == null ? throw new GamestoreException($"No game found with given key: {key}") : _automapper.Map<GameModel>(game);
    }

    public async Task AddGameAsync(GameModelDto gameModel)
    {
        _logger.LogInformation($"Adding game Id: {gameModel.Id} Name: {gameModel.Name}");
        var result = await _gameModelDtoValidator.ValidateAsync(gameModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var game = _automapper.Map<Game>(gameModel);

        await _unitOfWork.GameRepository.AddAsync(game);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateGameAsync(GameModelDto gameModel)
    {
        _logger.LogInformation($"Updating game Id: {gameModel.Id} Name: {gameModel.Name}");
        var result = await _gameModelDtoUpdateValidator.ValidateAsync(gameModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var gameGenres = await _unitOfWork.GameGenreRepository.GetByGameIdAsync(gameModel.Id);
        foreach (var item in gameGenres)
        {
            _unitOfWork.GameGenreRepository.Delete(item);
        }

        var gamePlatforms = await _unitOfWork.GamePlatformRepository.GetByGameIdAsync(gameModel.Id);
        foreach (var item in gamePlatforms)
        {
            _unitOfWork.GamePlatformRepository.Delete(item);
        }

        await _unitOfWork.SaveAsync();

        var game = _automapper.Map<Game>(gameModel);

        foreach (var item in game.GameGenres)
        {
            item.GameId = gameModel.Id;
            await _unitOfWork.GameGenreRepository.AddAsync(item);
        }

        foreach (var item in game.GamePlatforms)
        {
            item.GameId = gameModel.Id;
            await _unitOfWork.GamePlatformRepository.AddAsync(item);
        }

        await _unitOfWork.GameRepository.UpdateAsync(game);

        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteGameAsync(Guid gameId)
    {
        _logger.LogInformation($"Deleting game Id: {gameId}");
        var game = await _unitOfWork.GameRepository.GetByIdAsync(gameId);

        if (game != null)
        {
            _unitOfWork.GameRepository.Delete(game);

            var genres = await _unitOfWork.GameGenreRepository.GetByGameIdAsync(game.Id);
            if (genres != null)
            {
                foreach (var item in genres)
                {
                    _unitOfWork.GameGenreRepository.Delete(item);
                }
            }

            var platforms = await _unitOfWork.GamePlatformRepository.GetByGameIdAsync(game.Id);
            if (platforms != null)
            {
                foreach (var item in platforms)
                {
                    _unitOfWork.GamePlatformRepository.Delete(item);
                }
            }

            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new GamestoreException($"No game found with given id: {gameId}");
        }
    }
}
