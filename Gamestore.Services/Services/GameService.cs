using Gamestore.DAL.Interfaces;
using Gamestore.Services.Helpers;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Validation;

namespace Gamestore.Services.Services;

public class GameService(IUnitOfWork unitOfWork) : IGameService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<GameModel>> GetAllGamesAsync()
    {
        var games = await _unitOfWork.GameRepository.GetAllAsync();
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
    }

    public async Task<IEnumerable<DetailedGenreModel>> GetGenresByGameAsync(Guid gameId)
    {
        var genres = await _unitOfWork.GameRepository.GetGenresByGameAsync(gameId);
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
    }

    public async Task<IEnumerable<DetailedPlatformModel>> GetPlatformsByGameAsync(Guid gameId)
    {
        var platforms = await _unitOfWork.GameRepository.GetPlatformsByGameAsync(gameId);
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
    }

    public async Task<GameModel> GetGameByIdAsync(Guid gameId)
    {
        var game = await _unitOfWork.GameRepository.GetByIdAsync(gameId);

        return game == null ? throw new GamestoreException($"No game found with given id: {gameId}") : MappingHelpers.CreateGameModel(game);
    }

    public async Task<GameModel> GetGameByKeyAsync(string key)
    {
        var game = await _unitOfWork.GameRepository.GetGameByKeyAsync(key);

        return game == null ? throw new GamestoreException($"No game found with given key: {key}") : MappingHelpers.CreateGameModel(game);
    }

    public async Task AddGameAsync(DetailedGameModel gameModel)
    {
        ValidationHelpers.ValidateDetailedGameModel(gameModel);
        var game = MappingHelpers.CreateDetailedGame(gameModel);

        await _unitOfWork.GameRepository.AddAsync(game);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateGameAsync(DetailedGameModel gameModel)
    {
        ValidationHelpers.ValidateDetailedGameModel(gameModel);
        var game = MappingHelpers.CreateDetailedGame(gameModel);

        await _unitOfWork.GameRepository.UpdateAsync(game);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteGameAsync(Guid gameId)
    {
        await _unitOfWork.GameRepository.Delete(gameId);
        await _unitOfWork.SaveAsync();
    }
}
