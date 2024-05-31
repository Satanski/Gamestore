using Gamestore.DAL.Entities;
using Gamestore.DAL.Repositories;
using Gamestore.Tests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DALTests;
public class GameRepositoryTests : IDisposable
{
    private readonly GamestoreContext _context;
    private readonly GameRepository _gameRepository;
    private bool _disposedValue;

    public GameRepositoryTests()
    {
        var options = new DbContextOptionsBuilder().UseInMemoryDatabase("GameRepoTest").Options;

        _context = new GamestoreContext(options);
        _gameRepository = new(_context);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        ContextHelpers.ClearContext(_context);
        ContextHelpers.SeedGenres(_context);
        ContextHelpers.SeedPlatforms(_context);
    }

    [Fact]
    public async Task AddAsyncAddsGameToDb()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        var genre = _context.Genres.First();
        var expectedGenreId = genre.Id;
        var expectedGameGenre = new GameGenre() { GameId = expectedGameId, GenreId = expectedGenreId };

        var platform = _context.Platforms.First();
        var expectedPlatformId = platform.Id;
        var expectedGamePlatform = new GamePlatform() { GameId = expectedGameId, PlatformId = expectedPlatformId };

        var expectedName = "Baldurs Gate";
        var expectedKey = "BG";
        var expectedDescription = "Rpg game";

#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
        List<GameGenre> gameGenres = [expectedGameGenre];
        List<GamePlatform> gamePlatforms = [expectedGamePlatform];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly

        // Act
        await _gameRepository.AddAsync(new Game()
        {
            Id = expectedGameId,
            Name = expectedName,
            Key = expectedKey,
            Description = expectedDescription,
            GameGenres = gameGenres,
            GamePlatforms = gamePlatforms,
        });
        await _context.SaveChangesAsync();

        // Assert
        var gamesInDatabase = _context.Games;
        var resultGame = _context.Games.Find(expectedGameId);

        Assert.Equal(1, gamesInDatabase.Count());
        Assert.Equal(expectedName, resultGame.Name);
        Assert.Equal(expectedKey, resultGame.Key);
        Assert.Equal(expectedDescription, resultGame.Description);
        Assert.Contains(expectedGameGenre, resultGame.GameGenres);
        Assert.Contains(expectedGamePlatform, resultGame.GamePlatforms);
    }

    [Fact]
    public void DeleteThrowsExceptionWhenAssociationIsSevered()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
#pragma warning disable xUnit1031 // Do not use blocking task operations in test method
        var game = TestGameHelpers.AddTestGameAsync(_context, expectedGameId, "Baldurs Gate", "BG", "Rpg game").Result;
#pragma warning restore xUnit1031 // Do not use blocking task operations in test method

        // Act
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            _gameRepository.Delete(game);
            await _context.SaveChangesAsync();
        });
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        await TestGameHelpers.AddTestGameAsync(_context, expectedGameId, "Baldurs Gate", "BG", "Rpg game");

        // Act
        var resultGame = await _gameRepository.GetByIdAsync(expectedGameId);

        // Assert
        Assert.Equal(expectedGameId, resultGame.Id);
    }

    [Fact]
    public async Task GetByKeyAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        var expectedGameKey = "Key";
        await TestGameHelpers.AddTestGameAsync(_context, expectedGameId, "Baldurs Gate", expectedGameKey, "Rpg game");

        // Act
        var resultGame = await _gameRepository.GetByIdAsync(expectedGameId);

        // Assert
        Assert.Equal(expectedGameKey, resultGame.Key);
    }

    [Fact]
    public async Task GetPlatformsByGameAsyncShouldReturnCorrectPlatform()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        await TestGameHelpers.AddTestGameAsync(_context, expectedGameId, "Baldurs Gate", "BG", "Rpg game");
        var expectedPlatform = _context.Platforms.First();

        // Act
        var resultPlatforms = await _gameRepository.GetPlatformsByGameAsync(expectedGameId);

        // Assert
        Assert.Equal(expectedPlatform.Id, resultPlatforms[0].Id);
        Assert.Equal(expectedPlatform.Type, resultPlatforms[0].Type);
    }

    [Fact]
    public async Task GetGenresByGameShouldReturnCorrectGenre()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        await TestGameHelpers.AddTestGameAsync(_context, expectedGameId, "Baldurs Gate", "BG", "Rpg game");
        var expectedGenre = _context.Genres.First();

        // Act
        var resultGenres = await _gameRepository.GetGenresByGameAsync(expectedGameId);

        // Assert
        Assert.Equal(expectedGenre.Id, resultGenres[0].Id);
        Assert.Equal(expectedGenre.Name, resultGenres[0].Name);
    }

    [Fact]
    public async Task GetAllAsyncShouldreturnAllGames()
    {
        // Arrange
        var expectedGameId1 = Guid.NewGuid();
        await TestGameHelpers.AddTestGameAsync(_context, expectedGameId1, "Baldurs Gate", "BG", "Rpg game");

        var expectedGameId2 = Guid.NewGuid();
        await TestGameHelpers.AddTestGameAsync(_context, expectedGameId2, "Digital Combat Simulator", "DCS", "Flight sim");

        // Act
        var games = await _gameRepository.GetAllAsync();

        // Assert
        Assert.Equal(2, games.Count);
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdateGame()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        await TestGameHelpers.AddTestGameAsync(_context, expectedGameId, "Baldurs Gate", "BG", "Rpg game");
        var gameToUpdate = _context.Games.First();

        var expectedName = "Digital Combat Simulator";
        var expectedKey = "DCS";
        var expectedDescription = "Flight sim";

        gameToUpdate.Name = expectedName;
        gameToUpdate.Key = expectedKey;
        gameToUpdate.Description = expectedDescription;

        // Act
        await _gameRepository.UpdateAsync(gameToUpdate);

        // Assert
        Assert.Equal(expectedName, gameToUpdate.Name);
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
