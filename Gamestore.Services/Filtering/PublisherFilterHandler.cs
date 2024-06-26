using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class PublisherFilterHandler : FilterHandlerBase
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, List<Guid> genresFilter, List<Guid> platformsFilter, List<Guid> publishersFilter)
    {
        List<Game> gamesByPublishers = [];
        foreach (var publisherId in publishersFilter)
        {
            gamesByPublishers.AddRange(await unitOfWork.PublisherRepository.GetGamesByPublisherIdAsync(publisherId));
        }

        filteredGames = filteredGames.Union(gamesByPublishers).ToList();

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, genresFilter, platformsFilter, publishersFilter);

        return filteredGames;
    }
}
