using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Helpers;
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
    private readonly GameDtoWrapperValidator _gameDtoWrapperValidator = new();

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

    public async Task<IEnumerable<GenreModelDto>> GetGenresByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting genres by game Id: {gameKey}", gameKey);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
        var genres = await unitOfWork.GameRepository.GetGenresByGameAsync(game.Id);
        List<GenreModelDto> genreModels = [];

        foreach (var genre in genres)
        {
            genreModels.Add(automapper.Map<GenreModelDto>(genre));
        }

        return genreModels.AsEnumerable();
    }

    public async Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting platforms by game Key: {gameKey}", gameKey);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
        var platforms = await unitOfWork.GameRepository.GetPlatformsByGameAsync(game.Id);
        List<PlatformModelDto> platformModels = [];

        foreach (var platform in platforms)
        {
            platformModels.Add(automapper.Map<PlatformModelDto>(platform));
        }

        return platformModels.AsEnumerable();
    }

    public async Task<PublisherModelDto> GetPublisherByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting publisher by game Key: {gameKey}", gameKey);

        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
        var publisher = await unitOfWork.GameRepository.GetPublisherByGameAsync(game.Id);

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

    public async Task AddGameAsync(GameDtoWrapper gameModel)
    {
        logger.LogInformation("Adding game {@gameModel}", gameModel);

        await _gameDtoWrapperValidator.ValidateGame(gameModel);

        var game = automapper.Map<Game>(gameModel.Game);
        var addedGame = await AddGameToRepository(unitOfWork, gameModel, game);

        var genres = gameModel.Genres;
        var platforms = gameModel.Platforms;
        await AddGameGenresTorepository(unitOfWork, addedGame, genres);
        await AddGamePlatformsToRepository(unitOfWork, addedGame, platforms);
    }

    public async Task UpdateGameAsync(GameDtoWrapper gameModel)
    {
        logger.LogInformation("Updating game {@gameModel}", gameModel);

        await _gameDtoWrapperValidator.ValidateGame(gameModel);

        await DeleteGameGenresFromRepository(unitOfWork, gameModel.Game.Id);
        await DeleteGamePlatformsFromRepository(unitOfWork, gameModel.Game.Id);

        var game = automapper.Map<Game>(gameModel.Game);
        await UpdateGameInrepository(unitOfWork, gameModel, game);

        if (gameModel.Game.Id != null)
        {
            await AddGameGenresTorepository(unitOfWork, game, gameModel.Genres);
            await AddGamePlatformsToRepository(unitOfWork, game, gameModel.Platforms);
        }

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteGameByIdAsync(Guid gameId)
    {
        logger.LogInformation("Deleting game by Id: {gameId}", gameId);
        var game = await unitOfWork.GameRepository.GetByIdAsync(gameId);

        if (game != null)
        {
            await DeleteGameGenresFromRepository(unitOfWork, game.Id);
            await DeleteGamePlatformsFromRepository(unitOfWork, game.Id);
            await DeleteGameFromRepository(unitOfWork, game);
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
            await DeleteGameGenresFromRepository(unitOfWork, game.Id);
            await DeleteGamePlatformsFromRepository(unitOfWork, game.Id);
            await DeleteGameFromRepository(unitOfWork, game);
        }
        else
        {
            throw new GamestoreException($"No game found with given key: {gameKey}");
        }
    }

    private static async Task<Game> AddGameToRepository(IUnitOfWork unitOfWork, GameDtoWrapper gameModel, Game game)
    {
        game.PublisherId = gameModel.Publisher;

        var addedGame = await unitOfWork.GameRepository.AddAsync(game);
        await unitOfWork.SaveAsync();
        return addedGame;
    }

    private static async Task AddGamePlatformsToRepository(IUnitOfWork unitOfWork, Game addedGame, List<Guid> platforms)
    {
        foreach (var platformId in platforms)
        {
            await unitOfWork.GamePlatformRepository.AddAsync(new GamePlatform() { GameId = addedGame.Id, PlatformId = platformId });
        }

        await unitOfWork.SaveAsync();
    }

    private static async Task AddGameGenresTorepository(IUnitOfWork unitOfWork, Game addedGame, List<Guid> genres)
    {
        foreach (var genreId in genres)
        {
            await unitOfWork.GameGenreRepository.AddAsync(new GameGenre() { GameId = addedGame.Id, GenreId = genreId });
        }

        await unitOfWork.SaveAsync();
    }

    private static async Task UpdateGameInrepository(IUnitOfWork unitOfWork, GameDtoWrapper gameModel, Game game)
    {
        game.PublisherId = gameModel.Publisher;
        await unitOfWork.GameRepository.UpdateAsync(game);
        await unitOfWork.SaveAsync();
    }

    public async Task AddGameToCartAsync(Guid customerId, string gameKey, int quantity)
    {
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
        var unitInStock = game.UnitInStock;

        var exisitngOrder = await unitOfWork.OrderRepository.GetByCustomerIdAsync(customerId);
        if (exisitngOrder == null)
        {
            if (quantity > unitInStock)
            {
                quantity = unitInStock;
            }

            var newOrderId = Guid.NewGuid();
            List<OrderGame> orderGames = [new() { OrderId = newOrderId, ProductId = game.Id, Price = 10, Discount = 10, Quantity = quantity }];
            Order order = new() { Id = newOrderId, CustomerId = customerId, Date = DateTime.Now, OrderGames = orderGames, Status = OrderStatus.Open };
            await unitOfWork.OrderRepository.AddAsync(order);
            await unitOfWork.OrderGameRepository.AddAsync(orderGames[0]);
            await unitOfWork.SaveAsync();
        }
        else
        {
            OrderGame existingOrderGame = await unitOfWork.OrderGameRepository.GetByOrderIdAndProductIdAsync(exisitngOrder.Id, game.Id);
            var expectedTotalQuantity = quantity + existingOrderGame.Quantity;
            if (expectedTotalQuantity > unitInStock)
            {
                expectedTotalQuantity = unitInStock;
            }

            existingOrderGame.Quantity = expectedTotalQuantity;
            await unitOfWork.OrderGameRepository.UpdateAsync(existingOrderGame);
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
    private static async Task DeleteGamePlatformsFromRepository(IUnitOfWork unitOfWork, Guid? gameId)
    {
        if (gameId != null)
        {
            var gamePlatforms = await unitOfWork.GamePlatformRepository.GetByGameIdAsync((Guid)gameId);
            foreach (var item in gamePlatforms)
            {
                unitOfWork.GamePlatformRepository.Delete(item);
            }

            await unitOfWork.SaveAsync();
        }
    }

    private static async Task DeleteGameGenresFromRepository(IUnitOfWork unitOfWork, Guid? gameId)
    {
        if (gameId != null)
        {
            var gameGenres = await unitOfWork.GameGenreRepository.GetByGameIdAsync((Guid)gameId);
            foreach (var item in gameGenres)
            {
                unitOfWork.GameGenreRepository.Delete(item);
            }

            await unitOfWork.SaveAsync();
        }
    }

    private static async Task DeleteGameFromRepository(IUnitOfWork unitOfWork, Game game)
    {
        unitOfWork.GameRepository.Delete(game);
        await unitOfWork.SaveAsync();
    }
}
