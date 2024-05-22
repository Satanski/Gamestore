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

    public static void ValidateDetailedGameModel(DetailedGameModel gameModel)
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

    internal static void ValidateDetailedPlatformModel(DetailedPlatformModel platformModel)
    {
        if (platformModel == null || string.IsNullOrEmpty(platformModel.Type) || platformModel.Id == Guid.Empty)
        {
            throw new GamestoreException("Platform model invalid");
        }
    }
}
