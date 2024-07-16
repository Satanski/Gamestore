using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PlatformFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Product>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Product> query)
    {
        if (filters.Platforms.Count == 0)
        {
            await SelectAllPLatforms(unitOfWork, filters);
        }

        query = query.Where(game => game.ProductPlatforms.Any(gp => filters.Platforms.Contains(gp.PlatformId)));
        query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);

        return query;
    }

    private static async Task SelectAllPLatforms(IUnitOfWork unitOfWork, GameFiltersDto filters)
    {
        filters.Platforms.AddRange((await unitOfWork.PlatformRepository.GetAllAsync()).Select(x => x.Id));
    }
}
