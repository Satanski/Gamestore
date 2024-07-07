using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering;

public class GameProcessingPipelineHandlerBase : IGameProcessingPipelineHandler
{
    private IGameProcessingPipelineHandler _nextHandler;

    public void SetNext(IGameProcessingPipelineHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public virtual async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        if (_nextHandler != null)
        {
            return await _nextHandler.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);
        }

        return query;
    }
}
