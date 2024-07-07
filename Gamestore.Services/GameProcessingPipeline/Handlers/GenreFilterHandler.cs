using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Helpers;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class GenreFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        if (filters.Genres.Count == 0)
        {
            await SelectAllGenres(unitOfWork, mongoUnitOfWork, filters);
        }

        query = query.Where(game => game.GameGenres.Any(gp => filters.Genres.Contains(gp.GenreId)));
        query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);

        return query;
    }

    private static async Task SelectAllGenres(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters)
    {
        filters.Genres.AddRange((await unitOfWork.GenreRepository.GetAllAsync()).Select(x => x.Id));
        var categoryIds = (await mongoUnitOfWork.CategoryRepository.GetAllAsync()).Select(x => x.CategoryId).ToList();
        var categoryGuids = ConvertIdsToGuids(categoryIds);
        filters.Genres.AddRange(categoryGuids);
    }

    private static List<Guid> ConvertIdsToGuids(List<int> ids)
    {
        List<Guid> guids = [];

        foreach (var id in ids)
        {
            guids.Add(GuidHelpers.IntToGuid(id));
        }

        return guids;
    }
}
