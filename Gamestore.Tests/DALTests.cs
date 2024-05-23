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
        AddTestGame(expectedGameId, "Baldurs Gate", "BG", "Rpg game");

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
        AddTestGame(expectedGameId, "Baldurs Gate", "BG", "Rpg game");

        // Act
        var resultGame = await _gameRepository.GetGameByIdAsync(expectedGameId);

        // Assert
        Assert.Equal(expectedGameId, resultGame.Id);
    }

    [Fact]
    public async Task GetGameByKeyAsyncShouldReturnCorrectGame()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        string expectedGameKey = "Key";
        AddTestGame(expectedGameId, "Baldurs Gate", expectedGameKey, "Rpg game");

        // Act
        var resultGame = await _gameRepository.GetGameByIdAsync(expectedGameId);

        // Assert
        Assert.Equal(expectedGameKey, resultGame.Key);
    }

    [Fact]
    public async Task GetPlatformsByGameAsyncShouldReturnCorrectPlatform()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        AddTestGame(expectedGameId, "Baldurs Gate", "BG", "Rpg game");
        var expectedPlatform = _context.Platforms.First();

        // Act
        var resultPlatforms = await _gameRepository.GetPlatformsByGameAsync(expectedGameId);

        // Assert
        Assert.Equal(expectedPlatform.Id, resultPlatforms.First().Id);
        Assert.Equal(expectedPlatform.Type, resultPlatforms.First().Type);
    }

    [Fact]
    public async Task GetGenresByGameShouldReturnCorrectGenre()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        AddTestGame(expectedGameId, "Baldurs Gate", "BG", "Rpg game");
        var expectedGenre = _context.Genres.First();

        // Act
        var resultGenres = await _gameRepository.GetGenresByGameAsync(expectedGameId);

        // Assert
        Assert.Equal(expectedGenre.Id, resultGenres.First().Id);
        Assert.Equal(expectedGenre.Name, resultGenres.First().Name);
    }

    [Fact]
    public async Task GetAllGamesAsyncShouldreturnAllGames()
    {
        // Arrange
        var expectedGameId1 = Guid.NewGuid();
        AddTestGame(expectedGameId1, "Baldurs Gate", "BG", "Rpg game");

        var expectedGameId2 = Guid.NewGuid();
        AddTestGame(expectedGameId2, "Digital Combat Simulator", "DCS", "Flight sim");

        // Act
        var games = await _gameRepository.GetAllGamesAsync();

        // Assert
        Assert.Equal(2, games.Count());
    }

    [Fact]
    public async Task UpdateGameAsyncShouldUpdateGame()
    {
        // Arrange
        var expectedGameId = Guid.NewGuid();
        AddTestGame(expectedGameId, "Baldurs Gate", "BG", "Rpg game");
        var gameToUpdate = _context.Games.First();

        string expectedName = "Digital Combat Simulator";
        string expectedKey = "DCS";
        string expectedDescription = "Flight sim";

        gameToUpdate.Name = expectedName;
        gameToUpdate.Key = expectedKey;
        gameToUpdate.Description = expectedDescription;

        // Act
        await _gameRepository.UpdateGameAsync(gameToUpdate);

        // Assert
        Assert.Equal(expectedName, gameToUpdate.Name);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    private void AddTestGame(Guid expectedGameId, string expectedName, string expectedKey, string expectedDescription)
    {
        var genre = _context.Genres.First();
        var expectedGenreId = genre.Id;
        GameGenre expectedGameGenre = new GameGenre() { GameId = expectedGameId, GenreId = expectedGenreId };

        var platform = _context.Platforms.First();
        var expectedPlatformId = platform.Id;
        GamePlatform expectedGamePlatform = new GamePlatform() { GameId = expectedGameId, PlatformId = expectedPlatformId };

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
