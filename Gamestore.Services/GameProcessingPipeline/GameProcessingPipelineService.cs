using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class GameProcessingPipelineService(IGameProcessingPipelineHandler handlerChain) : IGameProcessingPipelineService
{
    public Task<IQueryable<Game>> ProcessGamesAsync(IUnitOfWork unitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        return handlerChain.HandleAsync(unitOfWork, filters, query);
    }
}
