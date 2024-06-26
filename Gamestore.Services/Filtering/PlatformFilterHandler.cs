using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class PlatformFilterHandler : FilterHandlerBase
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, List<Guid> genresFilter, List<Guid> platformsFilter, List<Guid> publishersFilter)
    {
        List<Game> gamesByPlatforms = [];
        foreach (var platformId in platformsFilter)
        {
            gamesByPlatforms.AddRange(await unitOfWork.PlatformRepository.GetGamesByPlatformAsync(platformId));
        }

        filteredGames = filteredGames.Union(gamesByPlatforms).ToList();

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, genresFilter, platformsFilter, publishersFilter);

        return filteredGames;
    }
}
