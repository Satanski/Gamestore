using Gamestore.DAL.Entities;

namespace Gamestore.Tests.Helpers;

internal static class TestGameHelpers
{
    internal static async Task<Game> AddTestGameAsync(GamestoreContext context, Guid expectedGameId, string expectedName, string expectedKey, string expectedDescription, Genre expectedGenre = null, Platform expectedPlatform = null)
    {
        expectedGenre ??= context.Genres.First();
        expectedPlatform ??= context.Platforms.First();

        var expectedGenreId = expectedGenre.Id;

        var expectedGameGenre = new GameGenres() { GameId = expectedGameId, GenreId = expectedGenreId };

        var expectedPlatformId = expectedPlatform.Id;
        var expectedGamePlatform = new GamePlatform() { GameId = expectedGameId, PlatformId = expectedPlatformId };

        List<GameGenres> gameGenres = [expectedGameGenre];
        List<GamePlatform> gamePlatforms = [expectedGamePlatform];

        var game = new Game()
        {
            Id = expectedGameId,
            Name = expectedName,
            Key = expectedKey,
            Description = expectedDescription,
            ProductCategories = gameGenres,
            ProductPlatforms = gamePlatforms,
            Publisher = new Publisher() { Id = Guid.NewGuid(), CompanyName = "Test company" },
            Comments = [],
            IsDeleted = false,
        };

        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();
        return game;
    }
}
