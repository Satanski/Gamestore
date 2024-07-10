using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class GenreFilterHandler : GameProcessingPipelineHandlerBase
{
    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        if (filters.Genres.Count == 0)
        {
            await SelectAllGenres(unitOfWork, filters);
        }

        query = query.Where(game => game.GameGenres.Any(gp => filters.Genres.Contains(gp.GenreId)));
        query = await base.HandleAsync(unitOfWork, filters, query);

        return query;
    }

    private static async Task SelectAllGenres(IUnitOfWork unitOfWork, GameFiltersDto filters)
    {
        filters.Genres.AddRange((await unitOfWork.GenreRepository.GetAllAsync()).Select(x => x.Id));
    }
}
