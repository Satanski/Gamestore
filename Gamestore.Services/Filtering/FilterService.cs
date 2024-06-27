using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class FilterService(IFilterHandler handlerChain) : IFilterService
{
    public async Task<List<Game>> FilterGames(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters)
    {
        return await handlerChain.HandleAsync(unitOfWork, filteredGames, filters);
    }
}
