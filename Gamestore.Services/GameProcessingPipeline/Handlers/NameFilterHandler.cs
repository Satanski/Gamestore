using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class NameFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        if (filters.Name is not null)
        {
            if (filters.Name.Length < 3)
            {
                throw new GamestoreException("Name should be at least 3 characters long");
            }

            query = query.Where(x => x.Name.Contains(filters.Name, StringComparison.CurrentCultureIgnoreCase));
        }

        query = await base.HandleAsync(unitOfWork, filters, query);

        return query;
    }
}
