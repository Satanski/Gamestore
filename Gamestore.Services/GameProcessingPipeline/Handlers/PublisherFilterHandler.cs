using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PublisherFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        if (filters.Publishers.Count == 0)
        {
            await SelectAllPublishers(unitOfWork, filters);
        }

        query = query.Where(game => filters.Publishers.Contains(game.Publisher.Id));
        query = await base.HandleAsync(unitOfWork, filters, query);

        return query;
    }

    private static async Task SelectAllPublishers(IUnitOfWork unitOfWork, GameFiltersDto filters)
    {
        filters.Publishers.AddRange((await unitOfWork.PublisherRepository.GetAllAsync()).Select(x => x.Id));
    }
}
