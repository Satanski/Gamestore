using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PlatformFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters)
    {
        if (filters.PlatformsFilter.Count == 0 && filteredGames.Count == 0)
        {
            await SelectAllPLatforms(unitOfWork, filters);
        }

        List<Game> gamesByPlatforms = [];
        foreach (var platformId in filters.PlatformsFilter)
        {
            gamesByPlatforms.AddRange(await unitOfWork.PlatformRepository.GetGamesByPlatformAsync(platformId));
        }

        filteredGames = filteredGames.Union(gamesByPlatforms).ToList();

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }

    private static async Task SelectAllPLatforms(IUnitOfWork unitOfWork, GameFiltersDto filters)
    {
        filters.PlatformsFilter.AddRange((await unitOfWork.PlatformRepository.GetAllAsync()).Select(x => x.Id));
    }
}
