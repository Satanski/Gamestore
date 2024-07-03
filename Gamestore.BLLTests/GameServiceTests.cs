using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering;
using Gamestore.BLL.Filtering.Handlers;
using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Services;
using Gamestore.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Gamestore.Tests.BLL;

public class GameServiceTests
{
    private readonly Mock<ILogger<GameService>> _logger;
    private readonly GameProcessingPipelineDirector _gameProcessingPipelineDirector;

    public GameServiceTests()
    {
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
    [InlineData("Rpg", "Desktop", "Blizzard")]
    [InlineData("Racing", "Mobile", "Activision")]
    [InlineData("Simulator", "Console", "BioWare")]
    public async Task GetFilteredGamesAsyncShouldreturnOneGameForGenrePlatformPublisherCombination(string genreName, string platformType, string publisherCompanyName)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        GameFiltersDto filters = new GameFiltersDto
        {
            Genres = [BllHelpers.Genres.Find(x => x.Name == genreName).Id],
            Platforms = [BllHelpers.Platforms.Find(x => x.Type == platformType).Id],
            Publishers = [BllHelpers.Publishers.Find(x => x.CompanyName == publisherCompanyName).Id],
        };

        SetupUnitOfWorkForFilterTests(unitOfWork);

        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters);

        Assert.Single(result.Games);
    }

    [Theory]
    [InlineData("Rpg", "Mobile", "BioWare")]
    [InlineData("Racing", "Desktop", "BioWare")]
    [InlineData("Simulator", "Mobile", "Blizzard")]
    public async Task GetFilteredGamesAsyncShouldreturnThreeGamesForGenrePlatformPublisherCombination(string genreName, string platformType, string publisherCompanyName)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        GameFiltersDto filters = new GameFiltersDto
        {
            Genres = [BllHelpers.Genres.Find(x => x.Name == genreName).Id],
            Platforms = [BllHelpers.Platforms.Find(x => x.Type == platformType).Id],
            Publishers = [BllHelpers.Publishers.Find(x => x.CompanyName == publisherCompanyName).Id],
        };

        SetupUnitOfWorkForFilterTests(unitOfWork);

        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters);

        Assert.Equal(3, result.Games.Count);
    }

    [Theory]
    [InlineData(0, 100)]
    [InlineData(101, 200)]
    [InlineData(201, 300)]
    public async Task GetFilteredGamesAsyncShouldreturnOneGameForPriceRange(int minPriace, int maxPrice)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        GameFiltersDto filters = new GameFiltersDto
        {
            MinPrice = minPriace,
            MaxPrice = maxPrice,
        };

        SetupUnitOfWorkForFilterTests(unitOfWork);

        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters);

        Assert.Single(result.Games);
    }

    [Theory]
    [InlineData(300)]
    public async Task GetFilteredGamesAsyncShouldreturnThreeGamesWithoutMinimalPrice(int maxPrice)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        GameFiltersDto filters = new GameFiltersDto
        {
            MaxPrice = maxPrice,
        };

        SetupUnitOfWorkForFilterTests(unitOfWork);

        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters);

        Assert.Equal(3, result.Games.Count);
    }

    [Theory]
    [InlineData(300)]
    public async Task GetFilteredGamesAsyncShouldreturnOneGameWithoutMaximalPrice(int minPrice)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        GameFiltersDto filters = new GameFiltersDto
        {
            MinPrice = minPrice,
        };

        SetupUnitOfWorkForFilterTests(unitOfWork);

        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters);

        Assert.Single(result.Games);
    }

    [Theory]
    [InlineData("Baldurs")]
    [InlineData("Drive")]
    [InlineData("mba")]
    public async Task GetFilteredGamesAsyncShouldreturnOneGameForNameFilter(string name)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        GameFiltersDto filters = new GameFiltersDto
        {
            Name = name,
        };

        SetupUnitOfWorkForFilterTests(unitOfWork);

        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters);

        Assert.Single(result.Games);
    }

    [Theory]
    [InlineData("Last week")]
    [InlineData("Last month")]
    [InlineData("Last year")]
    [InlineData("2 years")]
    [InlineData("3 years")]
    public async Task GetFilteredGamesAsyncShouldreturnCorrectGamesForPublishDate(string publishDate)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        GameFiltersDto filters = new GameFiltersDto
        {
            DatePublishing = publishDate,
        };

        int expectedNumberOfGames = 0;
        switch (publishDate)
        {
            case "Last week":
                expectedNumberOfGames = BllHelpers.Games.Count(x => x.PublishDate >= DateOnly.FromDateTime(DateTime.Now).AddDays(-7));
                break;
            case "Last month":
                expectedNumberOfGames = BllHelpers.Games.Count(x => x.PublishDate >= DateOnly.FromDateTime(DateTime.Now).AddMonths(-1));
                break;
            case "Last year":
                expectedNumberOfGames = BllHelpers.Games.Count(x => x.PublishDate >= DateOnly.FromDateTime(DateTime.Now).AddYears(-1));
                break;
            case "2 years":
                expectedNumberOfGames = BllHelpers.Games.Count(x => x.PublishDate >= DateOnly.FromDateTime(DateTime.Now).AddYears(-2));
                break;
            case "3 years":
                expectedNumberOfGames = BllHelpers.Games.Count(x => x.PublishDate >= DateOnly.FromDateTime(DateTime.Now).AddYears(-3));
                break;
            default:
                break;
        }

        SetupUnitOfWorkForFilterTests(unitOfWork);

        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters);

        Assert.Equal(expectedNumberOfGames, result.Games.Count);
    }

    [Theory]
    [InlineData("Most popular")]
    [InlineData("Most commented")]
    [InlineData("Price ASC")]
    [InlineData("Price DESC")]
    [InlineData("New")]
    public async Task GetFilteredGamesAsyncShouldreturnGamesInCorrectOrderForsortingOption(string sortingOption)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        GameFiltersDto filters = new GameFiltersDto
        {
            Sort = sortingOption,
        };

        List<Game> expectedGames = [];
        switch (sortingOption)
        {
            case "Most popular":
                expectedGames = [.. BllHelpers.Games.OrderByDescending(x => x.NumberOfViews)];
                break;
            case "Most commented":
                expectedGames = [.. BllHelpers.Games.OrderByDescending(x => x.Comments.Count)];
                break;
            case "Price ASC":
                expectedGames = [.. BllHelpers.Games.OrderBy(x => x.Price)];
                break;
            case "Price DESC":
                expectedGames = [.. BllHelpers.Games.OrderByDescending(x => x.Price)];
                break;
            case "New":
                expectedGames = [.. BllHelpers.Games.OrderByDescending(x => x.PublishDate)];
                break;
            default:
                break;
        }

        SetupUnitOfWorkForFilterTests(unitOfWork);

        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters);

        for (var i = 0; i < result.Games.Count; i++)
        {
            Assert.Equal(expectedGames[i].Id, result.Games[i].Id);
        }
    }

    [Theory]
    [InlineData("10")]
    [InlineData("20")]
    [InlineData("50")]
    [InlineData("100")]
    [InlineData("all")]
    public async Task GetFilteredGamesAsyncShouldReturnCorrectNumberOfPages(string noOfGamesPerPage)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        GameFiltersDto filters = new GameFiltersDto
        {
            PageCount = noOfGamesPerPage,
        };

        List<Game> games = [];
        for (int i = 0; i < 100; i++)
        {
            games.Add(new Game() { Id = Guid.NewGuid(), Name = i.ToString(), Key = i.ToString() });
        }

        int expectedNumberOfPages = 0;
        int expectedNumberOfGamesPerPage = 0;
        switch (noOfGamesPerPage)
        {
            case "10":
                expectedNumberOfPages = 10;
                expectedNumberOfGamesPerPage = 10;
                break;
            case "20":
                expectedNumberOfPages = 5;
                expectedNumberOfGamesPerPage = 20;
                break;
            case "50":
                expectedNumberOfPages = 2;
                expectedNumberOfGamesPerPage = 50;
                break;
            case "100":
                expectedNumberOfPages = 1;
                expectedNumberOfGamesPerPage = 100;
                break;
            case "all":
                expectedNumberOfPages = 1;
                expectedNumberOfGamesPerPage = 100;
                break;
            default:
                break;
        }

        SetupUnitOfWorkForFilterTests(unitOfWork, games);

        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);
        var result = await gameService.GetFilteredGamesAsync(filters);

        Assert.Equal(expectedNumberOfPages, filters.NumberOfPagesAfterFiltration);
        Assert.Equal(expectedNumberOfGamesPerPage, result.Games.Count);
    }

    [Fact]
    public async Task GetAllGamesAsyncShouldReturnAllGames()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var expected = BllHelpers.Games.ToList();

        unitOfWork.Setup(x => x.GameRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Games]);
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

        // Act
        var actual = await gameService.GetAllGamesAsync();

        // Assert
        Assert.Equal(expected.Count, actual.Count());
    }

    [Fact]
    public async Task GetGameByIdAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var expectedId = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63");
        var expected = BllHelpers.Games.First(x => x.Id == expectedId);

        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First(x => x.Id == expectedId));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

        // Act
        var actual = await gameService.GetGameByIdAsync(expectedId);

        // Assert
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Key, actual.Key);
        Assert.Equal(expected.Price, actual.Price);
    }

    [Fact]
    public async Task GetGameByKeyAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var expectedKey = "BG";
        var expected = BllHelpers.Games.First(x => x.Key == expectedKey);

        unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>())).ReturnsAsync(BllHelpers.Games.First(x => x.Key == expectedKey));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

        // Act
        var actual = await gameService.GetGameByKeyAsync(expectedKey);

        // Assert
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Key, actual.Key);
        Assert.Equal(expected.Price, actual.Price);
    }

    [Fact]
    public async Task AddGameAsyncShouldAddGame()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
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

        unitOfWork.Setup(x => x.GameRepository.AddAsync(It.IsAny<Game>())).ReturnsAsync(new Game());
        unitOfWork.Setup(x => x.GameRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Games]);
        unitOfWork.Setup(x => x.GameGenreRepository.AddAsync(It.IsAny<GameGenre>()));
        unitOfWork.Setup(x => x.GamePlatformRepository.AddAsync(It.IsAny<GamePlatform>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

        // Act
        await gameService.AddGameAsync(gameToAddWrapper);

        // Assert
        unitOfWork.Verify(x => x.GameRepository.AddAsync(It.Is<Game>(x => x.Id == gameToAdd.Id && x.Name == gameToAdd.Name
        && x.Key == gameToAdd.Key && x.Description == gameToAdd.Description && x.PublisherId == gameToAdd.Publisher.Id
        && x.Price == gameToAdd.Price && x.UnitInStock == gameToAdd.UnitInStock && x.Discount == gameToAdd.Discontinued)));
        unitOfWork.Verify(x => x.SaveAsync(), Times.Exactly(3));
    }

    [Fact]
    public async Task DeleteGameAsyncShouldDeleteGame()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameToDelete = BllHelpers.Games[0];
        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        unitOfWork.Setup(x => x.GameRepository.Delete(It.IsAny<Game>()));
        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First(x => x.Id == gameToDelete.Id));
        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync([]);
        unitOfWork.Setup(x => x.GameGenreRepository.Delete(It.IsAny<GameGenre>()));
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync([]);
        unitOfWork.Setup(x => x.GamePlatformRepository.Delete(It.IsAny<GamePlatform>()));
        unitOfWork.Setup(x => x.OrderGameRepository.GetAllAsync()).ReturnsAsync([]);
        unitOfWork.Setup(x => x.OrderGameRepository.Delete(It.IsAny<OrderGame>()));

        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

        // Act
        await gameService.DeleteGameByIdAsync(gameToDelete.Id);

        // Assert
        unitOfWork.Verify(x => x.GameRepository.Delete(It.Is<Game>(x => x.Id == gameToDelete.Id && x.Name == gameToDelete.Name && x.Key == gameToDelete.Key && x.Description == gameToDelete.Description)));
        unitOfWork.Verify(x => x.SaveAsync(), Times.Exactly(3));
    }

    [Fact]
    public async Task UpdateGameAsyncShouldUpdateGame()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
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

        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GameGenres.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GamePlatforms.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First);
        unitOfWork.Setup(x => x.GameRepository.UpdateAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

        // Act
        await gameService.UpdateGameAsync(gameToUpdateWrapper);

        // Assert
        unitOfWork.Verify(x => x.GameRepository.UpdateAsync(It.Is<Game>(x => x.Id == gameToUpdate.Id && x.Name == gameToUpdate.Name
            && x.Key == gameToUpdate.Key && x.Description == gameToUpdate.Description && x.PublisherId == gameToUpdate.Publisher.Id
            && x.Price == gameToUpdate.Price && x.UnitInStock == gameToUpdate.UnitInStock && x.Discount == gameToUpdate.Discontinued)));
        unitOfWork.Verify(x => x.SaveAsync(), Times.Exactly(6));
    }

    [Fact]
    public async Task GetGenresByGameAsyncShouldReturnGenres()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var game = BllHelpers.Games[0];
        var genreId = game.GameGenres[0].GenreId;

        unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>())).ReturnsAsync(game);
        unitOfWork.Setup(x => x.GameRepository.GetGenresByGameAsync(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(BllHelpers.GetGenresByGameAsync(id)));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

        // Act
        var resultGenres = await gameService.GetGenresByGameKeyAsync(game.Key);

        // Assert
        Assert.Equal(genreId, resultGenres.ToList()[0].Id);
    }

    [Fact]
    public async Task GetPlatformsByGameAsyncShouldReturnPlatforms()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var game = BllHelpers.Games[0];
        var platformId = game.GamePlatforms[0].PlatformId;

        unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>())).ReturnsAsync(game);
        unitOfWork.Setup(x => x.GameRepository.GetPlatformsByGameAsync(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(BllHelpers.GetPlatformsByGameAsync(id)));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

        // Act
        var resultPlatforms = await gameService.GetPlatformsByGameKeyAsync(game.Key);

        // Assert
        Assert.Equal(platformId, resultPlatforms.ToList()[0].Id);
    }

    [Fact]
    public async Task AddGameAsyncThrowsWhenIvalidName()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameId = Guid.NewGuid();

        var gameToAdd = new GameModelDto() { Id = gameId, Name = string.Empty, Key = "DCS", Description = "Flight sim", Platforms = BllHelpers.PlatformModelDtos, Genres = BllHelpers.GenreModelDtos };
        var gameToAddWrapper = new GameDtoWrapper() { Game = gameToAdd };

        unitOfWork.Setup(x => x.GameRepository.AddAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

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
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameId = Guid.NewGuid();

        var gameToAdd = new GameModelDto() { Id = gameId, Name = "Digital Combat Simulator", Key = string.Empty, Description = "Flight sim", Platforms = BllHelpers.PlatformModelDtos, Genres = BllHelpers.GenreModelDtos };
        var gameToAddWrapper = new GameDtoWrapper() { Game = gameToAdd };

        unitOfWork.Setup(x => x.GameRepository.AddAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

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
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameToDelete = BllHelpers.Games[0];
        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        unitOfWork.Setup(x => x.GameRepository.Delete(It.IsAny<Game>()));
        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

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
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameToUpdate = BllHelpers.GameModelDtos[0];
        gameToUpdate.Name = string.Empty;
        gameToUpdate.Key = "NFS";
        gameToUpdate.Description = "Old racing game";
        var gameToUpdateWrapper = new GameDtoWrapper() { Game = gameToUpdate };

        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GameGenres.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GamePlatforms.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First);
        unitOfWork.Setup(x => x.GameRepository.UpdateAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

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
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameToUpdate = BllHelpers.GameModelDtos[0];
        gameToUpdate.Name = "Need For Speed";
        gameToUpdate.Key = string.Empty;
        gameToUpdate.Description = "Old racing game";
        var gameToUpdateWrapper = new GameDtoWrapper() { Game = gameToUpdate };

        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GameGenres.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GamePlatforms.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First);
        unitOfWork.Setup(x => x.GameRepository.UpdateAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

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
        var unitOfWork = new Mock<IUnitOfWork>();
        var expectedId = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63");

        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

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
        var unitOfWork = new Mock<IUnitOfWork>();
        var expectedKey = "BG";

        unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object, _gameProcessingPipelineDirector);

        // Assert
        await Assert.ThrowsAsync<GamestoreException>(async () =>
        {
            _ = await gameService.GetGameByKeyAsync(expectedKey);
        });
    }

    private static void SetupUnitOfWorkForFilterTests(Mock<IUnitOfWork> unitOfWork, List<Game> games = null)
    {
        if (games is null)
        {
            unitOfWork.Setup(x => x.GenreRepository.GetGamesByGenreAsync(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(BllHelpers.GetGamesByGenreAsync(id)));
            unitOfWork.Setup(x => x.PlatformRepository.GetGamesByPlatformAsync(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(BllHelpers.GetGamesByPlatformAsync(id)));
            unitOfWork.Setup(x => x.PublisherRepository.GetGamesByPublisherIdAsync(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(BllHelpers.GetGamesByPublisherAsync(id)));
        }
        else
        {
            unitOfWork.Setup(x => x.GenreRepository.GetGamesByGenreAsync(It.IsAny<Guid>())).ReturnsAsync(games);
            unitOfWork.Setup(x => x.PlatformRepository.GetGamesByPlatformAsync(It.IsAny<Guid>())).ReturnsAsync(games);
            unitOfWork.Setup(x => x.PublisherRepository.GetGamesByPublisherIdAsync(It.IsAny<Guid>())).ReturnsAsync(games);
        }

        unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Genres]);
        unitOfWork.Setup(x => x.PlatformRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Platforms]);
        unitOfWork.Setup(x => x.PublisherRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Publishers]);
    }
}
