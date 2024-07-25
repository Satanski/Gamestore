using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Services;
using Gamestore.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Sdk;

namespace Gamestore.Tests.BLL;

public class GenreServiceTests
{
    private readonly Mock<ILogger<GenreService>> _logger;
    private readonly Mock<IMongoUnitOfWork> _mongoUnitOfWork = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    public GenreServiceTests()
    {
        _logger = new Mock<ILogger<GenreService>>();
    }

    [Fact]
    public async Task GetAllGenresAsyncShouldReturnAllGenres()
    {
        // Arrange
        var expected = BllHelpers.GenreModelDtos.ToList();

        _unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Genres]);
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetByNameAsync(It.IsAny<string>()));
        _mongoUnitOfWork.Setup(x => x.CategoryRepository.GetAllAsync()).Returns(Task.FromResult(new List<MongoCategory>()));
        var genreService = new GenreService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        var actual = await genreService.GetAllGenresAsync();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetGenreByIdAsyncShouldReturnCorrectGenre()
    {
        // Arrange
        var expectedName = "Rpg";
        var expectedId = BllHelpers.Genres.First(x => x.Name == expectedName).Id;
        var expected = BllHelpers.GenreModelDtos.First(x => x.Name == expectedName);

        _unitOfWork.Setup(x => x.GenreRepository.GetByOrderIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Genres.First(x => x.Id == expectedId));
        var genreService = new GenreService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        var actual = await genreService.GetGenreByIdAsync(expectedId);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddGenreAsyncShouldAddGenre()
    {
        // Arrange
        var expectedName = "NewGenre";
        var genreDtoWrapper = new GenreDtoWrapper
        {
            Genre = new GenreModelDto() { Name = expectedName },
        };
        var genreToAdd = genreDtoWrapper;
        _unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Genres);
        _unitOfWork.Setup(x => x.GenreRepository.AddAsync(It.IsAny<Genre>()));
        var genreService = new GenreService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        await genreService.AddGenreAsync(genreToAdd);

        // Assert
        _unitOfWork.Verify(x => x.GenreRepository.AddAsync(It.Is<Genre>(x => x.Name == expectedName)));
        _unitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteGenreShouldDeleteGenre()
    {
        // Arrange
        var genreToDelete = BllHelpers.Genres[0];
        var genreToDeleteId = genreToDelete.Id;

        _unitOfWork.Setup(x => x.GenreRepository.GetByOrderIdAsync(It.IsAny<Guid>())).ReturnsAsync(genreToDelete);
        _unitOfWork.Setup(x => x.GenreRepository.GetGenresByParentGenreAsync(It.IsAny<Guid>())).ReturnsAsync([]);
        _unitOfWork.Setup(x => x.GenreRepository.Delete(It.IsAny<Genre>()));
        var genreService = new GenreService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        await genreService.DeleteGenreAsync(genreToDeleteId);

        // Assert
        _unitOfWork.Verify(x => x.GenreRepository.Delete(It.Is<Genre>(x => x.Id == genreToDeleteId)));
        _unitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateGenreShouldUpdateGenre()
    {
        // Arrange
        var genreToUpdate = BllHelpers.GenreModelDtos[0];
        var genreToUpdateId = genreToUpdate.Id;
        genreToUpdate.Name = "New Name";
        var genreDtoWrapper = new GenreDtoWrapper
        {
            Genre = genreToUpdate,
        };
        _unitOfWork.Setup(x => x.GenreRepository.GetByOrderIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Genres.Find(x => x.Id == genreToUpdateId));
        _unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Genres);
        _unitOfWork.Setup(x => x.GenreRepository.UpdateAsync(It.IsAny<Genre>()));
        var genreService = new GenreService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        await genreService.UpdateGenreAsync(genreDtoWrapper);

        // Assert
        _unitOfWork.Verify(x => x.GenreRepository.UpdateAsync(It.Is<Genre>(x => x.Name == genreToUpdate.Name && x.Id == genreToUpdate.Id)));
        _unitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task GetGamesByGenreAsyncShouldReturnGames()
    {
        // Arrange
        _unitOfWork.Setup(x => x.GenreRepository.GetGamesByGenreAsync(It.IsAny<Guid>())).ReturnsAsync([.. BllHelpers.Games]);
        _mongoUnitOfWork.Setup(x => x.CategoryRepository.GetById(It.IsAny<int>()));
        _mongoUnitOfWork.Setup(x => x.ProductRepository.GetByCategoryIdAsync(It.IsAny<int>()));
        var genreService = new GenreService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        var actualGames = await genreService.GetGamesByGenreAsync(Guid.NewGuid());

        // Assert
        Assert.Equal(actualGames.Count(), BllHelpers.GameModelDtos.Count);
    }

    [Fact]
    public async Task AddGenreAsyncThrowsWhenInvalidModel()
    {
        // Arrange
        var expectedName = string.Empty;
        var genreDtoWrapper = new GenreDtoWrapper
        {
            Genre = new GenreModelDto() { Name = expectedName },
        };
        var genreToAdd = genreDtoWrapper;
        _unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Genres);
        _unitOfWork.Setup(x => x.GenreRepository.AddAsync(It.IsAny<Genre>()));
        var genreService = new GenreService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await genreService.AddGenreAsync(genreToAdd);
        });
    }

    [Fact]
    public async Task DeleteGenreThrowsWhenNoGenreWithThisId()
    {
        // Arrange
        _unitOfWork.Setup(x => x.GenreRepository.GetByOrderIdAsync(It.IsAny<Guid>()));
        _unitOfWork.Setup(x => x.GenreRepository.GetGenresByParentGenreAsync(It.IsAny<Guid>())).ReturnsAsync([]);
        _unitOfWork.Setup(x => x.GenreRepository.Delete(It.IsAny<Genre>()));
        var genreService = new GenreService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Assert
        await Assert.ThrowsAsync<GamestoreException>(async () =>
        {
            await genreService.DeleteGenreAsync(Guid.NewGuid());
        });
    }

    [Fact]
    public async Task GetGenreByIdAsyncThrowsWhenNoGenreWithGivenId()
    {
        // Arrange
        var expectedId = Guid.Empty;

        _unitOfWork.Setup(x => x.GenreRepository.GetByOrderIdAsync(It.IsAny<Guid>()));
        _mongoUnitOfWork.Setup(x => x.CategoryRepository.GetAllAsync()).Returns(Task.FromResult(new List<MongoCategory>()));
        var genreService = new GenreService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Assert
        await Assert.ThrowsAsync<GamestoreException>(async () =>
        {
            _ = await genreService.GetGenreByIdAsync(expectedId);
        });
    }

    [Fact]
    public async Task UpdateGenreAsyncThrowsWhenInvalidModel()
    {
        // Arrange
        var genreToUpdate = BllHelpers.GenreModelDtos[0];
        var genreToUpdateId = genreToUpdate.Id;
        genreToUpdate.Name = string.Empty;
        var genreDtoWrapper = new GenreDtoWrapper
        {
            Genre = genreToUpdate,
        };

        _unitOfWork.Setup(x => x.GenreRepository.GetByOrderIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Genres.Find(x => x.Id == genreToUpdateId));
        _unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Genres);
        _unitOfWork.Setup(x => x.GenreRepository.UpdateAsync(It.IsAny<Genre>()));
        var genreService = new GenreService(_unitOfWork.Object, _mongoUnitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await genreService.UpdateGenreAsync(genreDtoWrapper);
        });
    }
}
