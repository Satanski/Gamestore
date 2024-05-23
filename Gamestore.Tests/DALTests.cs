using Gamestore.Repository.Entities;
using Gamestore.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Tests;

#pragma warning disable S3881 // "IDisposable" should be implemented correctly
public class DALTests : IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
{
    private readonly GamestoreContext _context;
    private readonly GameRepository _gameRepository;

    public DALTests()
    {
        var options = new DbContextOptionsBuilder().
        UseSqlite("Data Source = testDb.db")
        .Options;

        _context = new GamestoreContext(options);
        _gameRepository = new(_context);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task AddGameAsyncAddsGameToDb()
    {
        // Arrange
        Guid expectedGameId = Guid.NewGuid();
        var genre = _context.Genres.First();
        var expectedGenreId = genre.Id;
        GameGenre expectedGameGenre = new GameGenre() { GameId = expectedGameId, GenreId = expectedGenreId };

        var platform = _context.Platforms.First();
        var expectedPlatformId = platform.Id;
        GamePlatform expectedGamePlatform = new GamePlatform() { GameId = expectedGameId, PlatformId = expectedPlatformId };

        string expectedName = "Baldurs Gate";
        string expectedKey = "BG";
        string expectedDescription = "Rpg game";

#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
        List<GameGenre> gameGenres = [expectedGameGenre];
        List<GamePlatform> gamePlatforms = [expectedGamePlatform];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly

        // Act
        await _gameRepository.AddGameAsync(new Game()
        {
            Id = expectedGameId,
            Name = expectedName,
            Key = expectedKey,
            Description = expectedDescription,
            GameGenres = gameGenres,
            GamePlatforms = gamePlatforms,
        });

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
    public async Task DeleteGameAsyncShouldDeleteGameFromDb()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        AddTestGame(expectedGameId);

        // Act
        await _gameRepository.DeleteGameAsync(expectedGameId);

        // Assert
        var gamesInDatabase = _context.Games;
        Assert.Equal(0, gamesInDatabase.Count());
    }

    [Fact]
    public async Task GetGameByIdAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        AddTestGame(expectedGameId);

        // Act
        var resultGame = await _gameRepository.GetGameByIdAsync(expectedGameId);

        // Assert
        Assert.Equal(expectedGameId, resultGame.Id);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    private void AddTestGame(Guid expectedGameId)
    {
        var genre = _context.Genres.First();
        var expectedGenreId = genre.Id;
        GameGenre expectedGameGenre = new GameGenre() { GameId = expectedGameId, GenreId = expectedGenreId };

        var platform = _context.Platforms.First();
        var expectedPlatformId = platform.Id;
        GamePlatform expectedGamePlatform = new GamePlatform() { GameId = expectedGameId, PlatformId = expectedPlatformId };

        string expectedName = "Baldurs Gate";
        string expectedKey = "BG";
        string expectedDescription = "Rpg game";

#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
        List<GameGenre> gameGenres = [expectedGameGenre];
        List<GamePlatform> gamePlatforms = [expectedGamePlatform];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly

        Game game = new Game()
        {
            Id = expectedGameId,
            Name = expectedName,
            Key = expectedKey,
            Description = expectedDescription,
            GameGenres = gameGenres,
            GamePlatforms = gamePlatforms,
        };

        _context.Games.Add(game);
        _context.SaveChanges();
    }
}
