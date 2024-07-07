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

#pragma warning disable CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
#pragma warning disable CA1311 // Specify a culture or use an invariant version
            query = query.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
#pragma warning restore CA1311 // Specify a culture or use an invariant version
#pragma warning restore CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
        }

        query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);

        return query;
    }
}
