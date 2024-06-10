using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Models;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Enums;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.Extensions.Logging;

namespace Gamestore.Services.Services;

public class GameService(IUnitOfWork unitOfWork, IMapper automapper, ILogger<GameService> logger) : IGameService
{
    private readonly GameModelUpdateValidator _gameModelUpdateValidator = new(unitOfWork);
    private readonly GameModelValidator _gameModelValidator = new();

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

    public async Task<PublisherModelDto> GetPublisherByGameIdAsync(Guid gameId)
    {
        logger.LogInformation("Getting publisher by game Id: {gameId}", gameId);

        var publisher = await unitOfWork.GameRepository.GetPublisherByGameAsync(gameId);

        return automapper.Map<PublisherModelDto>(publisher);
    }

    public async Task<GameModelDto> GetGameByIdAsync(Guid gameId)
    {
        logger.LogInformation("Getting game by Id: {gameId}", gameId);
        var game = await unitOfWork.GameRepository.GetByIdAsync(gameId);

        return game == null ? throw new GamestoreException($"No game found with given id: {gameId}") : automapper.Map<GameModelDto>(game);
    }

    public async Task<GameModelDto> GetGameByKeyAsync(string key)
    {
        logger.LogInformation("Getting game by Key: {key}", key);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(key);

        return game == null ? throw new GamestoreException($"No game found with given key: {key}") : automapper.Map<GameModelDto>(game);
    }

    public async Task AddGameAsync(GameModel gameModel)
    {
        logger.LogInformation("Adding game {@gameModel}", gameModel);

        var result = await _gameModelValidator.ValidateAsync(gameModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var game = automapper.Map<Game>(gameModel);

        var addedGame = await unitOfWork.GameRepository.AddAsync(game);
        await unitOfWork.SaveAsync();

        foreach (var genreId in gameModel.Genres)
        {
            await unitOfWork.GameGenreRepository.AddAsync(new GameGenre() { GameId = addedGame.Id, GenreId = genreId });
        }

        foreach (var platformId in gameModel.Platforms)
        {
            await unitOfWork.GamePlatformRepository.AddAsync(new GamePlatform() { GameId = addedGame.Id, PlatformId = platformId });
        }

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateGameAsync(GameUpdateModel gameModel)
    {
        logger.LogInformation("Updating game {@gameModel}", gameModel);
        var result = await _gameModelUpdateValidator.ValidateAsync(gameModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        await DeleteGameGenresFromRepository(unitOfWork, gameModel);
        await DeleteGamePlatformsFromRepository(unitOfWork, gameModel);

        await unitOfWork.SaveAsync();

        foreach (var genreId in gameModel.Genres)
        {
            await unitOfWork.GameGenreRepository.AddAsync(new GameGenre() { GameId = gameModel.Id, GenreId = genreId });
        }

        foreach (var platformId in gameModel.Platforms)
        {
            await unitOfWork.GamePlatformRepository.AddAsync(new GamePlatform() { GameId = gameModel.Id, PlatformId = platformId });
        }

        var game = automapper.Map<Game>(gameModel);
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

    public async Task BuyGameAsync(Guid customerId, Guid gameId, int quantity)
    {
        var exisitngOrder = await unitOfWork.OrderRepository.GetByCustomerId(customerId);

        if (exisitngOrder == null)
        {
            Order order = new() { Id = Guid.NewGuid(), CustomerId = customerId, Date = DateTime.Now, Status = OrderStatus.Open };
            OrderGame orderGame = new() { OrderId = order.Id, ProductId = gameId, Price = 10, Discount = 10, Quantity = quantity };
            await unitOfWork.OrderRepository.AddAsync(order);
            await unitOfWork.OrderGameRepository.AddAsync(orderGame);
            await unitOfWork.SaveAsync();
        }
    }

    private static async Task DeleteGamePlatformsFromRepository(IUnitOfWork unitOfWork, GameUpdateModel gameModel)
    {
        var gamePlatforms = await unitOfWork.GamePlatformRepository.GetByGameIdAsync(gameModel.Id);
        foreach (var item in gamePlatforms)
        {
            unitOfWork.GamePlatformRepository.Delete(item);
        }
    }

    private static async Task DeleteGameGenresFromRepository(IUnitOfWork unitOfWork, GameUpdateModel gameModel)
    {
        var gameGenres = await unitOfWork.GameGenreRepository.GetByGameIdAsync(gameModel.Id);
        foreach (var item in gameGenres)
        {
            unitOfWork.GameGenreRepository.Delete(item);
        }
    }
}
