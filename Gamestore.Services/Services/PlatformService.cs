using Gamestore.DAL.Interfaces;
using Gamestore.Services.Helpers;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Validation;

namespace Gamestore.Services.Services;

public class PlatformService(IUnitOfWork unitOfWork) : IPlatformService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<GameModel>> GetGamesByPlatformAsync(Guid platformId)
    {
        var games = await _unitOfWork.PlatformRepository.GetGamesByPlatformAsync(platformId);

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

    public async Task AddPlatformAsync(PlatformModel platformModel)
    {
        ValidationHelpers.ValidatePlatformModel(platformModel);
        var platform = MappingHelpers.CreatePlatform(platformModel);

        await _unitOfWork.PlatformRepository.AddAsync(platform);
        await _unitOfWork.SaveAsync();
    }

    public async Task<PlatformModel> GetPlatformByIdAsync(Guid platformId)
    {
        var platform = await _unitOfWork.PlatformRepository.GetByIdAsync(platformId);

        return platform == null ? throw new GamestoreException($"No platform found with given id: {platformId}") : MappingHelpers.CreatePlatformModel(platform);
    }

    public async Task<IEnumerable<PlatformModel>> GetAllPlatformsAsync()
    {
        var platforms = await _unitOfWork.PlatformRepository.GetAllAsync();
        List<PlatformModel> platformModels = [];

        if (platforms.Count == 0)
        {
            throw new GamestoreException("No platforms found");
        }

        foreach (var platform in platforms)
        {
            platformModels.Add(MappingHelpers.CreatePlatformModel(platform));
        }

        return platformModels.AsEnumerable();
    }

    public async Task UpdatePlatformAsync(DetailedPlatformModel platformModel)
    {
        ValidationHelpers.ValidateDetailedPlatformModel(platformModel);
        var platform = MappingHelpers.CreateDetailedPlatform(platformModel);

        await _unitOfWork.PlatformRepository.UpdateAsync(platform);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeletePlatformAsync(Guid platformId)
    {
        await _unitOfWork.PlatformRepository.DeleteAsync(platformId);
    }
}
