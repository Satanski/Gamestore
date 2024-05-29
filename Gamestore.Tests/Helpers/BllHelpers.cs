using AutoMapper;
using Gamestore.DAL.Entities;
using Gamestore.Services.Models;

namespace Gamestore.Tests.Helpers;

internal static class BllHelpers
{
    internal static IEnumerable<GameModelDto> GameModelDtos =>
   [
       new() { Id = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63"), Name = "Baldurs Gate", Key = "BG", Description = "Rpg game", GamePlatforms = PlatformModelDtos, GameGenres = GenreModelDtos },
       new() { Id = new Guid("382f980c-11fd-48f8-8c12-916ad4390622"), Name = "Test Drive", Key = "TD", Description = "Racing game", GamePlatforms = PlatformModelDtos, GameGenres = GenreModelDtos },
        ];

    internal static IEnumerable<GameModel> GameModels =>
       [
           new() { Id = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63"), Name = "Baldurs Gate", Key = "BG", Description = "Rpg game" },
            new() { Id = new Guid("382f980c-11fd-48f8-8c12-916ad4390622"), Name = "Test Drive", Key = "TD", Description = "Racing game" },
        ];

    internal static IEnumerable<Game> Games =>
        [
            new() { Id = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63"), Name = "Baldurs Gate", Key = "BG", Description = "Rpg game" },
            new() { Id = new Guid("382f980c-11fd-48f8-8c12-916ad4390622"), Name = "Test Drive", Key = "TD", Description = "Racing game" },
        ];

    internal static List<GenreModelDto> GenreModelDtos =>
        [
            new() { Id = new Guid("cc3a7cb9-2a97-4440-9965-97f47decd0d8"), Name = "Rpg" }
        ];

    internal static List<GenreModel> GenreModels =>
        [
            new() { Name = "Rpg" }
        ];

    internal static List<Genre> Genres =>
        [
            new() { Id = new Guid("cc3a7cb9-2a97-4440-9965-97f47decd0d8"), Name = "Rpg" }
        ];

    internal static List<PlatformModelDto> PlatformModelDtos =>
        [
            new() { Id = new Guid("22a4c311-e897-4d46-abb7-2b604c748e4d"), Type = "Desktop" }
        ];

    internal static List<Platform> Platforms =>
        [
            new() { Id = new Guid("22a4c311-e897-4d46-abb7-2b604c748e4d"), Type = "Desktop" }
        ];

    internal static List<GameGenre> GameGenres =>
        [
            new() { GameId = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63"), GenreId = new Guid("cc3a7cb9-2a97-4440-9965-97f47decd0d8") }
        ];

    internal static List<GamePlatform> GamePlatforms =>
    [
        new() { GameId = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63"), PlatformId = new Guid("22a4c311-e897-4d46-abb7-2b604c748e4d") }
    ];

    internal static IMapper CreateMapperProfile()
    {
        var myProfile = new MappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

        return new Mapper(configuration);
    }
}
