using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class GenreFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Product>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Product> query)
    {
        if (filters.Genres.Count != 0)
        {
            query = query.Where(game => game.ProductCategories.Any(gp => filters.Genres.Contains(gp.CategoryId)));
        }

        query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);

        return query;
    }
}
