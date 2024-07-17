using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Helpers;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering;

public class GameProcessingPipelineHandlerBase : IGameProcessingPipelineHandler
{
    private IGameProcessingPipelineHandler _nextHandler;

    public void SetNext(IGameProcessingPipelineHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public virtual async Task<IQueryable<Product>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Product> query)
    {
        if (_nextHandler != null)
        {
            return await _nextHandler.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);
        }

        return query;
    }

    protected static List<Guid> ConvertIdsToGuids(List<int> ids)
    {
        List<Guid> guids = [];

        foreach (var id in ids)
        {
            guids.Add(GuidHelpers.IntToGuid(id));
        }

        return guids;
    }
}
