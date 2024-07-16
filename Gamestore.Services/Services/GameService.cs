using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering;
using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Models;
using Gamestore.BLL.MongoLogging;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Enums;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.WebApi.Stubs;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Gamestore.Services.Services;

public class GameService(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, ILogger<GameService> logger, MongoLoggingService mongoLoggingService, IGameProcessingPipelineDirector gameProcessingPipelineDirector) : IGameService
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
        await FilterGamesFromSQLServer(unitOfWork, mongoUnitOfWork, automapper, gameFilters, filteredGameDtos, gameProcessingPipelineService);
        await FilterProductsFromMongoDB(unitOfWork, mongoUnitOfWork, automapper, gameFilters, filteredGameDtos, gameProcessingPipelineService);
        SetTotalNumberOfPagesAfterFiltering(gameFilters, filteredGameDtos);
        CheckIfCurrentPageDoesntExceedTotalNumberOfPages(gameFilters, filteredGameDtos);

        return filteredGameDtos;
    }

    public async Task<IEnumerable<GenreModelDto>> GetGenresByGameKeyAsync(string gameKey)
    {
        logger.LogInformation("Getting genres by game Id: {gameKey}", gameKey);

        var genreModels = await GetGenresFromSQLServerByGameKey(unitOfWork, automapper, gameKey);
        genreModels ??= await GetGenresFromMongoDBByGameKey(mongoUnitOfWork, automapper, gameKey);

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

        var publisher = await GetPublisherFromSQLServerByGameKey(unitOfWork, automapper, gameKey);
        publisher ??= await GetPublisherFromMongoDBByGameKey(mongoUnitOfWork, automapper, gameKey);

        return publisher;
    }

    public async Task<GameModelDto> GetGameByIdAsync(Guid gameId)
    {
        logger.LogInformation("Getting game by Id: {gameId}", gameId);
        var game = await GetGameFromSQLServerById(unitOfWork, gameId);

        game ??= await GetGameFromMongoDBById(mongoUnitOfWork, automapper, gameId);

        return game == null ? throw new GamestoreException($"No game found with given id: {gameId}") : automapper.Map<GameModelDto>(game);
    }

    public async Task<GameModelDto> GetGameByKeyAsync(string key)
    {
        logger.LogInformation("Getting game by Key: {key}", key);

        var game = await GetGameFromSQLServerByKey(unitOfWork, automapper, key);
        game ??= await GetGameWithDetailsFromMongoDBByKey(mongoUnitOfWork, automapper, key);

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

        var game = automapper.Map<Product>(gameModel.Game);
        var addedGame = await AddGameToRepository(unitOfWork, gameModel, game);

        var genres = gameModel.Genres;
        var platforms = gameModel.Platforms;
        await AddGameGenresTorepository(unitOfWork, addedGame, genres);
        await AddGamePlatformsToRepository(unitOfWork, addedGame, platforms);

        await mongoLoggingService.LogGameAdd(gameModel);
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
            var gameFromMongoDB = await GetGameWithDetailsFromMongoDBById(mongoUnitOfWork, automapper, id);
            await CopyGameFromMongoDBToSQLServerIfDoesntExistThere(unitOfWork, automapper, gameFromMongoDB, existingGameInSQLServer);
            oldObjectState = gameFromMongoDB;
        }

        await DeleteGameGenresFromRepository(unitOfWork, gameModel.Game.Id);
        await DeleteGamePlatformsFromRepository(unitOfWork, gameModel.Game.Id);

        var game = automapper.Map<Product>(gameModel.Game);
        await UpdateGameInrepository(unitOfWork, gameModel, game);

        await mongoLoggingService.LogGameUpdate(oldObjectState, newObjectState);
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
            await mongoLoggingService.LogGameDelete(gameId);
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

        var game = await GetGameFromSQLServerByKey(unitOfWork, automapper, gameKey);
        game ??= await GetGameWithDetailsFromMongoDBByKey(mongoUnitOfWork, automapper, gameKey) ?? throw new GamestoreException($"No game found with given key: {gameKey}");
        var unitInStock = game.UnitInStock;

        if (unitInStock > 0)
        {
            var exisitngOrder = await unitOfWork.OrderRepository.GetByCustomerIdAsync(customerId);
            if (exisitngOrder == null || exisitngOrder.Status != OrderStatus.Open)
            {
                await CreateNewOrder(unitOfWork, automapper, customerId, quantity, game, unitInStock);
            }
            else
            {
                await UpdateExistingOrder(unitOfWork, automapper, quantity, game, unitInStock, exisitngOrder);
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
                await CopyGameFromMongoDBToSQLServerIfDoesntExistThere(unitOfWork, automapper, gameModelDto, game);
            }

            game = await unitOfWork.GameRepository.GetGameByKeyAsync(@gameKey);
            var commenttoAdd = ConvertCommentModelDtoToComment(comment, game.Id);

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

    private static async Task UpdateExistingOrder(IUnitOfWork unitOfWork, IMapper automapper, int quantity, GameModelDto game, int unitInStock, Order? exisitngOrder)
    {
        if (game.Id is not null)
        {
            OrderProduct existingOrderGame = await unitOfWork.OrderGameRepository.GetByOrderIdAndProductIdAsync(exisitngOrder.Id, (Guid)game.Id);

            if (existingOrderGame != null)
            {
                await UpdateExistingOrderGame(unitOfWork, quantity, unitInStock, existingOrderGame);
            }
            else
            {
                await CreateNewOrderGame(unitOfWork, automapper, quantity, game, unitInStock, exisitngOrder);
            }
        }
    }

    private static async Task CreateNewOrderGame(IUnitOfWork unitOfWork, IMapper automapper, int quantity, GameModelDto game, int unitInStock, Order? exisitngOrder)
    {
        if (game.Id is not null)
        {
            var expectedTotalQuantity = quantity < unitInStock ? quantity : unitInStock;

            var gameInSQLServer = await unitOfWork.GameRepository.GetByIdAsync((Guid)game.Id);
            if (gameInSQLServer is null)
            {
                await CopyGameFromMongoDBToSQLServerIfDoesntExistThere(unitOfWork, automapper, game, gameInSQLServer);
            }

            OrderProduct newOrderGame = new OrderProduct()
            {
                OrderId = exisitngOrder.Id,
                ProductId = game.Id ?? Guid.Empty,
                Price = game.Price,
                Discount = game.Discontinued,
                Quantity = expectedTotalQuantity,
            };

            await unitOfWork.OrderGameRepository.AddAsync(newOrderGame);
            await unitOfWork.SaveAsync();
        }
    }

    private static async Task CopyGameFromMongoDBToSQLServerIfDoesntExistThere(IUnitOfWork unitOfWork, IMapper automapper, GameModelDto game, Product? gameInSQLServer)
    {
        if (gameInSQLServer is null)
        {
            var gameToAdd = automapper.Map<Product>(game);

            await CreateGenreInSQLServerIfDoesntExist(unitOfWork, game, gameToAdd);
            await CreatePlatformInSQLServerIfDoesntExist(unitOfWork, game, gameToAdd);
            await CreatePublisherInSQLServerIfDoesntExist(unitOfWork, game, gameToAdd);

            await unitOfWork.GameRepository.AddAsync(gameToAdd);
            await unitOfWork.SaveAsync();
        }
    }

    private static async Task CreatePublisherInSQLServerIfDoesntExist(IUnitOfWork unitOfWork, GameModelDto game, Product? gameInSQLServer)
    {
        if (game.Publisher is not null && game.Publisher.Id is not null)
        {
            var publisherInSQLServer = await unitOfWork.PublisherRepository.GetByIdAsync((Guid)game.Publisher.Id);

            if (publisherInSQLServer is null)
            {
                await unitOfWork.PublisherRepository.AddAsync(new Supplier { Id = (Guid)game.Publisher.Id, CompanyName = game.Publisher.CompanyName });
                await unitOfWork.SaveAsync();
                await AttachPublisherFromSQLServer(unitOfWork, game, gameInSQLServer);
            }
            else
            {
                var publisherId = game.Publisher.Id;
                if (publisherId is not null)
                {
                    var pub = await unitOfWork.PublisherRepository.GetByIdAsync((Guid)publisherId);
                    if (pub is not null)
                    {
                        await AttachPublisherFromSQLServer(unitOfWork, game, gameInSQLServer);
                    }
                }
            }
        }
    }

    private static async Task AttachPublisherFromSQLServer(IUnitOfWork unitOfWork, GameModelDto game, Product? gameInSQLServer)
    {
        var publisherId = game.Publisher.Id;
        if (publisherId is not null)
        {
            var pub = await unitOfWork.PublisherRepository.GetByIdAsync((Guid)publisherId);
            if (pub is not null)
            {
                gameInSQLServer.Publisher = pub;
            }
        }
    }

    private static async Task CreatePlatformInSQLServerIfDoesntExist(IUnitOfWork unitOfWork, GameModelDto game, Product gameToAdd)
    {
        var firstPlatform = game.Platforms[0];
        if (firstPlatform != null && firstPlatform.Id != null && game.Id != null)
        {
            var existingPlatform = await unitOfWork.PlatformRepository.GetByIdAsync((Guid)firstPlatform.Id);
            if (existingPlatform is null)
            {
                await unitOfWork.GenreRepository.AddAsync(new() { Id = (Guid)firstPlatform.Id, Name = firstPlatform.Type });
            }

            gameToAdd.ProductPlatforms =
            [
                 new ProductPlatform { ProductId = game.Id.Value, PlatformId = (Guid)firstPlatform.Id }
            ];
        }
    }

    private static async Task CreateGenreInSQLServerIfDoesntExist(IUnitOfWork unitOfWork, GameModelDto game, Product gameToAdd)
    {
        var firstGenre = game.Genres[0];
        if (firstGenre != null && firstGenre.Id != null && game.Id != null)
        {
            var existingGenre = await unitOfWork.GenreRepository.GetByIdAsync((Guid)firstGenre.Id);
            if (existingGenre is null)
            {
                await unitOfWork.GenreRepository.AddAsync(new() { Id = (Guid)firstGenre.Id, Name = firstGenre.Name });
            }

            gameToAdd.ProductCategories =
            [
                 new ProductCategory { ProductId = game.Id.Value, CategoryId = (Guid)firstGenre.Id }
            ];
        }
    }

    private static async Task UpdateExistingOrderGame(IUnitOfWork unitOfWork, int quantity, int unitInStock, OrderProduct existingOrderGame)
    {
        var expectedTotalQuantity = quantity + existingOrderGame.Quantity;
        expectedTotalQuantity = expectedTotalQuantity < unitInStock ? expectedTotalQuantity : unitInStock;
        existingOrderGame.Quantity = expectedTotalQuantity;
        await unitOfWork.OrderGameRepository.UpdateAsync(existingOrderGame);
        await unitOfWork.SaveAsync();
    }

    private static async Task<int> CreateNewOrder(IUnitOfWork unitOfWork, IMapper automapper, Guid customerId, int quantity, GameModelDto game, int unitInStock)
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
                await CopyGameFromMongoDBToSQLServerIfDoesntExistThere(unitOfWork, automapper, game, gameInSQLServer);
            }

            var newOrderId = Guid.NewGuid();
            List<OrderProduct> orderGames = [new() { OrderId = newOrderId, ProductId = (Guid)game.Id, Price = game.Price, Discount = game.Discontinued, Quantity = quantity }];
            Order order = new() { Id = newOrderId, CustomerId = customerId, OrderDate = DateTime.Now, OrderProducts = orderGames, Status = OrderStatus.Open };
            await unitOfWork.OrderRepository.AddAsync(order);
            await unitOfWork.OrderGameRepository.AddAsync(orderGames[0]);
            await unitOfWork.SaveAsync();
        }

        return quantity;
    }

    private static async Task<Product> AddGameToRepository(IUnitOfWork unitOfWork, GameDtoWrapper gameModel, Product game)
    {
        game.PublisherId = gameModel.Publisher;

        var addedGame = await unitOfWork.GameRepository.AddAsync(game);
        await unitOfWork.SaveAsync();
        return addedGame;
    }

    private static async Task AddGamePlatformsToRepository(IUnitOfWork unitOfWork, Product addedGame, List<Guid> platforms)
    {
        foreach (var platformId in platforms)
        {
            await unitOfWork.GamePlatformRepository.AddAsync(new ProductPlatform() { ProductId = addedGame.Id, PlatformId = platformId });
        }

        await unitOfWork.SaveAsync();
    }

    private static async Task AddGameGenresTorepository(IUnitOfWork unitOfWork, Product addedGame, List<Guid> genres)
    {
        foreach (var genreId in genres)
        {
            await unitOfWork.GameGenreRepository.AddAsync(new ProductCategory() { ProductId = addedGame.Id, CategoryId = genreId });
        }

        await unitOfWork.SaveAsync();
    }

    private static async Task UpdateGameInrepository(IUnitOfWork unitOfWork, GameDtoWrapper gameModel, Product game)
    {
        game.PublisherId = gameModel.Publisher;

        game.ProductCategories = [];
        game.ProductPlatforms = [];

        foreach (var genre in gameModel.Genres)
        {
            game.ProductCategories.Add(new() { ProductId = (Guid)gameModel.Game.Id!, CategoryId = genre });
        }

        foreach (var platform in gameModel.Platforms)
        {
            game.ProductPlatforms.Add(new() { ProductId = (Guid)gameModel.Game.Id!, PlatformId = platform });
        }

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

    private static async Task DeleteOrderGamesFromRepository(IUnitOfWork unitOfWork, Product? game)
    {
        var orderGames = await unitOfWork.OrderGameRepository.GetAllAsync();
        var orderGamesToRemove = orderGames.Where(x => x.ProductId == game.Id);
        foreach (var og in orderGamesToRemove)
        {
            unitOfWork.OrderGameRepository.Delete(og);
        }
    }

    private static async Task SoftDeleteGameFromRepository(IUnitOfWork unitOfWork, Product game)
    {
        await unitOfWork.GameRepository.SoftDelete(game);
        await unitOfWork.SaveAsync();
    }

    private static async Task DeleteGameFromRepository(IUnitOfWork unitOfWork, Product game)
    {
        unitOfWork.GameRepository.Delete(game);
        await unitOfWork.SaveAsync();
    }

    private static Comment ConvertCommentModelDtoToComment(CommentModelDto comment, Guid gameId)
    {
        return new Comment()
        {
            ProductId = gameId,
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

    private static async Task IncreaseGameViewCounter(IUnitOfWork unitOfWork, Product? game)
    {
        if (game is not null)
        {
            game.NumberOfViews++;
            await unitOfWork.SaveAsync();
        }
    }

    private static async Task<List<GenreModelDto>> GetGenresFromMongoDBByGameKey(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, string gameKey)
    {
        var product = await mongoUnitOfWork.ProductRepository.GetByNameAsync(gameKey);
        var category = await mongoUnitOfWork.CategoryRepository.GetById(product.CategoryID);

        return [automapper.Map<GenreModelDto>(category)];
    }

    private static async Task<List<GenreModelDto>> GetGenresFromSQLServerByGameKey(IUnitOfWork unitOfWork, IMapper automapper, string gameKey)
    {
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);

        List<GenreModelDto> genreModels = [];
        if (game is not null)
        {
            var genres = await unitOfWork.GameRepository.GetGenresByGameAsync(game.Id);

            foreach (var genre in genres)
            {
                genreModels.Add(automapper.Map<GenreModelDto>(genre));
            }

            return genreModels;
        }

        return null;
    }

    private static async Task<PublisherModelDto> GetPublisherFromMongoDBByGameKey(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, string gameKey)
    {
        var product = await mongoUnitOfWork.ProductRepository.GetByNameAsync(gameKey);
        var supplier = await mongoUnitOfWork.SupplierRepository.GetByIdAsync(product.SupplierID);

        return automapper.Map<PublisherModelDto>(supplier);
    }

    private static async Task<PublisherModelDto> GetPublisherFromSQLServerByGameKey(IUnitOfWork unitOfWork, IMapper automapper, string gameKey)
    {
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
        if (game is not null)
        {
            var publisher = await unitOfWork.GameRepository.GetPublisherByGameAsync(game.Id);

            return automapper.Map<PublisherModelDto>(publisher);
        }

        return null;
    }

    private static async Task<GameModelDto> GetGameWithDetailsFromMongoDBByKey(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, string key)
    {
        var product = await mongoUnitOfWork.ProductRepository.GetByNameAsync(key);
        var gameFromProduct = automapper.Map<GameModelDto>(product);
        await SetGameDetailsForMongoDBGame(mongoUnitOfWork, gameFromProduct);

        return gameFromProduct;
    }

    private static async Task<GameModelDto> GetGameWithDetailsFromMongoDBById(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, int id)
    {
        var product = await mongoUnitOfWork.ProductRepository.GetByIdAsync(id);
        var gameFromProduct = automapper.Map<GameModelDto>(product);
        await SetGameDetailsForMongoDBGame(mongoUnitOfWork, gameFromProduct);

        return gameFromProduct;
    }

    private static async Task SetGameDetailsForMongoDBGame(IMongoUnitOfWork mongoUnitOfWork, GameModelDto gameFromProduct)
    {
        if (gameFromProduct.Publisher.Id is not null)
        {
            gameFromProduct.Publisher.CompanyName = (await mongoUnitOfWork.SupplierRepository.GetByIdAsync(GuidHelpers.GuidToInt((Guid)gameFromProduct.Publisher.Id))).CompanyName;
        }

        if (gameFromProduct.Genres[0] is not null)
        {
#pragma warning disable CS8629 // Nullable value type may be null.
            gameFromProduct.Genres[0].Name = (await mongoUnitOfWork.CategoryRepository.GetById(GuidHelpers.GuidToInt((Guid)gameFromProduct.Genres[0].Id))).CategoryName;
#pragma warning restore CS8629 // Nullable value type may be null.
        }

        if (gameFromProduct.Platforms[0] is not null)
        {
            gameFromProduct.Platforms[0].Type = "Physical Product";
        }
    }

    private static async Task<GameModelDto> GetGameFromSQLServerByKey(IUnitOfWork unitOfWork, IMapper automapper, string key)
    {
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(key);
        await IncreaseGameViewCounter(unitOfWork, game);
        return automapper.Map<GameModelDto>(game);
    }

    private static async Task<Product?> GetGameFromMongoDBById(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, Guid gameId)
    {
        int id = GuidHelpers.GuidToInt(gameId);
        var product = await mongoUnitOfWork.ProductRepository.GetByIdAsync(id);
        var game = automapper.Map<Product>(product);
        return game;
    }

    private static async Task<Product?> GetGameFromSQLServerById(IUnitOfWork unitOfWork, Guid gameId)
    {
        return await unitOfWork.GameRepository.GetByIdAsync(gameId);
    }

    private static void CheckIfCurrentPageDoesntExceedTotalNumberOfPages(GameFiltersDto gameFilters, FilteredGamesDto filteredGameDtos)
    {
        if (gameFilters.Page <= gameFilters.NumberOfPagesAfterFiltration)
        {
            filteredGameDtos.CurrentPage = gameFilters.Page;
        }
        else
        {
            filteredGameDtos.CurrentPage = gameFilters.NumberOfPagesAfterFiltration;
        }
    }

    private static async Task FilterGamesFromSQLServer(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, GameFiltersDto gameFilters, FilteredGamesDto filteredGameDtos, IGameProcessingPipelineService gameProcessingPipelineService)
    {
        var games = unitOfWork.GameRepository.GetGamesAsQueryable();
        var gamesFromSQLServer = (await gameProcessingPipelineService.ProcessGamesAsync(unitOfWork, mongoUnitOfWork, gameFilters, games)).ToList();
        if (gamesFromSQLServer.Count != 0)
        {
            filteredGameDtos.Games.AddRange(automapper.Map<List<GameModelDto>>(gamesFromSQLServer));
        }
    }

    private static async Task FilterProductsFromMongoDB(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, GameFiltersDto gameFilters, FilteredGamesDto filteredGameDtos, IGameProcessingPipelineService gameProcessingPipelineService)
    {
        var productsFromMongoDB = await mongoUnitOfWork.ProductRepository.GetAllAsync();
        var prodctsAsGames = automapper.Map<List<Product>>(productsFromMongoDB);
        prodctsAsGames = prodctsAsGames.Except(await unitOfWork.GameRepository.GetAllAsync()).ToList();
        var productsAsGamesFromMongoDB = (await gameProcessingPipelineService.ProcessGamesAsync(unitOfWork, mongoUnitOfWork, gameFilters, prodctsAsGames.AsQueryable())).ToList();
        if (productsAsGamesFromMongoDB.Count != 0)
        {
            filteredGameDtos.Games.AddRange(automapper.Map<List<GameModelDto>>(productsAsGamesFromMongoDB));
        }
    }

    private static void SetTotalNumberOfPagesAfterFiltering(GameFiltersDto gameFilters, FilteredGamesDto filteredGameDtos)
    {
        filteredGameDtos.TotalPages = gameFilters.NumberOfPagesAfterFiltration;
    }
}
