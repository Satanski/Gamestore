using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class GenreFilterHandler : FilterHandlerBase, IGenreFilterHandler
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters)
    {
        if (filters.GenresFilter.Count == 0 && filters.PlatformsFilter.Count == 0 && filters.PublishersFilter.Count == 0)
        {
            filters.GenresFilter.AddRange((await unitOfWork.GenreRepository.GetAllAsync()).Select(x => x.Id));
        }

        List<Game> gamesByGenres = [];
        foreach (var genreId in filters.GenresFilter)
        {
            gamesByGenres.AddRange(await unitOfWork.GenreRepository.GetGamesByGenreAsync(genreId));
        }

        filteredGames.AddRange(gamesByGenres);

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }
}
