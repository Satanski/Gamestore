using Gamestore.DAL.Entities;
using Gamestore.DAL.Repositories;
using Gamestore.Tests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DALTests;

public class GenreRepositoryTests : IDisposable
{
    private readonly GamestoreContext _context;
    private readonly GenreRepository _genreRepository;
    private bool _disposedValue;

    public GenreRepositoryTests()
    {
        var options = new DbContextOptionsBuilder().UseInMemoryDatabase("GenreRepoTest").Options;

        _context = new GamestoreContext(options);
        _genreRepository = new(_context);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        ContextHelpers.ClearContext(_context);
        ContextHelpers.SeedGenres(_context);
        ContextHelpers.SeedPlatforms(_context);
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllGenres()
    {
        // Arrange
        var expectedGenres = _context.Genres;

        // Act
        var actualGenres = await _genreRepository.GetAllAsync();

        // Assert
        Assert.True(actualGenres.Count != 0);
        Assert.Equal(expectedGenres.Count(), actualGenres.Count);
    }

    [Fact]
    public async Task GetByIdAsyncShoudReturnCorrectGenre()
    {
        // Arrange
        var expectedGenre = _context.Genres.First();
        var expectedGenreId = expectedGenre.Id;

        // Act
        var actualGenre = await _genreRepository.GetByIdAsync(expectedGenreId);

        // Assert
        Assert.Equal(expectedGenre, actualGenre);
    }

    [Fact]
    public async Task GetGamesByGenreAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var id = Guid.NewGuid();
        var gameGenre = _context.Genres.First(x => x.Name == "RTS");
        await TestGameHelpers.AddTestGameAsync(_context, id, "Baldurs Gate", "BG", "Rpg game", gameGenre);

        var expectedGameId = Guid.NewGuid();
        var expectedGenre = _context.Genres.First(x => x.Name == "Races");
        var expectedGenreId = expectedGenre.Id;
        await TestGameHelpers.AddTestGameAsync(_context, expectedGameId, "Test Drive", "TD", "Racing game", expectedGenre);

        // Act
        var actualGames = await _genreRepository.GetGamesByGenreAsync(expectedGenreId);
        var actualGenreId = actualGames[0].GameGenres[0].GenreId;

        // Assert
        Assert.Single(actualGames);
        Assert.Equal(expectedGenre.Id, actualGenreId);
    }

    [Fact]
    public async Task GetGenresByParentGenreAsyncShouldReturnCorrectGenres()
    {
        // Arrange
        var parentGenre = _context.Genres.First(x => x.Name == "Races");
        var expectedGenres = _context.Genres.Where(x => x.ParentGenreId == parentGenre.Id);

        // Act
        var actualGenres = await _genreRepository.GetGenresByParentGenreAsync(parentGenre.Id);

        // Assert
        Assert.Equal(expectedGenres.Count(), actualGenres.Count);
    }

    [Fact]
    public async Task AddAsyncShouldAddGenre()
    {
        // Arrange
        var startingGenres = _context.Genres;
        var expectedGenreCount = startingGenres.Count() + 1;

        var expectedGenre = new Genre()
        {
            Id = Guid.NewGuid(),
            Name = "New Genre",
        };

        // Act
        await _genreRepository.AddAsync(expectedGenre);
        await _context.SaveChangesAsync();
        var actualGenres = _context.Genres;

        // Assert
        Assert.Equal(expectedGenreCount, actualGenres.Count());
        Assert.Contains(expectedGenre, actualGenres);
    }

    [Fact]
    public void DeleteShouldDeleteCorrectGenre()
    {
        // Arrange
        var startingGenres = _context.Genres;
        var genreToDelete = _context.Genres.First();
        var expectedGenreCount = startingGenres.Count() - 1;

        // Act
        _genreRepository.Delete(genreToDelete);
        _context.SaveChanges();
        var actualGenres = _context.Genres;

        // Assert
        Assert.Equal(expectedGenreCount, actualGenres.Count());
        Assert.DoesNotContain(genreToDelete, _context.Genres);
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdateGenre()
    {
        // Arrange
        var genreToUpdate = _context.Genres.First();
        var genreToUpdateId = genreToUpdate.Id;
        var expectedName = "New name";
        genreToUpdate.Name = expectedName;

        // Act
        await _genreRepository.UpdateAsync(genreToUpdate);
        var actualGenre = _context.Genres.First(x => x.Id == genreToUpdateId);

        // Assert
        Assert.Equal(expectedName, actualGenre.Name);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposedValue = true;
        }
    }
}
