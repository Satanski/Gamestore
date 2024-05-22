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

    public Task<GameModel> GetGameByIdAsync(Guid id)
    {
        var task = Task.Run(() => _unitOfWork.GameRepository.GetGameByIdAsync(id))
            .ContinueWith(x =>
            {
                var game = x.Result;

                return game == null ? throw new GamestoreException($"No game found with given id: {id}") : MappingHelpers.CreateGameModel(game);
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

    public Task DeleteGameAsync(Guid id)
    {
        var task = Task.Run(() => _unitOfWork.GameRepository.DeleteGameAsync(id));

        return task;
    }
}
