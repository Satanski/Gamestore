using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.Extensions.Logging;

namespace Gamestore.Services.Services;

public class PlatformService(IUnitOfWork unitOfWork, IMapper automapper, ILogger<PlatformService> logger) : IPlatformService
{
    private readonly PlatformModelValidator _platformModelValidator = new(unitOfWork);
    private readonly PlatformModelDtoValidator _platformModelDtoValidator = new(unitOfWork);

    public async Task<IEnumerable<GameModel>> GetGamesByPlatformIdAsync(Guid platformId)
    {
        logger.LogInformation("Getting games by platform id: {platformId}", platformId);
        var games = await unitOfWork.PlatformRepository.GetGamesByPlatformAsync(platformId);

        List<GameModel> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(automapper.Map<GameModel>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task AddPlatformAsync(PlatformModel platformModel)
    {
        logger.LogInformation("Adding platform: {@platformModel}", platformModel);
        var result = await _platformModelValidator.ValidateAsync(platformModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var platform = automapper.Map<Platform>(platformModel);

        await unitOfWork.PlatformRepository.AddAsync(platform);
        await unitOfWork.SaveAsync();
    }

    public async Task<PlatformModel> GetPlatformByIdAsync(Guid platformId)
    {
        logger.LogInformation("Getting platform by id: {platformId}", platformId);
        var platform = await unitOfWork.PlatformRepository.GetByIdAsync(platformId);

        return platform == null ? throw new GamestoreException($"No platform found with given id: {platformId}") : automapper.Map<PlatformModel>(platform);
    }

    public async Task<IEnumerable<PlatformModel>> GetAllPlatformsAsync()
    {
        logger.LogInformation("Getting all platforms");
        var platforms = await unitOfWork.PlatformRepository.GetAllAsync();
        List<PlatformModel> platformModels = [];

        foreach (var platform in platforms)
        {
            platformModels.Add(automapper.Map<PlatformModel>(platform));
        }

        return platformModels.AsEnumerable();
    }

    public async Task UpdatePlatformAsync(PlatformModelDto platformModel)
    {
        logger.LogInformation("Updating platform: {@platformModel}", platformModel);
        var result = await _platformModelDtoValidator.ValidateAsync(platformModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var platform = automapper.Map<Platform>(platformModel);

        await unitOfWork.PlatformRepository.UpdateAsync(platform);
        await unitOfWork.SaveAsync();
    }

    public async Task DeletePlatformByIdAsync(Guid platformId)
    {
        logger.LogInformation("Deleting platform by id: {platformId}", platformId);
        var platform = await unitOfWork.PlatformRepository.GetByIdAsync(platformId);
        if (platform != null)
        {
            unitOfWork.PlatformRepository.Delete(platform);
            await unitOfWork.SaveAsync();
        }
        else
        {
            throw new GamestoreException($"No platform found with given id: {platformId}");
        }
    }
}
