using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PaginationFilterHandler : GameProcessingPipelineHandlerBase
{
    private readonly string _allGames = PaginationOptionsDto.PaginationOptions[4];

    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters)
    {
        var pageCount = filters.PageCount;

        switch (pageCount)
        {
            case var filter when filter == _allGames:
            case null:
                filters.NumberOfPagesAfterFiltration = 1;
                filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);
                return filteredGames;
            default:
                filters.NumberOfPagesAfterFiltration = CountNumberOfPagesAfterFiltration(int.Parse(pageCount), filteredGames);
                CheckIfPageNumberDoesntExceedLastPage(filters);
                filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);
                return FilterGames(int.Parse(pageCount), filteredGames, filters);
        }
    }

    private static void CheckIfPageNumberDoesntExceedLastPage(GameFiltersDto filters)
    {
        if (filters.Page > filters.NumberOfPagesAfterFiltration)
        {
            filters.Page = (int)filters.NumberOfPagesAfterFiltration;
        }
    }

    private static int CountNumberOfPagesAfterFiltration(int numberOfGamesPerPage, List<Game> filteredGames)
    {
        return (int)Math.Ceiling((double)filteredGames.Count / numberOfGamesPerPage);
    }

    private static List<Game> FilterGames(int numberOfGamesPerPage, List<Game> filteredGames, GameFiltersDto filters)
    {
        return filteredGames.Skip(numberOfGamesPerPage * (filters.Page - 1)).Take(numberOfGamesPerPage).ToList();
    }
}
