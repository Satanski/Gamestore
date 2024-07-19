using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class NameFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        if (filters.Name is not null)
        {
            if (filters.Name.Length < 3)
            {
                throw new GamestoreException("Name should be at least 3 characters long");
            }

            query = query.Where(x => x.Name.Contains(filters.Name));
        }

        query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);

        return query;
    }
}
