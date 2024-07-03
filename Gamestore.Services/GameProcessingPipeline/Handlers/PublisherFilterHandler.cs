using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PublisherFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters)
    {
        if (filters.PublishersFilter.Count == 0 && filteredGames.Count == 0)
        {
            await SelectAllPublishers(unitOfWork, filters);
        }

        List<Game> gamesByPublishers = [];
        foreach (var publisherId in filters.PublishersFilter)
        {
            gamesByPublishers.AddRange(await unitOfWork.PublisherRepository.GetGamesByPublisherIdAsync(publisherId));
        }

        filteredGames = filteredGames.Union(gamesByPublishers).ToList();

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }

    private static async Task SelectAllPublishers(IUnitOfWork unitOfWork, GameFiltersDto filters)
    {
        filters.PublishersFilter.AddRange((await unitOfWork.PublisherRepository.GetAllAsync()).Select(x => x.Id));
    }
}
