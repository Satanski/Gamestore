using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PlatformFilterHandler : FilterHandlerBase, IPlatformFilterHandler
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters)
    {
        List<Game> gamesByPlatforms = [];
        foreach (var platformId in filters.PlatformsFilter)
        {
            gamesByPlatforms.AddRange(await unitOfWork.PlatformRepository.GetGamesByPlatformAsync(platformId));
        }

        filteredGames = filteredGames.Union(gamesByPlatforms).ToList();

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }
}
