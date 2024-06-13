using Gamestore.DAL.Entities;

namespace Gamestore.Tests.Helpers;

internal static class TestGameHelpers
{
    internal static async Task<Game> AddTestGameAsync(GamestoreContext context, Guid expectedGameId, string expectedName, string expectedKey, string expectedDescription, Genre expectedGenre = null, Platform expectedPlatform = null)
    {
        expectedGenre ??= context.Genres.First();
        expectedPlatform ??= context.Platforms.First();

        var expectedGenreId = expectedGenre.Id;

        var expectedGameGenre = new GameGenre() { GameId = expectedGameId, GenreId = expectedGenreId };

        var expectedPlatformId = expectedPlatform.Id;
        var expectedGamePlatform = new GamePlatform() { GameId = expectedGameId, PlatformId = expectedPlatformId };

#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
        List<GameGenre> gameGenres = [expectedGameGenre];
        List<GamePlatform> gamePlatforms = [expectedGamePlatform];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly

        var game = new Game()
        {
            Id = expectedGameId,
            Name = expectedName,
            Key = expectedKey,
            Description = expectedDescription,
            GameGenres = gameGenres,
            GamePlatforms = gamePlatforms,
        };

        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();
        return game;
    }
}
