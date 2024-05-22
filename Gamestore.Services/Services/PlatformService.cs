using Gamestore.Repository.Interfaces;
using Gamestore.Services.Helpers;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Validation;

namespace Gamestore.Services.Services;

public class PlatformService(IUnitOfWork unitOfWork) : IPlatformService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<IEnumerable<GameModel>> GetGamesByPlatformAsync(Guid platformId)
    {
        var task = Task.Run(() => _unitOfWork.PlatformRepository.GetGamesByPlatformAsync(platformId))
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

    public Task AddPlatformAsync(PlatformModel platformModel)
    {
        ValidationHelpers.ValidatePlatformModel(platformModel);
        var platform = MappingHelpers.CreatePlatform(platformModel);

        var task = Task.Run(() => _unitOfWork.PlatformRepository.AddPlatformAsync(platform));

        return task;
    }

    public Task<PlatformModel> GetPlatformByIdAsync(Guid id)
    {
        var task = Task.Run(() => _unitOfWork.PlatformRepository.GetPlatformByIdAsync(id))
            .ContinueWith(x =>
            {
                var platform = x.Result;

                return platform == null ? throw new GamestoreException($"No platform found with given id: {id}") : MappingHelpers.CreatePlatformModel(platform);
            });

        return task;
    }

    public Task<IEnumerable<PlatformModel>> GetAllPlatformsAsync()
    {
        var task = Task.Run(() => _unitOfWork.PlatformRepository.GetAllPlatformsAsync())
            .ContinueWith(x =>
            {
                var platforms = x.Result.ToList();
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
            });

        return task;
    }

    public Task UpdatePlatformAsync(DetailedPlatformModel platformModel)
    {
        ValidationHelpers.ValidateDetailedPlatformModel(platformModel);
        var platform = MappingHelpers.CreateDetailedPlatform(platformModel);

        var task = Task.Run(() => _unitOfWork.PlatformRepository.UpdatePlatformAsync(platform));

        return task;
    }

    public Task DeletePlatformAsync(Guid id)
    {
        var task = Task.Run(() => _unitOfWork.PlatformRepository.DeletePlatformAsync(id));

        return task;
    }
}
