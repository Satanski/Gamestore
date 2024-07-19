using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering;
using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Models;
using Gamestore.BLL.MongoLogging;
using Gamestore.BLL.Services;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Enums;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Helpers;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.WebApi.Stubs;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Gamestore.Services.Services;

public class GameService(
    IUnitOfWork unitOfWork,
    IMongoUnitOfWork mongoUnitOfWork,
    IMapper automapper,
    ILogger<GameService> logger,
    IMongoLoggingService mongoLoggingService,
    IGameProcessingPipelineDirector gameProcessingPipelineDirector) : IGameService
{
    private const string QuoteActionName = "Quote";
    private const string DeletedMessageTemplate = "A comment/quote was deleted";
    private readonly GameDtoWrapperValidator _gameDtoWrapperValidator = new();
    private readonly CommentModelDtoValidator _commentModelDtoValidator = new();

    public async Task<IEnumerable<GameModelDto>> GetAllGamesAsync()
    {
        logger.LogInformation("Getting all games");
        var games = await unitOfWork.GameRepository.GetAllAsync();
        var gameModels = automapper.Map<List<GameModelDto>>(games);

        return gameModels.AsEnumerable();
    }

    public async Task<FilteredGamesDto> GetFilteredGamesAsync(GameFiltersDto gameFilters)
    {
        logger.LogInformation("Getting games by filter");

        var gameProcessingPipelineService = gameProcessingPipelineDirector.ConstructGameCollectionPipelineService();

        FilteredGamesDto filteredGameDtos = new();
        await SqlServerHelperService.FilterGamesFromSQLServerAsync(unitOfWork, mongoUnitOfWork, automapper, gameFilters, filteredGameDtos, gameProcessingPipelineService);
        await MongoDbHelperService.FilterProductsFromMongoDBAsync(unitOfWork, mongoUnitOfWork, automapper, gameFilters, filteredGameDtos, gameProcessingPipelineService);
        MongoDbHelperService.SetTotalNumberOfPagesAfterFiltering(gameFilters, filteredGameDtos);
        MongoDbHelperService.CheckIfCurrentPageDoesntExceedTotalNumberOfPages(gameFilters, filteredGameDtos);

        return filteredGameDtos;
    }

    public async Task<IEnumerable<GenreModelDto>> GetGenresByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting genres by game Id: {gameKey}", gameKey);

        var genreModels = await SqlServerHelperService.GetGenresFromSQLServerByGameKeyAsync(unitOfWork, automapper, gameKey);
        genreModels ??= await MongoDbHelperService.GetGenresFromMongoDBByGameKeyAsync(mongoUnitOfWork, automapper, gameKey);

        return genreModels.AsEnumerable();
    }

    public async Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting platforms by game Key: {gameKey}", gameKey);
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
        if (game is not null)
        {
            var platforms = await unitOfWork.GameRepository.GetPlatformsByGameAsync(game.Id);

            if (platforms.IsNullOrEmpty())
            {
                return [];
            }

            var platformModels = automapper.Map<List<PlatformModelDto>>(platforms);

            return platformModels.AsEnumerable();
        }

        return [];
    }

    public async Task<PublisherModelDto> GetPublisherByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting publisher by game Key: {gameKey}", gameKey);

        var publisher = await SqlServerHelperService.GetPublisherFromSQLServerByGameKeyAsync(unitOfWork, automapper, gameKey);
        publisher ??= await MongoDbHelperService.GetPublisherFromMongoDBByGameKeyAsync(mongoUnitOfWork, automapper, gameKey);

        return publisher;
    }

    public async Task<GameModelDto> GetGameByIdAsync(Guid gameId)
    {
        logger.LogInformation("Getting game by Id: {gameId}", gameId);
        var game = await SqlServerHelperService.GetGameFromSQLServerByIdAsync(unitOfWork, gameId);

        game ??= await MongoDbHelperService.GetGameFromMongoDBByIdAsync(mongoUnitOfWork, automapper, gameId);

        return game == null ? throw new GamestoreException($"No game found with given id: {gameId}") : automapper.Map<GameModelDto>(game);
    }

    public async Task<GameModelDto> GetGameByKeyAsync(string key)
    {
        logger.LogInformation("Getting game by Key: {key}", key);

        var game = await SqlServerHelperService.GetGameFromSQLServerByKeyAsync(unitOfWork, automapper, key);
        game ??= await MongoDbHelperService.GetGameWithDetailsFromMongoDBByKeyAsync(mongoUnitOfWork, automapper, key);

        return game ?? throw new GamestoreException($"No game found with given key: {key}");
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
        var addedGame = await AddGameToRepositoryAsync(unitOfWork, gameModel, game);

        var genres = gameModel.Genres;
        var platforms = gameModel.Platforms;
        await AddGameGenresTorepositoryAsync(unitOfWork, addedGame, genres);
        await AddGamePlatformsToRepositoryAsync(unitOfWork, addedGame, platforms);

        await mongoLoggingService.LogGameAddAsync(gameModel);
    }

    public async Task UpdateGameAsync(GameDtoWrapper gameModel)
    {
        logger.LogInformation("Updating game {@gameModel}", gameModel);
        await _gameDtoWrapperValidator.ValidateGame(gameModel);

        GameModelDto oldObjectState;
        GameDtoWrapper newObjectState = gameModel;

        var existingGameInSQLServer = await unitOfWork.GameRepository.GetByIdAsync((Guid)gameModel.Game.Id!);
        if (existingGameInSQLServer != null)
        {
            oldObjectState = automapper.Map<GameModelDto>(existingGameInSQLServer);
        }
        else
        {
            var id = GuidHelpers.GuidToInt((Guid)gameModel.Game.Id);
            var gameFromMongoDB = await MongoDbHelperService.GetGameWithDetailsFromMongoDBByIdAsync(mongoUnitOfWork, automapper, id);
            await SqlServerHelperService.CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync(unitOfWork, automapper, gameFromMongoDB, existingGameInSQLServer);
            oldObjectState = gameFromMongoDB;
        }

        await DeleteGameGenresFromRepositoryAsync(unitOfWork, gameModel.Game.Id);
        await DeleteGamePlatformsFromRepositoryAsync(unitOfWork, gameModel.Game.Id);

        var game = automapper.Map<Game>(gameModel.Game);
        await UpdateGameInrepositoryAsync(unitOfWork, gameModel, game);

        await mongoLoggingService.LogGameUpdateAsync(oldObjectState, newObjectState);
    }

    public async Task DeleteGameByIdAsync(Guid gameId)
    {
        logger.LogInformation("Deleting game by Id: {gameId}", gameId);
        var game = await unitOfWork.GameRepository.GetByIdAsync(gameId);

        if (game != null)
        {
            await DeleteOrderGamesFromRepositoryAsync(unitOfWork, game);
            await DeleteGameGenresFromRepositoryAsync(unitOfWork, game.Id);
            await DeleteGamePlatformsFromRepositoryAsync(unitOfWork, game.Id);
            await DeleteGameFromRepositoryAsync(unitOfWork, game);
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
            await SoftDeleteGameFromRepositoryAsync(unitOfWork, game);
            await mongoLoggingService.LogGameDeleteAsync(gameId);
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
            await DeleteOrderGamesFromRepositoryAsync(unitOfWork, game);
            await DeleteGameGenresFromRepositoryAsync(unitOfWork, game.Id);
            await DeleteGamePlatformsFromRepositoryAsync(unitOfWork, game.Id);
            await DeleteGameFromRepositoryAsync(unitOfWork, game);
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
            await SoftDeleteGameFromRepositoryAsync(unitOfWork, game);
        }
        else
        {
            throw new GamestoreException($"No game found with given key: {gameKey}");
        }
    }

    public async Task AddGameToCartAsync(Guid customerId, string gameKey, int quantity)
    {
        logger.LogInformation("Adding game to cart: {@gameKey}", gameKey);

        var game = await SqlServerHelperService.GetGameFromSQLServerByKeyAsync(unitOfWork, automapper, gameKey);
        game ??= await MongoDbHelperService.GetGameWithDetailsFromMongoDBByKeyAsync(mongoUnitOfWork, automapper, gameKey) ?? throw new GamestoreException($"No game found with given key: {gameKey}");
        var unitInStock = game.UnitInStock;

        if (unitInStock > 0)
        {
            var exisitngOrder = await unitOfWork.OrderRepository.GetByCustomerIdAsync(customerId);
            if (exisitngOrder == null || exisitngOrder.Status != OrderStatus.Open)
            {
                await CreateNewOrderAsync(unitOfWork, automapper, customerId, quantity, game, unitInStock);
            }
            else
            {
                await SqlServerHelperService.UpdateExistingOrderAsync(unitOfWork, automapper, quantity, game, unitInStock, exisitngOrder);
            }
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
            var game = await unitOfWork.GameRepository.GetGameByKeyAsync(@gameKey);

            if (game == null)
            {
                GameModelDto gameModelDto = automapper.Map<GameModelDto>(await mongoUnitOfWork.ProductRepository.GetByNameAsync(gameKey));
                await SqlServerHelperService.CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync(unitOfWork, automapper, gameModelDto, game);
            }

            game = await unitOfWork.GameRepository.GetGameByKeyAsync(@gameKey);
            var commenttoAdd = ConvertCommentModelDtoToComment(comment, game.Id);

            if (comment.Action == QuoteActionName && comment.ParentId != null)
            {
                ComposeQuotedMessage(commenttoAdd);
            }

            await AddMessageToRepositoryAsync(unitOfWork, commenttoAdd);
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

    private static async Task<int> CreateNewOrderAsync(IUnitOfWork unitOfWork, IMapper automapper, Guid customerId, int quantity, GameModelDto game, int unitInStock)
    {
        if (quantity > unitInStock)
        {
            quantity = unitInStock;
        }

        if (game.Id is not null)
        {
            var gameInSQLServer = await unitOfWork.GameRepository.GetByIdAsync((Guid)game.Id);
            if (gameInSQLServer is null)
            {
                await SqlServerHelperService.CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync(unitOfWork, automapper, game, gameInSQLServer);
            }

            var newOrderId = Guid.NewGuid();
            List<OrderGame> orderGames = [new() { OrderId = newOrderId, GameId = (Guid)game.Id, Price = game.Price, Discount = game.Discontinued, Quantity = quantity }];
            Order order = new() { Id = newOrderId, CustomerId = customerId, OrderDate = DateTime.Now, OrderGames = orderGames, Status = OrderStatus.Open };
            await unitOfWork.OrderRepository.AddAsync(order);
            await unitOfWork.OrderGameRepository.AddAsync(orderGames[0]);
            await unitOfWork.SaveAsync();
        }

        return quantity;
    }

    private static async Task<Game> AddGameToRepositoryAsync(IUnitOfWork unitOfWork, GameDtoWrapper gameModel, Game game)
    {
        game.PublisherId = gameModel.Publisher;

        var addedGame = await unitOfWork.GameRepository.AddAsync(game);
        await unitOfWork.SaveAsync();
        return addedGame;
    }

    private static async Task AddGamePlatformsToRepositoryAsync(IUnitOfWork unitOfWork, Game addedGame, List<Guid> platforms)
    {
        foreach (var platformId in platforms)
        {
            await unitOfWork.GamePlatformRepository.AddAsync(new GamePlatform() { GameId = addedGame.Id, PlatformId = platformId });
        }

        await unitOfWork.SaveAsync();
    }

    private static async Task AddGameGenresTorepositoryAsync(IUnitOfWork unitOfWork, Game addedGame, List<Guid> genres)
    {
        foreach (var genreId in genres)
        {
            await unitOfWork.GameGenreRepository.AddAsync(new GameGenres() { GameId = addedGame.Id, GenreId = genreId });
        }

        await unitOfWork.SaveAsync();
    }

    private static async Task UpdateGameInrepositoryAsync(IUnitOfWork unitOfWork, GameDtoWrapper gameModel, Game game)
    {
        game.PublisherId = gameModel.Publisher;

        game.ProductCategories = [];
        game.ProductPlatforms = [];

        foreach (var genre in gameModel.Genres)
        {
            game.ProductCategories.Add(new() { GameId = (Guid)gameModel.Game.Id!, GenreId = genre });
        }

        foreach (var platform in gameModel.Platforms)
        {
            game.ProductPlatforms.Add(new() { GameId = (Guid)gameModel.Game.Id!, PlatformId = platform });
        }

        await unitOfWork.GameRepository.UpdateAsync(game);
        await unitOfWork.SaveAsync();
    }

    private static async Task DeleteGamePlatformsFromRepositoryAsync(IUnitOfWork unitOfWork, Guid? gameId)
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

    private static async Task DeleteGameGenresFromRepositoryAsync(IUnitOfWork unitOfWork, Guid? gameId)
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

    private static async Task DeleteOrderGamesFromRepositoryAsync(IUnitOfWork unitOfWork, Game? game)
    {
        var orderGames = await unitOfWork.OrderGameRepository.GetAllAsync();
        var orderGamesToRemove = orderGames.Where(x => x.GameId == game.Id);
        foreach (var og in orderGamesToRemove)
        {
            unitOfWork.OrderGameRepository.Delete(og);
        }
    }

    private static async Task SoftDeleteGameFromRepositoryAsync(IUnitOfWork unitOfWork, Game game)
    {
        await unitOfWork.GameRepository.SoftDelete(game);
        await unitOfWork.SaveAsync();
    }

    private static async Task DeleteGameFromRepositoryAsync(IUnitOfWork unitOfWork, Game game)
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

    private static async Task AddMessageToRepositoryAsync(IUnitOfWork unitOfWork, Comment commenttoAdd)
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
}
