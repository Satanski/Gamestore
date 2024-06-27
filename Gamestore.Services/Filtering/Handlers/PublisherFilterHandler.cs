using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PublisherFilterHandler : FilterHandlerBase, IPublisherFilterHandler
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters)
    {
        List<Game> gamesByPublishers = [];
        foreach (var publisherId in filters.PublishersFilter)
        {
            gamesByPublishers.AddRange(await unitOfWork.PublisherRepository.GetGamesByPublisherIdAsync(publisherId));
        }

        filteredGames = filteredGames.Union(gamesByPublishers).ToList();

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }
}
