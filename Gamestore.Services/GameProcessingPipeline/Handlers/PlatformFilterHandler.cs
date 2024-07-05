using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PlatformFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        if (filters.Platforms.Count == 0)
        {
            await SelectAllPLatforms(unitOfWork, filters);
        }

        query = query.Where(game => game.GamePlatforms.Any(gp => filters.Platforms.Contains(gp.PlatformId)));
        query = await base.HandleAsync(unitOfWork, filters, query);

        return query;
    }

    private static async Task SelectAllPLatforms(IUnitOfWork unitOfWork, GameFiltersDto filters)
    {
        filters.Platforms.AddRange((await unitOfWork.PlatformRepository.GetAllAsync()).Select(x => x.Id));
    }
}
