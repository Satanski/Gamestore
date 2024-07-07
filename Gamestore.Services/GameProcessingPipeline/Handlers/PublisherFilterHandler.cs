using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Helpers;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PublisherFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        if (filters.Publishers.Count == 0)
        {
            await SelectAllPublishers(unitOfWork, mongoUnitOfWork, filters);
        }

        query = query.Where(game => filters.Publishers.Contains(game.Publisher.Id));
        query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);

        return query;
    }

    private static async Task SelectAllPublishers(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters)
    {
        filters.Publishers.AddRange((await unitOfWork.PublisherRepository.GetAllAsync()).Select(x => x.Id));
        var supplierIds = (await mongoUnitOfWork.SupplierRepository.GetAllAsync()).Select(x => x.SupplierID).ToList();
        var supplierGuids = ConvertIdsToGuids(supplierIds);
        filters.Publishers.AddRange(supplierGuids);
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
