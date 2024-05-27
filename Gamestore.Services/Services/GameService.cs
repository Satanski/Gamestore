using AutoMapper;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Helpers;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Validation;

namespace Gamestore.Services.Services;

public class GameService(IUnitOfWork unitOfWork, IMapper automapper) : IGameService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _automapper = automapper;

    public async Task<IEnumerable<GameModel>> GetAllGamesAsync()
    {
        var games = await _unitOfWork.GameRepository.GetAllAsync();
        List<GameModel> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(_automapper.Map<GameModel>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task<IEnumerable<GenreModelDto>> GetGenresByGameAsync(Guid gameId)
    {
        var genres = await _unitOfWork.GameRepository.GetGenresByGameAsync(gameId);
        List<GenreModelDto> genreModels = [];

        foreach (var genre in genres)
        {
            genreModels.Add(_automapper.Map<GenreModelDto>(genre));
        }

        return genreModels.AsEnumerable();
    }

    public async Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameAsync(Guid gameId)
    {
        var platforms = await _unitOfWork.GameRepository.GetPlatformsByGameAsync(gameId);
        List<PlatformModelDto> platformModels = [];

        foreach (var platform in platforms)
        {
            platformModels.Add(_automapper.Map<PlatformModelDto>(platform));
        }

        return platformModels.AsEnumerable();
    }

    public async Task<GameModel> GetGameByIdAsync(Guid gameId)
    {
        var game = await _unitOfWork.GameRepository.GetByIdAsync(gameId);

        return game == null ? throw new GamestoreException($"No game found with given id: {gameId}") : _automapper.Map<GameModel>(game);
    }

    public async Task<GameModel> GetGameByKeyAsync(string key)
    {
        var game = await _unitOfWork.GameRepository.GetGameByKeyAsync(key);

        return game == null ? throw new GamestoreException($"No game found with given key: {key}") : _automapper.Map<GameModel>(game);
    }

    public async Task AddGameAsync(GameModelDto gameModel)
    {
        ValidationHelpers.ValidateDetailedGameModel(gameModel);
        var game = _automapper.Map<Game>(gameModel);

        foreach (var item in gameModel.GameGenres)
        {
            await _unitOfWork.GameGenreRepository.AddAsync(_automapper.Map<GameGenre>(item));
        }

        foreach (var item in gameModel.GamePlaftorms)
        {
            await _unitOfWork.GamePlatformRepository.AddAsync(_automapper.Map<GamePlatform>(item));
        }

        await _unitOfWork.GameRepository.AddAsync(game);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateGameAsync(GameModelDto gameModel)
    {
        ValidationHelpers.ValidateDetailedGameModel(gameModel);
        var game = _automapper.Map<Game>(gameModel);

        var genres = await _unitOfWork.GameGenreRepository.GetByGameIdAsync(game.Id);
        foreach (var item in genres)
        {
            _unitOfWork.GameGenreRepository.Delete(item);
        }

        var platforms = await _unitOfWork.GamePlatformRepository.GetByGameIdAsync(game.Id);
        foreach (var item in platforms)
        {
            _unitOfWork.GamePlatformRepository.Delete(item);
        }

        await _unitOfWork.SaveAsync();

        foreach (var item in game.GameGenres)
        {
            await _unitOfWork.GameGenreRepository.AddAsync(item);
        }

        foreach (var item in game.GamePlatforms)
        {
            await _unitOfWork.GamePlatformRepository.AddAsync(item);
        }

        await _unitOfWork.GameRepository.UpdateAsync(game);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteGameAsync(Guid gameId)
    {
        var game = await _unitOfWork.GameRepository.GetByIdAsync(gameId);

        if (game != null)
        {
            _unitOfWork.GameRepository.Delete(game);

            var genres = await _unitOfWork.GameGenreRepository.GetByGameIdAsync(game.Id);
            foreach (var item in genres)
            {
                _unitOfWork.GameGenreRepository.Delete(item);
            }

            var platforms = await _unitOfWork.GamePlatformRepository.GetByGameIdAsync(game.Id);
            foreach (var item in platforms)
            {
                _unitOfWork.GamePlatformRepository.Delete(item);
            }

            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new GamestoreException($"No game found with given id: {gameId}");
        }
    }
}
