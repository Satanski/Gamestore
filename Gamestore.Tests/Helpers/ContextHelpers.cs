using Gamestore.DAL.Entities;

namespace Gamestore.Tests.Helpers;

internal static class ContextHelpers
{
    internal static void ClearContext(GamestoreContext context)
    {
        var games = context.Games;
        var platforms = context.Platforms;
        var genres = context.Genres;
        var gamePlatforms = context.GamePlatforms;
        var gameGenres = context.GameGenres;
        context.Games.RemoveRange(games);
        context.Platforms.RemoveRange(platforms);
        context.Genres.RemoveRange(genres);
        context.GamePlatforms.RemoveRange(gamePlatforms);
        context.GameGenres.RemoveRange(gameGenres);
        context.SaveChanges();
    }

    internal static void SeedGenres(GamestoreContext context)
    {
        Guid strategyGuid = Guid.NewGuid();
        context.Genres.Add(new Genre() { Id = strategyGuid, Name = "Strategy" });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "RTS", ParentGenreId = strategyGuid });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "TBS", ParentGenreId = strategyGuid });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "RPG" });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "Sports" });
        Guid racesGuid = Guid.NewGuid();
        context.Genres.Add(new Genre() { Id = racesGuid, Name = "Races" });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "Rally", ParentGenreId = racesGuid });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "Arcade", ParentGenreId = racesGuid });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "Formula", ParentGenreId = racesGuid });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "Off-road", ParentGenreId = racesGuid });
        Guid actionGuid = Guid.NewGuid();
        context.Genres.Add(new Genre() { Id = actionGuid, Name = "Action" });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "FPS", ParentGenreId = actionGuid });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "TPS", ParentGenreId = actionGuid });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "Adventure" });
        context.Genres.Add(new Genre() { Id = Guid.NewGuid(), Name = "Puzzle & Skill" });
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
