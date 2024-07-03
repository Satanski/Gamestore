using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class NameFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters)
    {
        if (filters.Name is not null)
        {
            if (filters.Name.Length < 3)
            {
                throw new GamestoreException("Name should be at least 3 characters long");
            }

            filteredGames = [.. filteredGames.Where(x => x.Name.Contains(filters.Name, StringComparison.CurrentCultureIgnoreCase))];
        }

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }
}
