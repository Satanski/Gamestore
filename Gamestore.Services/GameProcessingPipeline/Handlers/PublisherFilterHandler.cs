using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PublisherFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Product>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Product> query)
    {
        if (filters.Publishers.Count != 0)
        {
            query = query.Where(game => filters.Publishers.Contains(game.Publisher.Id));
        }

        query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);

        return query;
    }
}
