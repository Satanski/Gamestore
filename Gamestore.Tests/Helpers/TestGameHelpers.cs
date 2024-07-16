using Gamestore.DAL.Entities;

namespace Gamestore.Tests.Helpers;

internal static class TestGameHelpers
{
    internal static async Task<Product> AddTestGameAsync(GamestoreContext context, Guid expectedGameId, string expectedName, string expectedKey, string expectedDescription, Category expectedGenre = null, Platform expectedPlatform = null)
    {
        expectedGenre ??= context.Genres.First();
        expectedPlatform ??= context.Platforms.First();

        var expectedGenreId = expectedGenre.Id;

        var expectedGameGenre = new ProductCategory() { ProductId = expectedGameId, CategoryId = expectedGenreId };

        var expectedPlatformId = expectedPlatform.Id;
        var expectedGamePlatform = new ProductPlatform() { ProductId = expectedGameId, PlatformId = expectedPlatformId };

        List<ProductCategory> gameGenres = [expectedGameGenre];
        List<ProductPlatform> gamePlatforms = [expectedGamePlatform];

        var game = new Product()
        {
            Id = expectedGameId,
            Name = expectedName,
            Key = expectedKey,
            Description = expectedDescription,
            ProductCategories = gameGenres,
            ProductPlatforms = gamePlatforms,
            Publisher = new Supplier() { Id = Guid.NewGuid(), CompanyName = "Test company" },
            Comments = [],
            IsDeleted = false,
        };

        await context.Products.AddAsync(game);
        await context.SaveChangesAsync();
        return game;
    }
}
