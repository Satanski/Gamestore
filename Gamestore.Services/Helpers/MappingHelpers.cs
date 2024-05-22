﻿using Gamestore.Repository.Entities;
using Gamestore.Services.Models;

namespace Gamestore.Services.Helpers;

internal static class MappingHelpers
{
    internal static GameModel CreateGameModel(Game game)
    {
        return new GameModel()
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description ?? string.Empty,
            Key = game.Key,
        };
    }

    internal static Game CreateGame(GameModel gameModel)
    {
        return new Game()
        {
            Id = gameModel.Id,
            Description = gameModel.Description ?? string.Empty,
            Key = gameModel.Key,
            Name = gameModel.Name,
        };
    }

    internal static Game CreateDetailedGame(DetailedGameModel gameModel)
    {
        var gameGenres = new List<GameGenre>();
        var gamePlatforms = new List<GamePlatform>();

        foreach (var genre in gameModel.Genres)
        {
            gameGenres.Add(new GameGenre() { GameId = gameModel.Id, GenreId = genre });
        }

        foreach (var platform in gameModel.Plaftorms)
        {
            gamePlatforms.Add(new GamePlatform() { GameId = gameModel.Id, Platform = platform });
        }

        return new Game()
        {
            Id = gameModel.Id,
            Description = gameModel.Description ?? string.Empty,
            Key = gameModel.Key,
            Name = gameModel.Name,
            GameGenres = gameGenres,
            GamePlatforms = gamePlatforms,
        };
    }

    internal static Platform CreatePlatform(PlatformModel platformModel)
    {
        Platform platform = new Platform()
        {
            Type = platformModel.Type,
        };

        return platform;
    }

    internal static Platform CreateDetailedPlatform(DetailedPlatformModel platformModel)
    {
        Platform platform = new Platform()
        {
            Id = platformModel.Id,
            Type = platformModel.Type,
        };

        return platform;
    }

    internal static PlatformModel CreatePlatformModel(Platform platform)
    {
        PlatformModel platformModel = new PlatformModel()
        {
            Type = platform.Type,
        };

        return platformModel;
    }
}
