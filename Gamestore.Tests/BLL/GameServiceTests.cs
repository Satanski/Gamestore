using FluentValidation;
using Gamestore.BLL.Exceptions;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Services;
using Gamestore.Tests.Helpers;
using Moq;

namespace Gamestore.Tests.BLL;

public class GameServiceTests
{
    [Fact]
    public async Task GetAllGamesAsyncShouldReturnAllGames()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var expected = BllHelpers.GameModels.ToList();

        unitOfWork.Setup(x => x.GameRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Games.ToList());
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Act
        var actual = await gameService.GetAllGamesAsync();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetGameByIdAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        Guid expectedId = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63");
        var expected = BllHelpers.GameModels.First(x => x.Id == expectedId);

        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First(x => x.Id == expectedId));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Act
        var actual = await gameService.GetGameByIdAsync(expectedId);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetGameByKeyAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        string expectedKey = "BG";
        var expected = BllHelpers.GameModels.First(x => x.Key == expectedKey);

        unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>())).ReturnsAsync(BllHelpers.Games.First(x => x.Key == expectedKey));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Act
        var actual = await gameService.GetGameByKeyAsync(expectedKey);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddGameAsyncShouldAddGame()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        Guid gameId = Guid.NewGuid();

        var gameToAdd = new GameModelDto() { Id = gameId, Name = "Digital Combat Simulator", Key = "DCS", Description = "Flight sim", GamePlatforms = BllHelpers.PlatformModelDtos, GameGenres = BllHelpers.GenreModelDtos };

        unitOfWork.Setup(x => x.GameRepository.AddAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Act
        await gameService.AddGameAsync(gameToAdd);

        // Assert
        unitOfWork.Verify(x => x.GameRepository.AddAsync(It.Is<Game>(x => x.Id == gameToAdd.Id && x.Name == gameToAdd.Name && x.Key == gameToAdd.Key && x.Description == gameToAdd.Description)));
        unitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteGameAsyncShouldDeleteGame()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameToDelete = BllHelpers.GameModels.First();
        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        unitOfWork.Setup(x => x.GameRepository.Delete(It.IsAny<Game>()));
        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First(x => x.Id == gameToDelete.Id));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Act
        await gameService.DeleteGameAsync(gameToDelete.Id);

        // Assert
        unitOfWork.Verify(x => x.GameRepository.Delete(It.Is<Game>(x => x.Id == gameToDelete.Id && x.Name == gameToDelete.Name && x.Key == gameToDelete.Key && x.Description == gameToDelete.Description)));
        unitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateGameAsyncShouldUpdateGame()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameToUpdate = BllHelpers.GameModelDtos.First();
        gameToUpdate.Name = "Need For Speed";
        gameToUpdate.Key = "NFS";
        gameToUpdate.Description = "Old racing game";

        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GameGenres.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GamePlatforms.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First);
        unitOfWork.Setup(x => x.GameRepository.UpdateAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Act
        await gameService.UpdateGameAsync(gameToUpdate);

        // Assert
        unitOfWork.Verify(x => x.GameRepository.UpdateAsync(It.Is<Game>(x => x.Id == gameToUpdate.Id && x.Name == gameToUpdate.Name && x.Key == gameToUpdate.Key && x.Description == gameToUpdate.Description)));
        unitOfWork.Verify(x => x.SaveAsync(), Times.Between(2, 2, Moq.Range.Inclusive));
    }

    [Fact]
    public async Task GetGenresByGameAsyncShouldReturnGenres()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var game = BllHelpers.GameModelDtos.First();
        var genreId = game.GameGenres[0].Id;

        unitOfWork.Setup(x => x.GameRepository.GetGenresByGameAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Genres);
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Act
        var resultGenres = await gameService.GetGenresByGameAsync(game.Id);

        // Assert
        Assert.Equal(genreId, resultGenres.ToList()[0].Id);
    }

    [Fact]
    public async Task GetPlatformsByGameAsyncShouldReturnPlatforms()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var game = BllHelpers.GameModelDtos.First();
        var platformId = game.GamePlatforms[0].Id;

        unitOfWork.Setup(x => x.GameRepository.GetPlatformsByGameAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Platforms);
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Act
        var resultPlatforms = await gameService.GetPlatformsByGameAsync(game.Id);

        // Assert
        Assert.Equal(platformId, resultPlatforms.ToList()[0].Id);
    }

    [Fact]
    public async Task AddGameAsyncThrowsWhenIvalidName()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        Guid gameId = Guid.NewGuid();

        var gameToAdd = new GameModelDto() { Id = gameId, Name = string.Empty, Key = "DCS", Description = "Flight sim", GamePlatforms = BllHelpers.PlatformModelDtos, GameGenres = BllHelpers.GenreModelDtos };

        unitOfWork.Setup(x => x.GameRepository.AddAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await gameService.AddGameAsync(gameToAdd);
        });
    }

    [Fact]
    public async Task AddGameAsyncThrowsWhenIvalidKey()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        Guid gameId = Guid.NewGuid();

        var gameToAdd = new GameModelDto() { Id = gameId, Name = "Digital Combat Simulator", Key = string.Empty, Description = "Flight sim", GamePlatforms = BllHelpers.PlatformModelDtos, GameGenres = BllHelpers.GenreModelDtos };

        unitOfWork.Setup(x => x.GameRepository.AddAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await gameService.AddGameAsync(gameToAdd);
        });
    }

    [Fact]
    public async Task DeleteGameThrowsWhenNoGameWithGivenId()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameToDelete = BllHelpers.GameModels.First();
        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>()));
        unitOfWork.Setup(x => x.GameRepository.Delete(It.IsAny<Game>()));
        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Assert
        await Assert.ThrowsAsync<GamestoreException>(async () =>
        {
            await gameService.DeleteGameAsync(gameToDelete.Id);
        });
    }

    [Fact]
    public async Task UpdateGameThrowsWhenInvalidName()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameToUpdate = BllHelpers.GameModelDtos.First();
        gameToUpdate.Name = string.Empty;
        gameToUpdate.Key = "NFS";
        gameToUpdate.Description = "Old racing game";

        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GameGenres.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GamePlatforms.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First);
        unitOfWork.Setup(x => x.GameRepository.UpdateAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await gameService.UpdateGameAsync(gameToUpdate);
        });
    }

    [Fact]
    public async Task UpdateGameThrowsWhenInvalidKey()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var gameToUpdate = BllHelpers.GameModelDtos.First();
        gameToUpdate.Name = "Need For Speed";
        gameToUpdate.Key = string.Empty;
        gameToUpdate.Description = "Old racing game";

        unitOfWork.Setup(x => x.GameGenreRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GameGenres.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GamePlatformRepository.GetByGameIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.GamePlatforms.Where(x => x.GameId == gameToUpdate.Id).ToList());
        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Games.First);
        unitOfWork.Setup(x => x.GameRepository.UpdateAsync(It.IsAny<Game>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await gameService.UpdateGameAsync(gameToUpdate);
        });
    }

    [Fact]
    public async Task GetGameByIdAsyncThrowsWhenNoGameWithGivenId()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        Guid expectedId = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63");

        unitOfWork.Setup(x => x.GameRepository.GetByIdAsync(It.IsAny<Guid>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

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
        string expectedKey = "BG";

        unitOfWork.Setup(x => x.GameRepository.GetGameByKeyAsync(It.IsAny<string>()));
        var gameService = new GameService(unitOfWork.Object, BllHelpers.CreateMapperProfile());

        // Assert
        await Assert.ThrowsAsync<GamestoreException>(async () =>
        {
            _ = await gameService.GetGameByKeyAsync(expectedKey);
        });
    }
}
