using System.Data;
using AutoMapper;
using Gamestore.BLL.Models;
using Gamestore.DAL.Entities;
using Gamestore.Services.Models;

namespace Gamestore.Tests.Helpers;

internal static class BllHelpers
{
    internal static List<Game> Games =>
        [
            new()
            {
                Id = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63"),
                Name = "Baldurs Gate",
                Key = "BG",
                Description = "Rpg game",
                Price = 100,
                PublishDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-7),
                NumberOfViews = 10,
                IsDeleted = false,
                Comments = [new()],
                GameGenres = [GameGenres.First(x => x.GenreId == Genres.First(x => x.Name == "Rpg").Id)],
                GamePlatforms = [GamePlatforms.First(x => x.PlatformId == Platforms.First(x => x.Type == "Desktop").Id)],
                Publisher = Publishers.First(x => x.CompanyName == "Blizzard"),
            },
            new()
            {
                Id = new Guid("382f980c-11fd-48f8-8c12-916ad4390622"),
                Name = "Test Drive",
                Key = "TD",
                Description = "Racing game",
                Price = 200,
                PublishDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(-1),
                NumberOfViews = 100,
                IsDeleted = false,
                Comments = [new(), new()],
                GameGenres = [GameGenres.First(x => x.GenreId == Genres.First(x => x.Name == "Racing").Id)],
                GamePlatforms = [GamePlatforms.First(x => x.PlatformId == Platforms.First(x => x.Type == "Mobile").Id)],
                Publisher = Publishers.First(x => x.CompanyName == "Activision"),
            },
            new()
            {
                Id = new Guid("08b747fc-8a9b-1234-94ef-56a36fc0fa63"),
                Name = "Digital Combat Simulator",
                Key = "DCS",
                Description = "Sim game",
                Price = 300,
                PublishDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1),
                NumberOfViews = 1000,
                IsDeleted = false,
                Comments = [new(), new(), new()],
                GameGenres = [GameGenres.First(x => x.GenreId == Genres.First(x => x.Name == "Simulator").Id)],
                GamePlatforms = [GamePlatforms.First(x => x.PlatformId == Platforms.First(x => x.Type == "Console").Id)],
                Publisher = Publishers.First(x => x.CompanyName == "BioWare"),
            },
        ];

    internal static List<GameGenre> GameGenres =>
        [
            new() { GameId = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63"), GenreId = Genres.Find(x => x.Name == "Rpg").Id },
            new() { GameId = new Guid("382f980c-11fd-48f8-8c12-916ad4390622"), GenreId = Genres.Find(x => x.Name == "Racing").Id },
            new() { GameId = new Guid("08b747fc-8a9b-1234-94ef-56a36fc0fa63"), GenreId = Genres.Find(x => x.Name == "Simulator").Id },
        ];

    internal static List<GamePlatform> GamePlatforms =>
        [
            new() { GameId = new Guid("08b747fc-8a9b-4041-94ef-56a36fc0fa63"), PlatformId = Platforms.Find(x => x.Type == "Desktop").Id },
            new() { GameId = new Guid("382f980c-11fd-48f8-8c12-916ad4390622"), PlatformId = Platforms.Find(x => x.Type == "Mobile").Id },
            new() { GameId = new Guid("08b747fc-8a9b-1234-94ef-56a36fc0fa63"), PlatformId = Platforms.Find(x => x.Type == "Console").Id },
        ];

#pragma warning disable CS8601 // Possible null reference assignment.
    internal static List<GameModelDto> GameModelDtos
    {
        get
        {
            List<GameModelDto> gameModelDtos = [];
            for (int i = 0; i < Games.Count; i++)
            {
                gameModelDtos.Add(new() { Id = Games[i].Id, Name = Games[i].Name, Key = Games[i].Key, Description = Games[i].Description, Platforms = [PlatformModelDtos.First(x => x.Id == Games[i].GamePlatforms[0].PlatformId)], Genres = [GenreModelDtos.First(x => x.Id == Games[i].GameGenres[0].GenreId)], Publisher = PublisherModelDtos.First(x => x.Id == Games[i].Publisher.Id) });
            }

            return gameModelDtos;
        }
    }
#pragma warning restore CS8601 // Possible null reference assignment.

    internal static List<Genre> Genres =>
        [
            new() { Id = new Guid("4885AB50-BF64-48D2-80CC-28C94D184841"), Name = "Rpg" },
            new() { Id = new Guid("4885AB50-1111-48D2-80CC-28C94D184841"), Name = "Racing" },
            new() { Id = new Guid("4885AB50-8361-48D2-80CC-28C94D184841"), Name = "Simulator" }
        ];

    internal static List<GenreModelDto> GenreModelDtos
    {
        get
        {
            List<GenreModelDto> genreModelDtos = [];
            for (int i = 0; i < Genres.Count; i++)
            {
                genreModelDtos.Add(new() { Id = Genres[i].Id, Name = Genres[i].Name });
            }

            return genreModelDtos;
        }
    }

    internal static List<Platform> Platforms =>
        [
            new() { Id = new Guid("32A5CCEE-D5E1-4449-AF51-2CB10B025935"), Type = "Desktop" },
            new() { Id = new Guid("32A5CCEE-D5E1-1111-AF51-2CB10B025935"), Type = "Mobile" },
            new() { Id = new Guid("32A5CCEE-D5E1-2222-AF51-2CB10B025935"), Type = "Console" },
        ];

    internal static List<PlatformModelDto> PlatformModelDtos
    {
        get
        {
            List<PlatformModelDto> platformModelDtos = [];
            for (int i = 0; i < Platforms.Count; i++)
            {
                platformModelDtos.Add(new() { Id = Platforms[i].Id, Type = Platforms[i].Type });
            }

            return platformModelDtos;
        }
    }

    internal static List<Publisher> Publishers =>
    [
        new() { Id = new Guid("12345678-D5E1-4449-AF51-2CB10B025935"), CompanyName = "Blizzard" },
        new() { Id = new Guid("12345678-D5E1-4581-AF51-2CB10B025935"), CompanyName = "Activision" },
        new() { Id = new Guid("12345678-D5E1-2749-AF51-2CB10B025935"), CompanyName = "BioWare" },
    ];

    internal static List<PublisherModelDto> PublisherModelDtos
    {
        get
        {
            List<PublisherModelDto> publisherModelDtos = [];
            for (int i = 0; i < Genres.Count; i++)
            {
                publisherModelDtos.Add(new() { Id = Publishers[i].Id, CompanyName = Publishers[i].CompanyName });
            }

            return publisherModelDtos;
        }
    }

    internal static IMapper CreateMapperProfile()
    {
        var myProfile = new MappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

        return new Mapper(configuration);
    }

    internal static List<Game> GetGamesByGenreAsync(Guid id)
    {
        return Games.Where(x => x.GameGenres.Exists(x => x.GenreId == id)).ToList();
    }

    internal static List<Game> GetGamesByPlatformAsync(Guid id)
    {
        return Games.Where(x => x.GamePlatforms.Exists(x => x.PlatformId == id)).ToList();
    }

    internal static List<Game> GetGamesByPublisherAsync(Guid id)
    {
        return Games.Where(x => x.Publisher.Id == id).ToList();
    }

    internal static List<Genre> GetGenresByGameAsync(Guid id)
    {
        var gameGernes = GameGenres.Where(x => x.GameId == id).ToList();
        var genres = new List<Genre>();

        foreach (var gameGenre in gameGernes)
        {
            genres.Add(Genres.First(x => x.Id == gameGenre.GenreId));
        }

        return genres;
    }

    internal static List<Platform> GetPlatformsByGameAsync(Guid id)
    {
        var gamePlatforms = GamePlatforms.Where(x => x.GameId == id).ToList();
        var platforms = new List<Platform>();

        foreach (var gamePlatform in gamePlatforms)
        {
            platforms.Add(Platforms.First(x => x.Id == gamePlatform.PlatformId));
        }

        return platforms;
    }
}
