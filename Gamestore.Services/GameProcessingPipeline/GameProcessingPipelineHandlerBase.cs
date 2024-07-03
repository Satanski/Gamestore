using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class GameProcessingPipelineHandlerBase : IGameProcessingPipelineHandler
{
    private IGameProcessingPipelineHandler _nextHandler;

    public void SetNext(IGameProcessingPipelineHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public virtual async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters)
    {
        if (_nextHandler != null)
        {
            return await _nextHandler.HandleAsync(unitOfWork, filteredGames, filters);
        }

        return filteredGames;
    }
}
