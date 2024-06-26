using Gamestore.BLL.BanHandler;
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

    public virtual async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, List<Guid> genresFilter, List<Guid> platformsFilter, List<Guid> publishersFilter)
    {
        if (_nextHandler != null)
        {
            return await _nextHandler.HandleAsync(unitOfWork, filteredGames, genresFilter, platformsFilter, publishersFilter);
        }

        return filteredGames;
    }
}
