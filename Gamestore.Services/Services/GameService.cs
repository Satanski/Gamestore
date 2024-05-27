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

        foreach (var game in games)
        {
            gameModels.Add(MappingHelpers.CreateGameModel(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task<IEnumerable<GenreModelDto>> GetGenresByGameAsync(Guid gameId)
    {
        var genres = await _unitOfWork.GameRepository.GetGenresByGameAsync(gameId);
        List<GenreModelDto> genreModels = [];

        foreach (var genre in genres)
        {
            genreModels.Add(MappingHelpers.CreateDetailedGenreModel(genre));
        }

        return genreModels.AsEnumerable();
    }

    public async Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameAsync(Guid gameId)
    {
        var platforms = await _unitOfWork.GameRepository.GetPlatformsByGameAsync(gameId);
        List<PlatformModelDto> platformModels = [];

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

    public async Task AddGameAsync(GameModelDto gameModel)
    {
        ValidationHelpers.ValidateDetailedGameModel(gameModel);
        var game = MappingHelpers.CreateDetailedGame(gameModel);

        await _unitOfWork.GameRepository.AddAsync(game);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateGameAsync(GameModelDto gameModel)
    {
        ValidationHelpers.ValidateDetailedGameModel(gameModel);
        var game = MappingHelpers.CreateDetailedGame(gameModel);

        await _unitOfWork.GameRepository.UpdateAsync(game);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteGameAsync(Guid gameId)
    {
        await _unitOfWork.GameRepository.DeleteAsync(gameId);
        await _unitOfWork.SaveAsync();
    }
}
