using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PriceFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        if (filters.MaxPrice < filters.MinPrice)
        {
            throw new GamestoreException("Max price should be larger then Min price");
        }

        if (filters.MinPrice != null)
        {
            query = query.Where(x => x.Price >= filters.MinPrice);
        }

        if (filters.MaxPrice != null)
        {
            query = query.Where(x => x.Price <= filters.MaxPrice);
        }

        query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);

        return query;
    }
}
