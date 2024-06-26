using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class GenreFilterHandler : FilterHandlerBase
{
    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, List<Guid> genresFilter, List<Guid> platformsFilter, List<Guid> publishersFilter)
    {
        if (genresFilter.Count == 0 && platformsFilter.Count == 0 && publishersFilter.Count == 0)
        {
            genresFilter.AddRange((await unitOfWork.GenreRepository.GetAllAsync()).Select(x => x.Id));
        }

        List<Game> gamesByGenres = [];
        foreach (var genreId in genresFilter)
        {
            gamesByGenres.AddRange(await unitOfWork.GenreRepository.GetGamesByGenreAsync(genreId));
        }

        filteredGames.AddRange(gamesByGenres);

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, genresFilter, platformsFilter, publishersFilter);

        return filteredGames;
    }
}
