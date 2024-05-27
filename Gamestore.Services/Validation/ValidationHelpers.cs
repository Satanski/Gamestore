using Gamestore.Services.Models;
using Gamestore.Services.Validation;

namespace Gamestore.Services.Helpers;

internal static class ValidationHelpers
{
    public static void ValidateGameModel(GameModel gameModel)
    {
        if (gameModel == null || gameModel.Id == Guid.Empty || string.IsNullOrEmpty(gameModel.Name) || string.IsNullOrEmpty(gameModel.Key))
        {
            throw new GamestoreException("Game model invalid");
        }
    }

    public static void ValidateDetailedGameModel(GameModelDto gameModel)
    {
        if (gameModel == null || gameModel.Id == Guid.Empty || string.IsNullOrEmpty(gameModel.Name) || string.IsNullOrEmpty(gameModel.Key)
            || gameModel.Plaftorms.Count == 0 || gameModel.Genres.Count == 0)
        {
            throw new GamestoreException("Detailed game model invalid");
        }
    }

    internal static void ValidatePlatformModel(PlatformModel platformModel)
    {
        if (platformModel == null || string.IsNullOrEmpty(platformModel.Type))
        {
            throw new GamestoreException("Platform model invalid");
        }
    }

    internal static void ValidateDetailedPlatformModel(PlatformModelDto platformModel)
    {
        if (platformModel == null || string.IsNullOrEmpty(platformModel.Type) || platformModel.Id == Guid.Empty)
        {
            throw new GamestoreException("Platform model invalid");
        }
    }

    internal static void ValidateGenreModel(GenreModel genreModel)
    {
        if (genreModel == null || string.IsNullOrEmpty(genreModel.Name))
        {
            throw new GamestoreException("Genre model invalid");
        }
    }

    internal static void ValidateDetailedGenreModel(GenreModelDto genreModel)
    {
        if (genreModel == null || string.IsNullOrEmpty(genreModel.Name) || genreModel.Id == Guid.Empty)
        {
            throw new GamestoreException("Genre model invalid");
        }
    }
}
