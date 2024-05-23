using Gamestore.Repository.Interfaces;
using Gamestore.Services.Helpers;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Validation;

namespace Gamestore.Services.Services;

public class GameService(IUnitOfWork unitOfWork) : IGameService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<IEnumerable<GameModel>> GetAllGamesAsync()
    {
        var task = Task.Run(() => _unitOfWork.GameRepository.GetAllGamesAsync())
            .ContinueWith(x =>
            {
                var games = x.Result.ToList();
                List<GameModel> gameModels = [];

                if (games.Count == 0)
                {
                    throw new GamestoreException("No games found");
                }

                foreach (var game in games)
                {
                    gameModels.Add(MappingHelpers.CreateGameModel(game));
                }

                return gameModels.AsEnumerable();
            });

        return task;
    }

    public Task<IEnumerable<DetailedGenreModel>> GetGenresByGameAsync(Guid gameId)
    {
        var task = Task.Run(() => _unitOfWork.GameRepository.GetGenresByGameAsync(gameId))
           .ContinueWith(x =>
           {
               var genres = x.Result.ToList();
               List<DetailedGenreModel> genreModels = [];

               if (genres.Count == 0)
               {
                   throw new GamestoreException("No genres found");
               }

               foreach (var genre in genres)
               {
                   genreModels.Add(MappingHelpers.CreateDetailedGenreModel(genre));
               }

               return genreModels.AsEnumerable();
           });

        return task;
    }

    public Task<IEnumerable<DetailedPlatformModel>> GetPlatformsByGameAsync(Guid gameId)
    {
        var task = Task.Run(() => _unitOfWork.GameRepository.GetPlatformsByGameAsync(gameId))
           .ContinueWith(x =>
           {
               var platforms = x.Result.ToList();
               List<DetailedPlatformModel> platformModels = [];

               if (platforms.Count == 0)
               {
                   throw new GamestoreException("No platforms found");
               }

               foreach (var platform in platforms)
               {
                   platformModels.Add(MappingHelpers.CreateDetailedPlatformModel(platform));
               }

               return platformModels.AsEnumerable();
           });

        return task;
    }

    public Task<GameModel> GetGameByIdAsync(Guid gameId)
    {
        var task = Task.Run(() => _unitOfWork.GameRepository.GetGameByIdAsync(gameId))
            .ContinueWith(x =>
            {
                var game = x.Result;

                return game == null ? throw new GamestoreException($"No game found with given id: {gameId}") : MappingHelpers.CreateGameModel(game);
            });

        return task;
    }

    public Task<GameModel> GetGameByKeyAsync(string key)
    {
        var task = Task.Run(() => _unitOfWork.GameRepository.GetGameByKeyAsync(key))
            .ContinueWith(x =>
            {
                var game = x.Result;

                return game == null ? throw new GamestoreException($"No game found with given key: {key}") : MappingHelpers.CreateGameModel(game);
            });

        return task;
    }

    public Task AddGameAsync(DetailedGameModel gameModel)
    {
        ValidationHelpers.ValidateDetailedGameModel(gameModel);
        var game = MappingHelpers.CreateDetailedGame(gameModel);

        var task = Task.Run(() => _unitOfWork.GameRepository.AddGameAsync(game));

        return task;
    }

    public Task UpdateGameAsync(DetailedGameModel gameModel)
    {
        ValidationHelpers.ValidateDetailedGameModel(gameModel);
        var game = MappingHelpers.CreateDetailedGame(gameModel);

        var task = Task.Run(() => _unitOfWork.GameRepository.UpdateGameAsync(game));

        return task;
    }

    public Task DeleteGameAsync(Guid gameId)
    {
        var task = Task.Run(() => _unitOfWork.GameRepository.DeleteGameAsync(gameId));

        return task;
    }
}
