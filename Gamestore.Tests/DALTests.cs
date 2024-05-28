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
    private readonly GenreRepository _genreRepository;
    private readonly PlatformRepository _platformRepository;

    public DALTests()
    {
        var options = new DbContextOptionsBuilder().UseSqlite("Data Source = testDb.db").Options;

        _context = new GamestoreContext(options);
        _gameRepository = new(_context);
        _genreRepository = new(_context);
        _platformRepository = new(_context);

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

    [Fact]
    public async Task GetAllGenresAsyncShouldReturnAllGenres()
    {
        // Arrange
        var expectedGenres = _context.Genres;

        // Act
        var actualGenres = await _genreRepository.GetAllGenresAsync();

        // Assert
        Assert.True(actualGenres.Any());
        Assert.Equal(expectedGenres.Count(), actualGenres.Count());
    }

    [Fact]
    public async Task GetGenreByIdAsyncShoudReturnCorrectGenre()
    {
        // Arrange
        var expectedGenre = _context.Genres.First();
        var expectedGenreId = expectedGenre.Id;

        // Act
        var actualGenre = await _genreRepository.GetGenreByIdAsync(expectedGenreId);

        // Assert
        Assert.Equal(expectedGenre, actualGenre);
    }

    [Fact]
    public async Task GetGamesByGenreAsyncShouldReturnCorrectGame()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AddTestGame(id, "Baldurs Gate", "BG", "Rpg game");

        Guid expectedGameId = Guid.NewGuid();
        var expectedGenre = _context.Genres.First(x => x.Name == "Races");
        var expectedGenreId = expectedGenre.Id;
        AddTestGame(expectedGameId, "Test Drive", "TD", "Racing game", expectedGenre);

        // Act
        var actualGames = await _genreRepository.GetGamesByGenreAsync(expectedGenreId);
        var actualGenreId = actualGames.First().GameGenres.First().GenreId;

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
        Assert.Equal(expectedGenres.Count(), actualGenres.Count());
    }

    [Fact]
    public async Task AddGenreAsyncShouldAddGenre()
    {
        // Arrange
        var startingGenres = _context.Genres;
        int expectedGenreCount = startingGenres.Count() + 1;

        Genre expectedGenre = new Genre()
        {
            Id = Guid.NewGuid(),
            Name = "New Genre",
        };

        // Act
        await _genreRepository.AddGenreAsync(expectedGenre);
        var actualGenres = _context.Genres;

        // Assert
        Assert.Equal(expectedGenreCount, actualGenres.Count());
        Assert.Contains(expectedGenre, actualGenres);
    }

    [Fact]
    public async Task DeleteGenreAsyncShouldDeleteCorrectGenre()
    {
        // Arrange
        var startingGenres = _context.Genres;
        var genreToDelete = _context.Genres.First();
        int expectedGenreCount = startingGenres.Count() - 1;

        // Act
        await _genreRepository.DeleteGenreAsync(genreToDelete.Id);
        var actualGenres = _context.Genres;

        // Assert
        Assert.Equal(expectedGenreCount, actualGenres.Count());
        Assert.DoesNotContain(genreToDelete, _context.Genres);
    }

    [Fact]
    public async Task UpdateGenreAsyncShouldUpdateGenre()
    {
        // Arrange
        var genreToUpdate = _context.Genres.First();
        var genreToUpdateId = genreToUpdate.Id;
        string expectedName = "New name";
        genreToUpdate.Name = expectedName;

        // Act
        await _genreRepository.UpdateGenreAsync(genreToUpdate);
        var actualGenre = _context.Genres.First(x => x.Id == genreToUpdateId);

        // Assert
        Assert.Equal(expectedName, actualGenre.Name);
    }

    [Fact]
    public async Task GetAllPlatformsAsyncShouldReturnAllPlatforms()
    {
        // Arrange
        var expectedGenres = _context.Platforms;

        // Act
        var actualGenres = await _platformRepository.GetAllPlatformsAsync();

        // Assert
        Assert.True(actualGenres.Any());
        Assert.Equal(expectedGenres.Count(), actualGenres.Count());
    }

    [Fact]
    public async Task GetPlatformByIdAsyncShoudReturnCorrectPlatform()
    {
        // Arrange
        var expectedPlatform = _context.Platforms.First();
        var expectedPlatformId = expectedPlatform.Id;

        // Act
        var actualPlatform = await _platformRepository.GetPlatformByIdAsync(expectedPlatformId);

        // Assert
        Assert.Equal(expectedPlatform, actualPlatform);
    }

    [Fact]
    public async Task GetGamesByPlatformAsyncShouldReturnCorrectGame()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AddTestGame(id, "Baldurs Gate", "BG", "Rpg game");

        Guid expectedGameId = Guid.NewGuid();
        var expectedPlatform = _context.Platforms.First(x => x.Type == "Mobile");
        var expectedPlatformId = expectedPlatform.Id;
        AddTestGame(expectedGameId, "Test Drive", "TD", "Racing game", null, expectedPlatform);

        // Act
        var actualGames = await _platformRepository.GetGamesByPlatformAsync(expectedPlatformId);
        var actualPlatformId = actualGames.First().GamePlatforms.First().PlatformId;

        // Assert
        Assert.Single(actualGames);
        Assert.Equal(expectedPlatform.Id, actualPlatformId);
    }

    [Fact]
    public async Task AddPlatformAsyncShouldAddPlatform()
    {
        // Arrange
        var startingPlatforms = _context.Platforms;
        int expectedPlatformCount = startingPlatforms.Count() + 1;

        Platform expectedPlatform = new Platform()
        {
            Id = Guid.NewGuid(),
            Type = "New Platform",
        };

        // Act
        await _platformRepository.AddPlatformAsync(expectedPlatform);
        var actualPlatforms = _context.Platforms;

        // Assert
        Assert.Equal(expectedPlatformCount, actualPlatforms.Count());
        Assert.Contains(expectedPlatform, actualPlatforms);
    }

    [Fact]
    public async Task DeletePlatformAsyncShouldDeleteCorrectPlatform()
    {
        // Arrange
        var startingPlatforms = _context.Platforms;
        var platformToDelete = _context.Platforms.First();
        int expectedPlatformCount = startingPlatforms.Count() - 1;

        // Act
        await _platformRepository.DeletePlatformAsync(platformToDelete.Id);
        var actualPlatforms = _context.Platforms;

        // Assert
        Assert.Equal(expectedPlatformCount, actualPlatforms.Count());
        Assert.DoesNotContain(platformToDelete, _context.Platforms);
    }

    [Fact]
    public async Task UpdatePlatformAsyncShouldUpdatePlatform()
    {
        // Arrange
        var platformToUpdate = _context.Platforms.First();
        var platformToUpdateId = platformToUpdate.Id;
        string expectedType = "New type";
        platformToUpdate.Type = expectedType;

        // Act
        await _platformRepository.UpdatePlatformAsync(platformToUpdate);
        var actualPlatform = _context.Platforms.First(x => x.Id == platformToUpdateId);

        // Assert
        Assert.Equal(expectedType, actualPlatform.Type);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    private void AddTestGame(Guid expectedGameId, string expectedName, string expectedKey, string expectedDescription, Genre expectedGenre = null, Platform expectedPlatform = null)
    {
        expectedGenre ??= _context.Genres.First();
        expectedPlatform ??= _context.Platforms.First();

        var expectedGenreId = expectedGenre.Id;

        GameGenre expectedGameGenre = new GameGenre() { GameId = expectedGameId, GenreId = expectedGenreId };

        var expectedPlatformId = expectedPlatform.Id;
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
