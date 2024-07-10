using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
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

    public GenreServiceTests()
    {
        _logger = new Mock<ILogger<GenreService>>();
    }

    [Fact]
    public async Task GetAllGenresAsyncShouldReturnAllGenres()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var expected = BllHelpers.GenreModelDtos.ToList();

        unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync([.. BllHelpers.Genres]);
        var genreService = new GenreService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        var actual = await genreService.GetAllGenresAsync();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetGenreByIdAsyncShouldReturnCorrectGenre()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var expectedName = "Rpg";
        var expectedId = BllHelpers.Genres.First(x => x.Name == expectedName).Id;
        var expected = BllHelpers.GenreModelDtos.First(x => x.Name == expectedName);

        unitOfWork.Setup(x => x.GenreRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Genres.First(x => x.Id == expectedId));
        var genreService = new GenreService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        var actual = await genreService.GetGenreByIdAsync(expectedId);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddGenreAsyncShouldAddGenre()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var expectedName = "NewGenre";
        var genreDtoWrapper = new GenreDtoWrapper
        {
            Genre = new GenreModelDto() { Name = expectedName },
        };
        var genreToAdd = genreDtoWrapper;
        unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Genres);
        unitOfWork.Setup(x => x.GenreRepository.AddAsync(It.IsAny<Genre>()));
        var genreService = new GenreService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        await genreService.AddGenreAsync(genreToAdd);

        // Assert
        unitOfWork.Verify(x => x.GenreRepository.AddAsync(It.Is<Genre>(x => x.Name == expectedName)));
        unitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteGenreShouldDeleteGenre()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var genreToDelete = BllHelpers.Genres[0];
        var genreToDeleteId = genreToDelete.Id;

        unitOfWork.Setup(x => x.GenreRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(genreToDelete);
        unitOfWork.Setup(x => x.GenreRepository.GetGenresByParentGenreAsync(It.IsAny<Guid>())).ReturnsAsync([]);
        unitOfWork.Setup(x => x.GenreRepository.Delete(It.IsAny<Genre>()));
        var genreService = new GenreService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        await genreService.DeleteGenreAsync(genreToDeleteId);

        // Assert
        unitOfWork.Verify(x => x.GenreRepository.Delete(It.Is<Genre>(x => x.Id == genreToDeleteId)));
        unitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateGenreShouldUpdateGenre()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var genreToUpdate = BllHelpers.GenreModelDtos[0];
        var genreToUpdateId = genreToUpdate.Id;
        genreToUpdate.Name = "New Name";
        var genreDtoWrapper = new GenreDtoWrapper
        {
            Genre = genreToUpdate,
        };
        unitOfWork.Setup(x => x.GenreRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Genres.Find(x => x.Id == genreToUpdateId));
        unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Genres);
        unitOfWork.Setup(x => x.GenreRepository.UpdateAsync(It.IsAny<Genre>()));
        var genreService = new GenreService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        await genreService.UpdateGenreAsync(genreDtoWrapper);

        // Assert
        unitOfWork.Verify(x => x.GenreRepository.UpdateAsync(It.Is<Genre>(x => x.Name == genreToUpdate.Name && x.Id == genreToUpdate.Id)));
        unitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task GetGamesByGenreAsyncShouldReturnGames()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(x => x.GenreRepository.GetGamesByGenreAsync(It.IsAny<Guid>())).ReturnsAsync([.. BllHelpers.Games]);
        var genreService = new GenreService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Act
        var actualGames = await genreService.GetGamesByGenreAsync(Guid.NewGuid());

        // Assert
        Assert.Equal(actualGames.Count(), BllHelpers.GameModelDtos.Count);
    }

    [Fact]
    public async Task AddGenreAsyncThrowsWhenInvalidModel()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var expectedName = string.Empty;
        var genreDtoWrapper = new GenreDtoWrapper
        {
            Genre = new GenreModelDto() { Name = expectedName },
        };
        var genreToAdd = genreDtoWrapper;
        unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Genres);
        unitOfWork.Setup(x => x.GenreRepository.AddAsync(It.IsAny<Genre>()));
        var genreService = new GenreService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

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
        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(x => x.GenreRepository.GetByIdAsync(It.IsAny<Guid>()));
        unitOfWork.Setup(x => x.GenreRepository.GetGenresByParentGenreAsync(It.IsAny<Guid>())).ReturnsAsync([]);
        unitOfWork.Setup(x => x.GenreRepository.Delete(It.IsAny<Genre>()));
        var genreService = new GenreService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

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
        var unitOfWork = new Mock<IUnitOfWork>();
        var expectedId = Guid.Empty;

        unitOfWork.Setup(x => x.GenreRepository.GetByIdAsync(It.IsAny<Guid>()));
        var genreService = new GenreService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

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
        var unitOfWork = new Mock<IUnitOfWork>();
        var genreToUpdate = BllHelpers.GenreModelDtos[0];
        var genreToUpdateId = genreToUpdate.Id;
        genreToUpdate.Name = string.Empty;
        var genreDtoWrapper = new GenreDtoWrapper
        {
            Genre = genreToUpdate,
        };

        unitOfWork.Setup(x => x.GenreRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(BllHelpers.Genres.Find(x => x.Id == genreToUpdateId));
        unitOfWork.Setup(x => x.GenreRepository.GetAllAsync()).ReturnsAsync(BllHelpers.Genres);
        unitOfWork.Setup(x => x.GenreRepository.UpdateAsync(It.IsAny<Genre>()));
        var genreService = new GenreService(unitOfWork.Object, BllHelpers.CreateMapperProfile(), _logger.Object);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await genreService.UpdateGenreAsync(genreDtoWrapper);
        });
    }
}
