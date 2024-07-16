using Gamestore.DAL.Entities;

namespace Gamestore.Tests.Helpers;

internal static class ContextHelpers
{
    internal static void ClearContext(GamestoreContext context)
    {
        var games = context.Products;
        var platforms = context.Platforms;
        var genres = context.Genres;
        var gamePlatforms = context.ProductPlatforms;
        var gameGenres = context.ProductGenres;
        context.Products.RemoveRange(games);
        context.Platforms.RemoveRange(platforms);
        context.Genres.RemoveRange(genres);
        context.ProductPlatforms.RemoveRange(gamePlatforms);
        context.ProductGenres.RemoveRange(gameGenres);
        context.SaveChanges();
    }

    internal static void SeedGenres(GamestoreContext context)
    {
        var strategyGuid = Guid.NewGuid();
        context.Genres.Add(new Category() { Id = strategyGuid, Name = "Strategy" });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "RTS", ParentCategoryId = strategyGuid });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "TBS", ParentCategoryId = strategyGuid });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "RPG" });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "Sports" });
        var racesGuid = Guid.NewGuid();
        context.Genres.Add(new Category() { Id = racesGuid, Name = "Races" });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "Rally", ParentCategoryId = racesGuid });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "Arcade", ParentCategoryId = racesGuid });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "Formula", ParentCategoryId = racesGuid });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "Off-road", ParentCategoryId = racesGuid });
        var actionGuid = Guid.NewGuid();
        context.Genres.Add(new Category() { Id = actionGuid, Name = "Action" });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "FPS", ParentCategoryId = actionGuid });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "TPS", ParentCategoryId = actionGuid });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "Adventure" });
        context.Genres.Add(new Category() { Id = Guid.NewGuid(), Name = "Puzzle & Skill" });
        context.SaveChanges();
    }

    internal static void SeedPlatforms(GamestoreContext context)
    {
        context.Platforms.Add(new Platform() { Id = Guid.NewGuid(), Type = "Mobile" });
        context.Platforms.Add(new Platform() { Id = Guid.NewGuid(), Type = "Browser" });
        context.Platforms.Add(new Platform() { Id = Guid.NewGuid(), Type = "Desktop" });
        context.Platforms.Add(new Platform() { Id = Guid.NewGuid(), Type = "Console" });
        context.SaveChanges();
    }
}
