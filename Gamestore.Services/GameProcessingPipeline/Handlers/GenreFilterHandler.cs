using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class GenreFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters)
    {
        if (filters.Genres.Count == 0)
        {
            await SelectAllGenres(unitOfWork, filters);
        }

        List<Game> gamesByGenres = [];
        foreach (var genreId in filters.Genres)
        {
            gamesByGenres.AddRange(await unitOfWork.GenreRepository.GetGamesByGenreAsync(genreId));
        }

        filteredGames.AddRange(gamesByGenres);

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }

    private static async Task SelectAllGenres(IUnitOfWork unitOfWork, GameFiltersDto filters)
    {
        filters.Genres.AddRange((await unitOfWork.GenreRepository.GetAllAsync()).Select(x => x.Id));
    }
}
