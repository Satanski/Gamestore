using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PaginationFilterHandler : GameProcessingPipelineHandlerBase
{
    private readonly string _allGames = PaginationOptionsDto.PaginationOptions[4];

    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        var pageCount = filters.PageCount;

        switch (pageCount)
        {
            case var filter when filter == _allGames:
            case null:
                filters.NumberOfPagesAfterFiltration = 1;
                query = await base.HandleAsync(unitOfWork, filters, query);
                return query;
            default:
                filters.NumberOfPagesAfterFiltration = CountNumberOfPagesAfterFiltration(int.Parse(pageCount), query);
                CheckIfPageNumberDoesntExceedLastPage(filters);

                var numberOfGamesPerPage = int.Parse(pageCount);
                query = query.Skip(numberOfGamesPerPage * (filters.Page - 1)).Take(numberOfGamesPerPage);

                return query;
        }
    }

    private static void CheckIfPageNumberDoesntExceedLastPage(GameFiltersDto filters)
    {
        if (filters.Page > filters.NumberOfPagesAfterFiltration)
        {
            filters.Page = (int)filters.NumberOfPagesAfterFiltration;
        }
    }

    private static int CountNumberOfPagesAfterFiltration(int numberOfGamesPerPage, IQueryable<Game> filteredGames)
    {
        return (int)Math.Ceiling((double)filteredGames.Count() / numberOfGamesPerPage);
    }
}
