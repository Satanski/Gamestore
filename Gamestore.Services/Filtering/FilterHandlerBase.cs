using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class FilterHandlerBase : IFilterHandler
{
    private IFilterHandler _nextHandler;

    public void SetNext(IFilterHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public virtual async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters)
    {
        if (_nextHandler != null)
        {
            return await _nextHandler.HandleAsync(unitOfWork, filteredGames, filters);
        }

        return filteredGames;
    }
}
