using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering;
using Gamestore.BLL.Filtering.Handlers;
using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Models;
using Gamestore.BLL.MongoLogging;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Enums;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Helpers;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Services;
using Gamestore.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using PaginationOptionsDto = Gamestore.BLL.Filtering.Models.PaginationOptionsDto;

namespace Gamestore.Tests.BLL;

public class GameServiceTests
{
    private readonly Mock<ILogger<GameService>> _logger;
    private readonly GameProcessingPipelineDirector _gameProcessingPipelineDirector;
    private readonly Mock<IMongoLoggingService> _mongoLoggingService = new();
    private readonly Mock<IMongoUnitOfWork> _mongoUnitOfWork = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly IMapper _autoMapper;

    public GameServiceTests()
    {
        var autoMapperConfiguration = new MapperConfiguration(m => m.AddProfile(new MappingProfile()));
        _autoMapper = autoMapperConfiguration.CreateMapper();

        _logger = new Mock<ILogger<GameService>>();

        var genreFilterHandler = new GenreFilterHandler();
        var gameProcessingPipelineBuilder = new GameProcessingPipelineBuilder();
        var nameFilterHandler = new NameFilterHandler();
        var paginationFilterHandler = new PaginationFilterHandler();
        var platformFilterHandler = new PlatformFilterHandler();
        var priceFilterHandler = new PriceFilterHandler();
        var publishDateHandler = new PublishDateHandler();
        var publishFilterHandler = new PublisherFilterHandler();
        var sortingHandler = new SortingHandler();

        _gameProcessingPipelineDirector = new GameProcessingPipelineDirector(
            gameProcessingPipelineBuilder,
            genreFilterHandler,
            nameFilterHandler,
            paginationFilterHandler,
            platformFilterHandler,
            priceFilterHandler,
            publishDateHandler,
            publishFilterHandler,
            sortingHandler);
    }

    [Theory]
    [InlineData("Rpg", "Desktop", "Blizzard", false)]
    [InlineData("Racing", "Mobile", "Activision", false)]
    [InlineData("Simulator", "Console", "BioWare", false)]
    [InlineData("Rpg", "Desktop", "Blizzard", true)]
    [InlineData("Racing", "Mobile", "Activision", true)]
    [InlineData("Simulator", "Console", "BioWare", true)]

    public async Task GetFilteredGamesAsyncShouldreturnOneGameForGenrePlatformPublisherCombination(string genreName, string platformType, string publisherCompanyName, bool canSeeDeletedGames)
    {
        GameFiltersDto filters = new GameFiltersDto
        {
            Genres = [BllHelpers.Genres.Find(x => x.Name == genreName).Id],
            Platforms = [BllHelpers.Platforms.Find(x => x.Type == platformType).Id],
            Publishers = [BllHelpers.Publishers.Find(x => x.CompanyName == publisherCompanyName).Id],
        };

        SetupUnitOfWorkForFilterTests(_unitOfWork, _mongoUnitOfWork, canSeeDeletedGames);

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters, canSeeDeletedGames);

        Assert.Single(result.Games);
    }

    [Theory]
    [InlineData("Rpg", "Mobile", "BioWare", false)]
    [InlineData("Racing", "Desktop", "BioWare", false)]
    [InlineData("Simulator", "Mobile", "Blizzard", false)]
    [InlineData("Rpg", "Mobile", "BioWare", true)]
    [InlineData("Racing", "Desktop", "BioWare", true)]
    [InlineData("Simulator", "Mobile", "Blizzard", true)]

    public async Task GetFilteredGamesAsyncShouldreturnNoGamesForGenrePlatformPublisherCombination(string genreName, string platformType, string publisherCompanyName, bool canSeeDeletedGames)
    {
        GameFiltersDto filters = new GameFiltersDto
        {
            Genres = [BllHelpers.Genres.Find(x => x.Name == genreName).Id],
            Platforms = [BllHelpers.Platforms.Find(x => x.Type == platformType).Id],
            Publishers = [BllHelpers.Publishers.Find(x => x.CompanyName == publisherCompanyName).Id],
        };

        SetupUnitOfWorkForFilterTests(_unitOfWork, _mongoUnitOfWork, canSeeDeletedGames);

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters, canSeeDeletedGames);

        Assert.Empty(result.Games);
    }

    [Theory]
    [InlineData(0, 100, false)]
    [InlineData(101, 200, false)]
    [InlineData(201, 300, false)]
    [InlineData(0, 100, true)]
    [InlineData(101, 200, true)]
    [InlineData(201, 300, true)]
    public async Task GetFilteredGamesAsyncShouldreturnTwoGamesForPriceRange(int minPriace, int maxPrice, bool canSeeDeletedGames)
    {
        GameFiltersDto filters = new GameFiltersDto
        {
            MinPrice = minPriace,
            MaxPrice = maxPrice,
        };

        SetupUnitOfWorkForFilterTests(_unitOfWork, _mongoUnitOfWork, canSeeDeletedGames);

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters, canSeeDeletedGames);

        Assert.Equal(2, result.Games.Count);
    }

    [Theory]
    [InlineData(300, false)]
    [InlineData(300, true)]

    public async Task GetFilteredGamesAsyncShouldreturnSixGamesWithoutMinimalPrice(int maxPrice, bool canSeeDeletedGames)
    {
        GameFiltersDto filters = new GameFiltersDto
        {
            MaxPrice = maxPrice,
        };

        SetupUnitOfWorkForFilterTests(_unitOfWork, _mongoUnitOfWork, canSeeDeletedGames);

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters, canSeeDeletedGames);

        Assert.Equal(6, result.Games.Count);
    }

    [Theory]
    [InlineData(300, false)]
    [InlineData(300, true)]

    public async Task GetFilteredGamesAsyncShouldreturnOneGameWithoutMaximalPrice(int minPrice, bool canSeeDeletedGames)
    {
        GameFiltersDto filters = new GameFiltersDto
        {
            MinPrice = minPrice,
        };

        SetupUnitOfWorkForFilterTests(_unitOfWork, _mongoUnitOfWork, canSeeDeletedGames);

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters, canSeeDeletedGames);

        Assert.Equal(2, result.Games.Count);
    }

    [Theory]
    [InlineData("Baldurs", false)]
    [InlineData("Drive", false)]
    [InlineData("mba", false)]
    [InlineData("Product1", false)]
    [InlineData("uct3", false)]
    [InlineData("Baldurs", true)]
    [InlineData("Drive", true)]
    [InlineData("mba", true)]
    [InlineData("Product1", true)]
    [InlineData("uct3", true)]

    public async Task GetFilteredGamesAsyncShouldreturnOneGameForNameFilter(string name, bool canSeeDeletedGames)
    {
        GameFiltersDto filters = new GameFiltersDto
        {
            Name = name,
        };

        SetupUnitOfWorkForFilterTests(_unitOfWork, _mongoUnitOfWork, canSeeDeletedGames);

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters, canSeeDeletedGames);

        Assert.Single(result.Games);
    }

    [Theory]
    [InlineData("Last week", false)]
    [InlineData("Last month", false)]
    [InlineData("Last year", false)]
    [InlineData("2 years", false)]
    [InlineData("3 years", false)]
    [InlineData("Last week", true)]
    [InlineData("Last month", true)]
    [InlineData("Last year", true)]
    [InlineData("2 years", true)]
    [InlineData("3 years", true)]

    public async Task GetFilteredGamesAsyncShouldreturnCorrectGamesForPublishDate(string publishDate, bool canSeeDeletedGames)
    {
        GameFiltersDto filters = new GameFiltersDto
        {
            DatePublishing = publishDate,
        };

        int expectedNumberOfGames = 0;
        switch (publishDate)
        {
            case "Last week":
                expectedNumberOfGames = BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).Count(x => x.PublishDate >= DateOnly.FromDateTime(DateTime.Now).AddDays(-7));
                break;
            case "Last month":
                expectedNumberOfGames = BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).Count(x => x.PublishDate >= DateOnly.FromDateTime(DateTime.Now).AddMonths(-1));
                break;
            case "Last year":
                expectedNumberOfGames = BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).Count(x => x.PublishDate >= DateOnly.FromDateTime(DateTime.Now).AddYears(-1));
                break;
            case "2 years":
                expectedNumberOfGames = BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).Count(x => x.PublishDate >= DateOnly.FromDateTime(DateTime.Now).AddYears(-2));
                break;
            case "3 years":
                expectedNumberOfGames = BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).Count(x => x.PublishDate >= DateOnly.FromDateTime(DateTime.Now).AddYears(-3));
                break;
            default:
                break;
        }

        SetupUnitOfWorkForFilterTests(_unitOfWork, _mongoUnitOfWork, canSeeDeletedGames);

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters, canSeeDeletedGames);

        Assert.Equal(expectedNumberOfGames, result.Games.Count);
    }

    [Theory]
    [InlineData("Most popular", false)]
    [InlineData("Most commented", false)]
    [InlineData("Price ASC", false)]
    [InlineData("Price DESC", false)]
    [InlineData("New", false)]
    [InlineData("Most popular", true)]
    [InlineData("Most commented", true)]
    [InlineData("Price ASC", true)]
    [InlineData("Price DESC", true)]
    [InlineData("New", true)]

    public async Task GetFilteredGamesAsyncShouldreturnGamesInCorrectOrderForsortingOption(string sortingOption, bool canSeeDeletedGames)
    {
        GameFiltersDto filters = new GameFiltersDto
        {
            Sort = sortingOption,
        };

        List<Game> expectedGames = [];
        switch (sortingOption)
        {
            case "Most popular":
                expectedGames = [.. BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).OrderByDescending(x => x.NumberOfViews)];
                expectedGames.AddRange(_autoMapper.Map<List<Game>>(BllHelpers.MongoProducts));
                break;
            case "Most commented":
                expectedGames = [.. BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).OrderByDescending(x => x.Comments.Count)];
                expectedGames.AddRange(_autoMapper.Map<List<Game>>(BllHelpers.MongoProducts));
                break;
            case "Price ASC":
                expectedGames = [.. BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).OrderBy(x => x.Price)];
                expectedGames.AddRange(_autoMapper.Map<List<Game>>(BllHelpers.MongoProducts.OrderBy(x => x.UnitPrice)));
                break;
            case "Price DESC":
                expectedGames = [.. BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).OrderByDescending(x => x.Price)];
                expectedGames.AddRange(_autoMapper.Map<List<Game>>(BllHelpers.MongoProducts.OrderByDescending(x => x.UnitPrice)));
                break;
            case "New":
                expectedGames = [.. BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).OrderByDescending(x => x.PublishDate)];
                expectedGames.AddRange(_autoMapper.Map<List<Game>>(BllHelpers.MongoProducts));
                break;
            default:
                break;
        }

        SetupUnitOfWorkForFilterTests(_unitOfWork, _mongoUnitOfWork, canSeeDeletedGames);

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters, canSeeDeletedGames);

        for (var i = 0; i < result.Games.Count; i++)
        {
            Assert.Equal(expectedGames[i].Id, result.Games[i].Id);
        }
    }

    [Theory]
    [InlineData("10", false)]
    [InlineData("20", false)]
    [InlineData("50", false)]
    [InlineData("100", false)]
    [InlineData("all", false)]
    [InlineData("10", true)]
    [InlineData("20", true)]
    [InlineData("50", true)]
    [InlineData("100", true)]
    [InlineData("all", true)]

    public async Task GetFilteredGamesAsyncShouldReturnCorrectNumberOfPages(string noOfGamesPerPage, bool canSeeDeletedGames)
    {
        GameFiltersDto filters = new GameFiltersDto
        {
            PageCount = noOfGamesPerPage,
        };

        var numberOfProductsInMongo = BllHelpers.MongoProducts.Count;

        List<Game> games = [];
        for (int i = 0; i < 100; i++)
        {
            games.Add(new Game()
            {
                Id = Guid.NewGuid(),
                Name = i.ToString(),
                Key = i.ToString(),
                IsDeleted = canSeeDeletedGames,
                Comments = [],
                ProductCategories = [BllHelpers.GameGenres[0]],
                ProductPlatforms = [BllHelpers.GamePlatforms[0]],
                OrderProducts = [],
                Publisher = BllHelpers.Publishers[0],
                PublishDate = DateOnly.FromDateTime(DateTime.Now),
                NumberOfViews = i,
            });
        }

        int expectedNumberOfPages = 0;
        int expectedNumberOfGamesPerPage = 0;
        switch (noOfGamesPerPage)
        {
            case "10":
                expectedNumberOfPages = 11;
                expectedNumberOfGamesPerPage = 10;
                break;
            case "20":
                expectedNumberOfPages = 6;
                expectedNumberOfGamesPerPage = 20;
                break;
            case "50":
                expectedNumberOfPages = 3;
                expectedNumberOfGamesPerPage = 50;
                break;
            case "100":
                expectedNumberOfPages = 2;
                expectedNumberOfGamesPerPage = 100;
                break;
            case "all":
                expectedNumberOfPages = 1;
                expectedNumberOfGamesPerPage = games.Count + numberOfProductsInMongo;
                break;
            default:
                break;
        }

        SetupUnitOfWorkForFilterTests(_unitOfWork, _mongoUnitOfWork, canSeeDeletedGames, games);
        _unitOfWork.Setup(x => x.GameRepository.GetAllAsync()).ReturnsAsync(games.Where(x => x.IsDeleted == canSeeDeletedGames).ToList());

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters, canSeeDeletedGames);

        Assert.Equal(expectedNumberOfPages, filters.NumberOfPagesAfterFiltration);
        Assert.Equal(expectedNumberOfGamesPerPage, result.Games.Count);
    }

    [Fact]
    public async Task GetAllGamesAsyncShouldReturnAllGames()
    {
        // Arrange
        var expectedFromSql = BllHelpers.Games.Where(x => !x.IsDeleted).ToList();
        var expectedFromMongo = BllHelpers.MongoProducts.ToList();
        var expectedCount = expectedFromSql.Count + expectedFromMongo.Count;

        _unitOfWork.Setup(x => x.GameRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Games.Where(x => !x.IsDeleted).ToList());
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.MongoProducts]);
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var actual = await gameService.GetAllGamesAsync(canSeeDeletedGames: false);

        // Assert
        Assert.Equal(expectedCount, actual.Count);
    }

    [Fact]
    public async Task GetAllGamesAsyncShouldReturnAllGamesWithDeleted()
    {
        // Arrange
        var expectedFromSql = BllHelpers.Games.Where(x => x.IsDeleted).ToList();
        var expectedFromMongo = BllHelpers.MongoProducts.ToList();
        var expectedCount = expectedFromSql.Count + expectedFromMongo.Count;

        _unitOfWork.Setup(x => x.GameRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Games.Where(x => x.IsDeleted).ToList());
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.MongoProducts]);
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var actual = await gameService.GetAllGamesAsync(canSeeDeletedGames: false);

        // Assert
        Assert.Equal(expectedCount, actual.Count);
    }

    [Fact]
    public async Task GetGameByIdAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var expectedId = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63");
        var expected = BllHelpers.Games.First(x => x.Id == expectedId);

        _unitOfWork.Setup(x => x.GameRepository.GetByOrderIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First(x => x.Id == expectedId));
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetByIdAsync(It.IsAny<int>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var actual = await gameService.GetGameByIdAsync(expectedId);

        // Assert
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Key, actual.Key);
        Assert.Equal(expected.Price, actual.Price);
    }

    [Fact]
    public async Task GetGameByIdAsyncShouldReturnCorrectProductFromMongo()
    {
        // Arrange
        var expectedId = 1;
        var id = GuidHelpers.IntToGuid(expectedId);
        var expected = BllHelpers.MongoProducts.First(x => x.ProductId == expectedId);

        _unitOfWork.Setup(x => x.GameRepository.GetByOrderIdAsync(It.IsAny<Guid>()));
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(BllHelpers.MongoProducts.First(x => x.ProductId == expectedId));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var actual = await gameService.GetGameByIdAsync(id);

        // Assert
        Assert.Equal(GuidHelpers.IntToGuid(expected.ProductId), actual.Id);
        Assert.Equal(expected.ProductName, actual.Name);
    }

    [Fact]
    public async Task GetGameByKeyAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var expectedKey = "BG";
        var expected = BllHelpers.Games.First(x => x.Key == expectedKey);

        _unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>())).ReturnsAsync(BllHelpers.Games.First(x => x.Key == expectedKey));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var actual = await gameService.GetGameByKeyAsync(expectedKey);

        // Assert
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Key, actual.Key);
        Assert.Equal(expected.Price, actual.Price);
    }

    [Fact]
    public async Task GetGameByKeyAsyncShouldReturnCorrectProductFromMongo()
    {
        // Arrange
        var expectedKey = BllHelpers.MongoProducts[0].ProductName;
        var expected = BllHelpers.MongoProducts.First(x => x.ProductName == expectedKey);

        _unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>()));
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(BllHelpers.MongoProducts.First(x => x.ProductName == expectedKey));
        _mongoUnitOfWork.Setup(x => x.SupplierRepository.GetByIdAsync(It.IsAny<int>())).Returns((int id) => Task.FromResult(BllHelpers.GetMongoSupplierById(id)));
        _mongoUnitOfWork.Setup(x => x.CategoryRepository.GetById(It.IsAny<int>())).Returns((int id) => Task.FromResult(BllHelpers.GetMongoCategoryById(id)));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var actual = await gameService.GetGameByKeyAsync(expectedKey);

        // Assert
        Assert.Equal(GuidHelpers.IntToGuid(expected.ProductId), actual.Id);
        Assert.Equal(expected.ProductName, actual.Name);
    }

    [Fact]
    public async Task AddGameAsyncShouldAddGame()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var publisher = new PublisherModelDto() { Id = Guid.NewGuid(), CompanyName = "Test name" };
        var gameToAdd = new GameModelDto()
        {
            Id = gameId,
            Name = "Digital Combat Simulator",
            Key = "DCS",
            Price = 150,
            Discontinued = 10,
            Publisher = publisher,
            UnitInStock = 100,
            Description = "Flight sim",
            Platforms = BllHelpers.PlatformModelDtos,
            Genres = BllHelpers.GenreModelDtos,
        };
        var gameToAddWrapper = new GameDtoWrapper() { Game = gameToAdd, Publisher = (Guid)publisher.Id, Platforms = [Guid.NewGuid()], Genres = [Guid.NewGuid()] };

        _unitOfWork.Setup(x => x.GameRepository.AddAsync(It.IsAny<Game>())).ReturnsAsync(new Game());
        _unitOfWork.Setup(x => x.GameRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Games]);
        _unitOfWork.Setup(x => x.GameGenreRepository.AddAsync(It.IsAny<GameGenres>()));
        _unitOfWork.Setup(x => x.GamePlatformRepository.AddAsync(It.IsAny<GamePlatform>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        await gameService.AddGameAsync(gameToAddWrapper);

        // Assert
        _unitOfWork.Verify(x => x.GameRepository.AddAsync(It.Is<Game>(x => x.Id == gameToAdd.Id && x.Name == gameToAdd.Name
        && x.Key == gameToAdd.Key && x.Description == gameToAdd.Description && x.PublisherId == gameToAdd.Publisher.Id
        && x.Price == gameToAdd.Price && x.UnitInStock == gameToAdd.UnitInStock && x.Discount == gameToAdd.Discontinued)));
        _unitOfWork.Verify(x => x.SaveAsync(), Times.Exactly(3));
    }

    [Fact]
    public async Task DeleteGameAsyncShouldDeleteGame()
    {
        // Arrange
        var gameToDelete = BllHelpers.Games[0];
        _unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        _unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        _unitOfWork.Setup(x => x.GameRepository.Delete(It.IsAny<Game>()));
        _unitOfWork.Setup(x => x.GameRepository.GetByOrderIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First(x => x.Id == gameToDelete.Id));
        _unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync([]);
        _unitOfWork.Setup(x => x.GameGenreRepository.Delete(It.IsAny<GameGenres>()));
        _unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync([]);
        _unitOfWork.Setup(x => x.GamePlatformRepository.Delete(It.IsAny<GamePlatform>()));
        _unitOfWork.Setup(x => x.OrderGameRepository.GetAllAsync()).ReturnsAsync([]);
        _unitOfWork.Setup(x => x.OrderGameRepository.Delete(It.IsAny<OrderGame>()));

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        await gameService.DeleteGameByIdAsync(gameToDelete.Id);

        // Assert
        _unitOfWork.Verify(x => x.GameRepository.Delete(It.Is<Game>(x => x.Id == gameToDelete.Id && x.Name == gameToDelete.Name && x.Key == gameToDelete.Key && x.Description == gameToDelete.Description)));
        _unitOfWork.Verify(x => x.SaveAsync(), Times.Exactly(3));
    }

    [Fact]
    public async Task SoftDeleteGameAsyncShouldSetDeleteFlag()
    {
        // Arrange
        var gameToDelete = BllHelpers.Games[0];

        _unitOfWork.Setup(x => x.GameRepository.GetByOrderIdAsync(It.IsAny<Guid>())).ReturnsAsync(gameToDelete);
        _unitOfWork.Setup(x => x.GameRepository.SoftDelete(It.IsAny<Game>())).Callback((Game game) => game.IsDeleted = true);
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        await gameService.SoftDeleteGameByIdAsync(gameToDelete.Id);

        // Assert
        Assert.True(gameToDelete.IsDeleted);
        _unitOfWork.Verify(x => x.SaveAsync(), Times.Exactly(1));
    }

    [Fact]
    public async Task SoftDeleteGameByGameKeyAsyncShouldSetDeleteFlag()
    {
        // Arrange
        var gameToDelete = BllHelpers.Games[0];

        _unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>())).ReturnsAsync(gameToDelete);
        _unitOfWork.Setup(x => x.GameRepository.SoftDelete(It.IsAny<Game>())).Callback((Game game) => game.IsDeleted = true);
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        await gameService.SoftDeleteGameByKeyAsync(gameToDelete.Key);

        // Assert
        Assert.True(gameToDelete.IsDeleted);
        _unitOfWork.Verify(x => x.SaveAsync(), Times.Exactly(1));
    }

    [Fact]
    public async Task UpdateGameAsyncShouldUpdateGame()
    {
        // Arrange
        var gameToUpdate = BllHelpers.GameModelDtos[0];
        gameToUpdate.Name = "Need For Speed";
        gameToUpdate.Key = "NFS";
        gameToUpdate.Description = "Old racing game";
        gameToUpdate.Price = 50;
        gameToUpdate.Discontinued = 75;
        gameToUpdate.UnitInStock = 10;
        gameToUpdate.Platforms = [new PlatformModelDto()];
        gameToUpdate.Genres = [new GenreModelDto()];
        gameToUpdate.Publisher = new PublisherModelDto()
        {
            Id = Guid.NewGuid(),
            CompanyName = "Test Name",
        };
        var gameToUpdateWrapper = new GameDtoWrapper()
        {
            Game = gameToUpdate,
            Publisher = (Guid)gameToUpdate.Publisher.Id,
            Platforms = [Guid.NewGuid()],
            Genres = [Guid.NewGuid()],
        };

        _unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GameGenres.Where(x => x.GameId == gameToUpdate.Id).ToList());
        _unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GamePlatforms.Where(x => x.GameId == gameToUpdate.Id).ToList());
        _unitOfWork.Setup(x => x.GameRepository.GetByOrderIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First);
        _unitOfWork.Setup(x => x.GameRepository.UpdateAsync(It.IsAny<Game>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        await gameService.UpdateGameAsync(gameToUpdateWrapper);

        // Assert
        _unitOfWork.Verify(x => x.GameRepository.UpdateAsync(It.Is<Game>(x => x.Id == gameToUpdate.Id && x.Name == gameToUpdate.Name
            && x.Key == gameToUpdate.Key && x.Description == gameToUpdate.Description && x.PublisherId == gameToUpdate.Publisher.Id
            && x.Price == gameToUpdate.Price && x.UnitInStock == gameToUpdate.UnitInStock && x.Discount == gameToUpdate.Discontinued)));
        _unitOfWork.Verify(x => x.SaveAsync(), Times.Exactly(3));
    }

    [Fact]
    public async Task AddGameToCartAsyncShouldWork()
    {
        var gameToBuy = BllHelpers.Games[0];
        Guid customerId = Guid.NewGuid();
        int quantity = 1;
        Order order = new() { CustomerId = customerId, Status = OrderStatus.Open };
        List<OrderGame> orderGames = [new() { GameId = gameToBuy.Id, OrderId = order.Id, Game = gameToBuy, Order = order }];
        order.OrderGames = orderGames;

        _unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>())).ReturnsAsync(gameToBuy);
        _unitOfWork.Setup(x => x.OrderRepository.GetByCustomerIdAsync(It.IsAny<Guid>())).ReturnsAsync(order);
        _unitOfWork.Setup(x => x.OrderGameRepository.GetByOrderIdAndProductIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(orderGames[0]);
        _unitOfWork.Setup(x => x.OrderGameRepository.UpdateAsync(It.IsAny<OrderGame>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        await gameService.AddGameToCartAsync(customerId, gameToBuy.Key, quantity);

        Assert.Equal(1, order.OrderGames[0].Quantity);
    }

    [Fact]
    public async Task GetGenresByGameAsyncShouldReturnGenres()
    {
        // Arrange
        var game = BllHelpers.Games[0];
        var genreId = game.ProductCategories[0].GenreId;

        _unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>())).ReturnsAsync(game);
        _unitOfWork.Setup(x => x.GameRepository.GetGenresByGameAsync(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(BllHelpers.GetGenresByGameAsync(id)));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var resultGenres = await gameService.GetGenresByGameKeyAsync(game.Key);

        // Assert
        Assert.Equal(genreId, resultGenres.ToList()[0].Id);
    }

    [Fact]
    public async Task GetPlatformsByGameAsyncShouldReturnPlatforms()
    {
        // Arrange
        var game = BllHelpers.Games[0];
        var platformId = game.ProductPlatforms[0].PlatformId;

        _unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>())).ReturnsAsync(game);
        _unitOfWork.Setup(x => x.GameRepository.GetPlatformsByGameAsync(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(BllHelpers.GetPlatformsByGameAsync(id)));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var resultPlatforms = await gameService.GetPlatformsByGameKeyAsync(game.Key);

        // Assert
        Assert.Equal(platformId, resultPlatforms.ToList()[0].Id);
    }

    [Fact]
    public async Task GetPublisherByGameAsyncShouldReturnPublisher()
    {
        // Arrange
        var game = BllHelpers.Games[0];
        var publisherId = game.Publisher.Id;

        _unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>())).ReturnsAsync(game);
        _unitOfWork.Setup(x => x.GameRepository.GetPublisherByGameAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => BllHelpers.GetPublisherByGameAsync(id));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var reultPublisher = await gameService.GetPublisherByGameKeyAsync(game.Key);

        // Assert
        Assert.Equal(publisherId, reultPublisher.Id);
    }

    [Fact]
    public void GetPaginationOptiosShouldreturnCorrectResult()
    {
        // Arrange
        var options = PaginationOptionsDto.PaginationOptions;

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var result = gameService.GetPaginationOptions();

        // Assert
        Assert.Equal(options, result);
    }

    [Fact]
    public void GetPublishDateOptionsShouldreturnCorrectResult()
    {
        // Arrange
        var options = PublishDateOptionsDto.PublishDateOptions;

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var result = gameService.GetPublishDateOptions();

        // Assert
        Assert.Equal(options, result);
    }

    [Fact]
    public void GetGetSortingOptionsShouldreturnCorrectResult()
    {
        // Arrange
        var options = SortingOptionsDto.SortingOptions;

        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Act
        var result = gameService.GetSortingOptions();

        // Assert
        Assert.Equal(options, result);
    }

    [Fact]
    public async Task AddGameAsyncThrowsWhenIvalidName()
    {
        // Arrange
        var gameId = Guid.NewGuid();

        var gameToAdd = new GameModelDto() { Id = gameId, Name = string.Empty, Key = "DCS", Description = "Flight sim", Platforms = BllHelpers.PlatformModelDtos, Genres = BllHelpers.GenreModelDtos };
        var gameToAddWrapper = new GameDtoWrapper() { Game = gameToAdd };

        _unitOfWork.Setup(x => x.GameRepository.AddAsync(It.IsAny<Game>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await gameService.AddGameAsync(gameToAddWrapper);
        });
    }

    [Fact]
    public async Task AddGameAsyncThrowsWhenIvalidKey()
    {
        // Arrange
        var gameId = Guid.NewGuid();

        var gameToAdd = new GameModelDto() { Id = gameId, Name = "Digital Combat Simulator", Key = string.Empty, Description = "Flight sim", Platforms = BllHelpers.PlatformModelDtos, Genres = BllHelpers.GenreModelDtos };
        var gameToAddWrapper = new GameDtoWrapper() { Game = gameToAdd };

        _unitOfWork.Setup(x => x.GameRepository.AddAsync(It.IsAny<Game>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await gameService.AddGameAsync(gameToAddWrapper);
        });
    }

    [Fact]
    public async Task DeleteGameThrowsWhenNoGameWithGivenId()
    {
        // Arrange
        var gameToDelete = BllHelpers.Games[0];
        _unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        _unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        _unitOfWork.Setup(x => x.GameRepository.Delete(It.IsAny<Game>()));
        _unitOfWork.Setup(x => x.GameRepository.GetByOrderIdAsync(It.IsAny<Guid>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Assert
        await Assert.ThrowsAsync<GamestoreException>(async () =>
        {
            await gameService.DeleteGameByKeyAsync(gameToDelete.Key);
        });
    }

    [Fact]
    public async Task UpdateGameThrowsWhenInvalidName()
    {
        // Arrange
        var gameToUpdate = BllHelpers.GameModelDtos[0];
        gameToUpdate.Name = string.Empty;
        gameToUpdate.Key = "NFS";
        gameToUpdate.Description = "Old racing game";
        var gameToUpdateWrapper = new GameDtoWrapper() { Game = gameToUpdate };

        _unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GameGenres.Where(x => x.GameId == gameToUpdate.Id).ToList());
        _unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GamePlatforms.Where(x => x.GameId == gameToUpdate.Id).ToList());
        _unitOfWork.Setup(x => x.GameRepository.GetByOrderIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First);
        _unitOfWork.Setup(x => x.GameRepository.UpdateAsync(It.IsAny<Game>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await gameService.UpdateGameAsync(gameToUpdateWrapper);
        });
    }

    [Fact]
    public async Task UpdateGameThrowsWhenInvalidKey()
    {
        // Arrange
        var gameToUpdate = BllHelpers.GameModelDtos[0];
        gameToUpdate.Name = "Need For Speed";
        gameToUpdate.Key = string.Empty;
        gameToUpdate.Description = "Old racing game";
        var gameToUpdateWrapper = new GameDtoWrapper() { Game = gameToUpdate };

        _unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GameGenres.Where(x => x.GameId == gameToUpdate.Id).ToList());
        _unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GamePlatforms.Where(x => x.GameId == gameToUpdate.Id).ToList());
        _unitOfWork.Setup(x => x.GameRepository.GetByOrderIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First);
        _unitOfWork.Setup(x => x.GameRepository.UpdateAsync(It.IsAny<Game>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await gameService.UpdateGameAsync(gameToUpdateWrapper);
        });
    }

    [Fact]
    public async Task GetGameByIdAsyncThrowsWhenNoGameWithGivenId()
    {
        // Arrange
        var expectedId = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63");

        _unitOfWork.Setup(x => x.GameRepository.GetByOrderIdAsync(It.IsAny<Guid>()));
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetByIdAsync(It.IsAny<int>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Assert
        await Assert.ThrowsAsync<GamestoreException>(async () =>
        {
            _ = await gameService.GetGameByIdAsync(expectedId);
        });
    }

    [Fact]
    public async Task GetGameByKeyAsyncThrowsWhenNoGameWithGivenKey()
    {
        // Arrange
        var expectedKey = "BG";

        _unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>()));
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetByNameAsync(It.IsAny<string>()));
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetByIdAsync(It.IsAny<int>()));
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetByNameAsync(It.IsAny<string>()));
        var gameService = new GameService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _mongoLoggingService.Object, _gameProcessingPipelineDirector);

        // Assert
        await Assert.ThrowsAsync<GamestoreException>(async () =>
        {
            _ = await gameService.GetGameByKeyAsync(expectedKey);
        });
    }

    private static void SetupUnitOfWorkForFilterTests(Mock<IUnitOfWork> unitOfWork, Mock<IMongoUnitOfWork> mongoUnitOfWork, bool canSeeDeletedGames, List<Game> games = null)
    {
        if (games is null)
        {
            unitOfWork.Setup(x => x.GenreRepository.GetGamesByGenreAsync(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(BllHelpers.GetGamesByGenreAsync(id)));
            unitOfWork.Setup(x => x.PlatformRepository.GetGamesByPlatformAsync(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(BllHelpers.GetGamesByPlatformAsync(id)));
            unitOfWork.Setup(x => x.PublisherRepository.GetGamesByPublisherIdAsync(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(BllHelpers.GetGamesByPublisherAsync(id)));
            unitOfWork.Setup(x => x.GameRepository.GetGamesAsQueryable()).Returns(BllHelpers.Games.Where(x => !x.IsDeleted).AsQueryable());
            unitOfWork.Setup(x => x.GameRepository.GetGamesWithDeletedAsQueryable()).Returns(BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).AsQueryable());
            unitOfWork.Setup(x => x.GameRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Games.Where(x => x.IsDeleted == canSeeDeletedGames).ToList());
        }
        else
        {
            unitOfWork.Setup(x => x.GenreRepository.GetGamesByGenreAsync(It.IsAny<Guid>())).ReturnsAsync(games);
            unitOfWork.Setup(x => x.PlatformRepository.GetGamesByPlatformAsync(It.IsAny<Guid>())).ReturnsAsync(games);
            unitOfWork.Setup(x => x.PublisherRepository.GetGamesByPublisherIdAsync(It.IsAny<Guid>())).ReturnsAsync(games);
            unitOfWork.Setup(x => x.GameRepository.GetGamesAsQueryable()).Returns(games.Where(x => x.IsDeleted == canSeeDeletedGames).AsQueryable());
            unitOfWork.Setup(x => x.GameRepository.GetGamesWithDeletedAsQueryable()).Returns(games.Where(x => x.IsDeleted == canSeeDeletedGames).AsQueryable());
        }

        unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Genres]);
        unitOfWork.Setup(x => x.PlatformRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Platforms]);
        unitOfWork.Setup(x => x.PublisherRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Publishers]);
        mongoUnitOfWork.Setup(x => x.ProductRepository.GetByNameAsync(It.IsAny<string>()));
        mongoUnitOfWork.Setup(x => x.ProductRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.MongoProducts]);
        mongoUnitOfWork.Setup(x => x.CategoryRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.MongoCategories]);
        mongoUnitOfWork.Setup(x => x.SupplierRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.MongoSuppliers]);
        mongoUnitOfWork.Setup(x => x.SupplierRepository.GetByIdAsync(It.IsAny<int>())).Returns((int id) => Task.FromResult(BllHelpers.GetMongoSupplierById(id)));
        mongoUnitOfWork.Setup(x => x.CategoryRepository.GetById(It.IsAny<int>())).Returns((int id) => Task.FromResult(BllHelpers.GetMongoCategoryById(id)));
    }
}
