using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Models;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.Extensions.Logging;

namespace Gamestore.Services.Services;

public class PlatformService(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, ILogger<PlatformService> logger) : IPlatformService
{
    private const string PhysicalProductType = "Physical Product";
    private readonly PlatformDtoWrapperValidator _platformDtoWrapperValidator = new(unitOfWork);

    public async Task<IEnumerable<GameModelDto>> GetGamesByPlatformIdAsync(Guid platformId)
    {
        logger.LogInformation("Getting games by platform id: {platformId}", platformId);
        var games = await unitOfWork.PlatformRepository.GetGamesByPlatformAsync(platformId);
        List<GameModelDto> gameModels = automapper.Map<List<GameModelDto>>(games);

        if (platformId == (await unitOfWork.PlatformRepository.GetByTypeAsync(PhysicalProductType)).Id)
        {
            var gamesFromMongoDB = automapper.Map<List<GameModelDto>>(await mongoUnitOfWork.ProductRepository.GetAllAsync()).Except(gameModels);
            gameModels.AddRange(gamesFromMongoDB);
        }

        return gameModels.AsEnumerable();
    }

    public async Task<PlatformModelDto> GetPlatformByIdAsync(Guid platformId)
    {
        logger.LogInformation("Getting platform by id: {platformId}", platformId);
        var platform = await unitOfWork.PlatformRepository.GetByIdAsync(platformId);

        return platform == null ? throw new GamestoreException($"No platform found with given id: {platformId}") : automapper.Map<PlatformModelDto>(platform);
    }

    public async Task<IEnumerable<PlatformModelDto>> GetAllPlatformsAsync()
    {
        logger.LogInformation("Getting all platforms");
        var platforms = await unitOfWork.PlatformRepository.GetAllAsync();
        List<PlatformModelDto> platformModels = automapper.Map<List<PlatformModelDto>>(platforms);

        return platformModels.AsEnumerable();
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

    public async Task AddPlatformAsync(PlatformDtoWrapper platformModel)
    {
        logger.LogInformation("Adding platform: {@platform}", platformModel);

        await _platformDtoWrapperValidator.ValidatePlatform(platformModel);

        var platform = automapper.Map<Platform>(platformModel.Platform);

        await unitOfWork.PlatformRepository.AddAsync(platform);
        await unitOfWork.SaveAsync();
    }

    public async Task UpdatePlatformAsync(PlatformDtoWrapper platformModel)
    {
        logger.LogInformation("Updating platform: {@platformModel}", platformModel);

        await _platformDtoWrapperValidator.ValidatePlatform(platformModel);

        var platform = automapper.Map<Platform>(platformModel.Platform);

        await unitOfWork.PlatformRepository.UpdateAsync(platform);
        await unitOfWork.SaveAsync();
    }
}
