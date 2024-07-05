using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering;
using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Models;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Enums;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.WebApi.Stubs;
using Microsoft.Extensions.Logging;

namespace Gamestore.Services.Services;

public class GameService(IUnitOfWork unitOfWork, IMapper automapper, ILogger<GameService> logger, IGameProcessingPipelineDirector gameProcessingPipelineDirector) : IGameService
{
    private const string QuoteActionName = "Quote";
    private const string DeletedMessageTemplate = "A comment/quote was deleted";
    private readonly GameDtoWrapperValidator _gameDtoWrapperValidator = new();
    private readonly CommentModelDtoValidator _commentModelDtoValidator = new();

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

    public async Task<FilteredGamesDto> GetFilteredGamesAsync(GameFiltersDto gameFilters)
    {
        logger.LogInformation("Getting games by filter");

        var gameProcessingPipelineService = gameProcessingPipelineDirector.ConstructGameCollectionPipelineService();
        var gamesQueryable = unitOfWork.GameRepository.GetGamesAsQueryable();
        var filteredGames = await gameProcessingPipelineService.ProcessGamesAsync(unitOfWork, gameFilters, gamesQueryable);

        FilteredGamesDto filteredGameDtos = new();
        foreach (var game in filteredGames)
        {
            filteredGameDtos.Games.Add(automapper.Map<GameModelDto>(game));
        }

        filteredGameDtos.TotalPages = gameFilters.NumberOfPagesAfterFiltration;
        filteredGameDtos.CurrentPage = gameFilters.Page;

        return filteredGameDtos;
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
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(key) ?? throw new GamestoreException($"No game found with given key: {key}");
        await IncreaseGameViewCounter(unitOfWork, game);

        return automapper.Map<GameModelDto>(game);
    }

    public List<string> GetPaginationOptions()
    {
        return PaginationOptionsDto.PaginationOptions;
    }

    public List<string> GetPublishDateOptions()
    {
        return PublishDateOptionsDto.PublishDateOptions;
    }

    public List<string> GetSortingOptions()
    {
        return SortingOptionsDto.SortingOptions;
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
            await DeleteOrderGamesFromRepository(unitOfWork, game);
            await DeleteGameGenresFromRepository(unitOfWork, game.Id);
            await DeleteGamePlatformsFromRepository(unitOfWork, game.Id);
            await DeleteGameFromRepository(unitOfWork, game);
        }
        else
        {
            throw new GamestoreException($"No game found with given id: {gameId}");
        }
    }

    public async Task SoftDeleteGameByIdAsync(Guid gameId)
    {
        logger.LogInformation("Deleting game by Id: {gameId}", gameId);
        var game = await unitOfWork.GameRepository.GetByIdAsync(gameId);

        if (game != null)
        {
            await SoftDeleteGameFromRepository(unitOfWork, game);
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
            await DeleteOrderGamesFromRepository(unitOfWork, game);
            await DeleteGameGenresFromRepository(unitOfWork, game.Id);
            await DeleteGamePlatformsFromRepository(unitOfWork, game.Id);
            await DeleteGameFromRepository(unitOfWork, game);
        }
        else
        {
            throw new GamestoreException($"No game found with given key: {gameKey}");
        }
    }

    public async Task SoftDeleteGameByKeyAsync(string gameKey)
    {
        logger.LogInformation("Deleting game by Key: {gameKey}", gameKey);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);

        if (game != null)
        {
            await SoftDeleteGameFromRepository(unitOfWork, game);
        }
        else
        {
            throw new GamestoreException($"No game found with given key: {gameKey}");
        }
    }

    public async Task AddGameToCartAsync(Guid customerId, string gameKey, int quantity)
    {
        logger.LogInformation("Adding game to cart: {@gameKey}", gameKey);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey) ?? throw new GamestoreException($"No game found with given key: {gameKey}");
        var unitInStock = game.UnitInStock;

        var exisitngOrder = await unitOfWork.OrderRepository.GetByCustomerIdAsync(customerId);
        if (exisitngOrder == null)
        {
            await CreateNewOrder(unitOfWork, customerId, quantity, game, unitInStock);
        }
        else
        {
            await UpdateExistingOrder(unitOfWork, quantity, game, unitInStock, exisitngOrder);
        }
    }

    public async Task<IEnumerable<CommentModel>> GetCommentsByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting comments by game key: {@gameKey}", gameKey);
        var comments = await unitOfWork.CommentRepository.GetByGameKeyAsync(gameKey);

        var commentHelpers = new CommentHelpers(automapper);
        List<CommentModel> commentList = commentHelpers.CommentListCreator(comments);

        return commentList.AsEnumerable();
    }

    public async Task<string> AddCommentToGameAsync(string gameKey, CommentModelDto comment)
    {
        logger.LogInformation("Adding comment: {@comment} to game {@gameKey}", comment, gameKey);
        await _commentModelDtoValidator.ValidateComment(comment);
        if (CheckIfUserIsBanned(comment))
        {
            return $"User banned till {CustomerStub.BannedTill}";
        }
        else
        {
            var gameId = (await unitOfWork.GameRepository.GetGameByKeyAsync(@gameKey)).Id;
            var commenttoAdd = ConvertCommentModelDtoToComment(comment, gameId);

            if (comment.Action == QuoteActionName && comment.ParentId != null)
            {
                ComposeQuotedMessage(commenttoAdd);
            }

            await AddMessageToRepository(unitOfWork, commenttoAdd);
        }

        return string.Empty;
    }

    public async Task DeleteCommentAsync(string gameKey, Guid commentId)
    {
        logger.LogInformation("Deleting comment: {@commentId}", commentId);

        var comment = await unitOfWork.CommentRepository.GetByIdAsync(commentId);

        if (comment != null)
        {
            comment.Body = DeletedMessageTemplate;
            await unitOfWork.CommentRepository.UpdateAsync(comment);
            await unitOfWork.SaveAsync();
        }
    }

    private static async Task UpdateExistingOrder(IUnitOfWork unitOfWork, int quantity, Game game, int unitInStock, Order? exisitngOrder)
    {
        OrderGame existingOrderGame = await unitOfWork.OrderGameRepository.GetByOrderIdAndProductIdAsync(exisitngOrder.Id, game.Id);

        if (existingOrderGame != null)
        {
            await UpdateExistingOrderGame(unitOfWork, quantity, unitInStock, existingOrderGame);
        }
        else
        {
            await CreateNewOrderGame(unitOfWork, quantity, game, unitInStock, exisitngOrder);
        }
    }

    private static async Task CreateNewOrderGame(IUnitOfWork unitOfWork, int quantity, Game game, int unitInStock, Order? exisitngOrder)
    {
        var expectedTotalQuantity = quantity < unitInStock ? quantity : unitInStock;

        OrderGame newOrderGame = new OrderGame()
        {
            OrderId = exisitngOrder.Id,
            ProductId = game.Id,
            Price = game.Price,
            Discount = game.Discount,
            Quantity = expectedTotalQuantity,
        };

        await unitOfWork.OrderGameRepository.AddAsync(newOrderGame);
        await unitOfWork.SaveAsync();
    }

    private static async Task UpdateExistingOrderGame(IUnitOfWork unitOfWork, int quantity, int unitInStock, OrderGame existingOrderGame)
    {
        var expectedTotalQuantity = quantity + existingOrderGame.Quantity;
        expectedTotalQuantity = expectedTotalQuantity < unitInStock ? expectedTotalQuantity : unitInStock;
        existingOrderGame.Quantity = expectedTotalQuantity;
        await unitOfWork.OrderGameRepository.UpdateAsync(existingOrderGame);
        await unitOfWork.SaveAsync();
    }

    private static async Task<int> CreateNewOrder(IUnitOfWork unitOfWork, Guid customerId, int quantity, Game game, int unitInStock)
    {
        if (quantity > unitInStock)
        {
            quantity = unitInStock;
        }

        var newOrderId = Guid.NewGuid();
        List<OrderGame> orderGames = [new() { OrderId = newOrderId, ProductId = game.Id, Price = game.Price, Discount = game.Discount, Quantity = quantity }];
        Order order = new() { Id = newOrderId, CustomerId = customerId, Date = DateTime.Now, OrderGames = orderGames, Status = OrderStatus.Open };
        await unitOfWork.OrderRepository.AddAsync(order);
        await unitOfWork.OrderGameRepository.AddAsync(orderGames[0]);
        await unitOfWork.SaveAsync();
        return quantity;
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

    private static async Task DeleteOrderGamesFromRepository(IUnitOfWork unitOfWork, Game? game)
    {
        var orderGames = await unitOfWork.OrderGameRepository.GetAllAsync();
        var orderGamesToRemove = orderGames.Where(x => x.ProductId == game.Id);
        foreach (var og in orderGamesToRemove)
        {
            unitOfWork.OrderGameRepository.Delete(og);
        }
    }

    private static async Task SoftDeleteGameFromRepository(IUnitOfWork unitOfWork, Game game)
    {
        await unitOfWork.GameRepository.SoftDelete(game);
        await unitOfWork.SaveAsync();
    }

    private static async Task DeleteGameFromRepository(IUnitOfWork unitOfWork, Game game)
    {
        unitOfWork.GameRepository.Delete(game);
        await unitOfWork.SaveAsync();
    }

    private static Comment ConvertCommentModelDtoToComment(CommentModelDto comment, Guid gameId)
    {
        return new Comment()
        {
            GameId = gameId,
            ParentCommentId = comment.ParentId,
            Body = comment.Comment.Body,
            Name = comment.Comment.Name,
        };
    }

    private static void ComposeQuotedMessage(Comment commenttoAdd)
    {
        commenttoAdd.Body = commenttoAdd.Body.Insert(0, "[$Quote$]");
    }

    private static async Task AddMessageToRepository(IUnitOfWork unitOfWork, Comment commenttoAdd)
    {
        await unitOfWork.CommentRepository.AddAsync(commenttoAdd);
        await unitOfWork.SaveAsync();
    }

    private static bool CheckIfUserIsBanned(CommentModelDto comment)
    {
        if (comment.Comment.Name == CustomerStub.Name && CustomerStub.BannedTill > DateTime.Now)
        {
            return true;
        }

        return false;
    }

    private static async Task IncreaseGameViewCounter(IUnitOfWork unitOfWork, Game? game)
    {
        game.NumberOfViews++;
        await unitOfWork.SaveAsync();
    }
}
