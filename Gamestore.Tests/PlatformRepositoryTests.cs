using Gamestore.DAL.Entities;
using Gamestore.DAL.Repositories;
using Gamestore.Tests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DALTests;

public class PlatformRepositoryTests : IDisposable
{
    private readonly GamestoreContext _context;
    private readonly PlatformRepository _platformRepository;
    private bool _disposedValue;

    public PlatformRepositoryTests()
    {
        var options = new DbContextOptionsBuilder().UseInMemoryDatabase("PlatformRepoTest").Options;

        _context = new GamestoreContext(options);
        _platformRepository = new(_context);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        ContextHelpers.ClearContext(_context);
        ContextHelpers.SeedGenres(_context);
        ContextHelpers.SeedPlatforms(_context);
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllPlatforms()
    {
        // Arrange
        var expectedGenres = _context.Platforms;

        // Act
        var actualGenres = await _platformRepository.GetAllAsync();

        // Assert
        Assert.True(actualGenres.Count != 0);
        Assert.Equal(expectedGenres.Count(), actualGenres.Count);
    }

    [Fact]
    public async Task GetByIdAsyncShoudReturnCorrectPlatform()
    {
        // Arrange
        var expectedPlatform = _context.Platforms.First();
        var expectedPlatformId = expectedPlatform.Id;

        // Act
        var actualPlatform = await _platformRepository.GetByIdAsync(expectedPlatformId);

        // Assert
        Assert.Equal(expectedPlatform, actualPlatform);
    }

    [Fact]
    public async Task GetGamesByPlatformAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var id = Guid.NewGuid();
        var gamePlatform = _context.Platforms.First(x => x.Type == "Mobile");
        await TestGameHelpers.AddTestGameAsync(_context, id, "Baldurs Gate", "BG", "Rpg game", null, gamePlatform);

        var expectedGameId = Guid.NewGuid();
        var expectedPlatform = _context.Platforms.First(x => x.Type == "Console");
        var expectedPlatformId = expectedPlatform.Id;
        await TestGameHelpers.AddTestGameAsync(_context, expectedGameId, "Test Drive", "TD", "Racing game", null, expectedPlatform);

        // Act
        var actualGames = await _platformRepository.GetGamesByPlatformAsync(expectedPlatformId);
        var actualPlatformId = actualGames[0].GamePlatforms[0].PlatformId;

        // Assert
        Assert.Single(actualGames);
        Assert.Equal(expectedPlatform.Id, actualPlatformId);
    }

    [Fact]
    public async Task AddAsyncShouldAddPlatform()
    {
        // Arrange
        var startingPlatforms = _context.Platforms;
        var expectedPlatformCount = startingPlatforms.Count() + 1;

        var expectedPlatform = new Platform()
        {
            Id = Guid.NewGuid(),
            Type = "New Platform",
        };

        // Act
        await _platformRepository.AddAsync(expectedPlatform);
        await _context.SaveChangesAsync();
        var actualPlatforms = _context.Platforms;

        // Assert
        Assert.Equal(expectedPlatformCount, actualPlatforms.Count());
        Assert.Contains(expectedPlatform, actualPlatforms);
    }

    [Fact]
    public void DeleteShouldDeleteCorrectPlatform()
    {
        // Arrange
        var startingPlatforms = _context.Platforms;
        var platformToDelete = _context.Platforms.First();
        var expectedPlatformCount = startingPlatforms.Count() - 1;

        // Act
        _platformRepository.Delete(platformToDelete);
        _context.SaveChanges();
        var actualPlatforms = _context.Platforms;

        // Assert
        Assert.Equal(expectedPlatformCount, actualPlatforms.Count());
        Assert.DoesNotContain(platformToDelete, _context.Platforms);
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdatePlatform()
    {
        // Arrange
        var platformToUpdate = _context.Platforms.First();
        var platformToUpdateId = platformToUpdate.Id;
        var expectedType = "New type";
        platformToUpdate.Type = expectedType;

        // Act
        await _platformRepository.UpdateAsync(platformToUpdate);
        var actualPlatform = _context.Platforms.First(x => x.Id == platformToUpdateId);

        // Assert
        Assert.Equal(expectedType, actualPlatform.Type);
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
