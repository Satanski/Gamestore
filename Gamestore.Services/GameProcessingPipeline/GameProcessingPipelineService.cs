using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering;

public class GameProcessingPipelineService(IGameProcessingPipelineHandler handlerChain) : IGameProcessingPipelineService
{
    public Task<IQueryable<Game>> ProcessGamesAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        return handlerChain.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);
    }
}
