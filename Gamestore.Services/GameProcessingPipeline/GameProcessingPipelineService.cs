using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class GameProcessingPipelineService(IGameProcessingPipelineHandler handlerChain) : IGameProcessingPipelineService
{
    public Task<List<Game>> ProcessGamesAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters)
    {
        return handlerChain.HandleAsync(unitOfWork, filteredGames, filters);
    }
}
