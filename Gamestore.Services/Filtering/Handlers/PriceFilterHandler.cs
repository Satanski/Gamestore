using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PriceFilterHandler : FilterHandlerBase, IPriceFilterHandler
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters)
    {
        if (filters.MaxPrice < filters.MinPrice)
        {
            throw new GamestoreException("Max price should be larger then Min price");
        }

        if (filters.MinPrice != null)
        {
            filteredGames = filteredGames.Where(x => x.Price >= filters.MinPrice).ToList();
        }

        if (filters.MaxPrice != null)
        {
            filteredGames = filteredGames.Where(x => x.Price <= filters.MaxPrice).ToList();
        }

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }
}
