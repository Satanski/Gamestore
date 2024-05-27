using AutoMapper;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Helpers;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Validation;

namespace Gamestore.Services.Services;

public class PlatformService(IUnitOfWork unitOfWork, IMapper automapper) : IPlatformService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _automapper = automapper;

    public async Task<IEnumerable<GameModel>> GetGamesByPlatformAsync(Guid platformId)
    {
        var games = await _unitOfWork.PlatformRepository.GetGamesByPlatformAsync(platformId);

        List<GameModel> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(_automapper.Map<GameModel>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task AddPlatformAsync(PlatformModel platformModel)
    {
        ValidationHelpers.ValidatePlatformModel(platformModel);
        var platform = _automapper.Map<Platform>(platformModel);

        await _unitOfWork.PlatformRepository.AddAsync(platform);
        await _unitOfWork.SaveAsync();
    }

    public async Task<PlatformModel> GetPlatformByIdAsync(Guid platformId)
    {
        var platform = await _unitOfWork.PlatformRepository.GetByIdAsync(platformId);

        return platform == null ? throw new GamestoreException($"No platform found with given id: {platformId}") : _automapper.Map<PlatformModel>(platform);
    }

    public async Task<IEnumerable<PlatformModel>> GetAllPlatformsAsync()
    {
        var platforms = await _unitOfWork.PlatformRepository.GetAllAsync();
        List<PlatformModel> platformModels = [];

        foreach (var platform in platforms)
        {
            platformModels.Add(_automapper.Map<PlatformModel>(platform));
        }

        return platformModels.AsEnumerable();
    }

    public async Task UpdatePlatformAsync(PlatformModelDto platformModel)
    {
        ValidationHelpers.ValidateDetailedPlatformModel(platformModel);
        var platform = _automapper.Map<Platform>(platformModel);

        await _unitOfWork.PlatformRepository.UpdateAsync(platform);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeletePlatformAsync(Guid platformId)
    {
        var platform = await _unitOfWork.PlatformRepository.GetByIdAsync(platformId);
        if (platform != null)
        {
            _unitOfWork.PlatformRepository.Delete(platform);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new GamestoreException($"No platform found with given id: {platformId}");
        }
    }
}
