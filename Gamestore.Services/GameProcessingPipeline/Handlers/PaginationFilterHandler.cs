using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PaginationFilterHandler : GameProcessingPipelineHandlerBase
{
    private readonly string _allGames = PaginationOptionsDto.PaginationOptions[4];

    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        var pageCount = filters.PageCount;

        switch (pageCount)
        {
            case var filter when filter == _allGames:
            case null:
                filters.NumberOfPagesAfterFiltration = 1;
                filters.NumberOfGamesFromPreviousSource = query.Count();
                query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);
                return query;
            default:
                var numberOfGamesPerPage = int.Parse(pageCount);
                int numberToSkip = CalculateNumberOfEntriesToSkip(filters, numberOfGamesPerPage);
                filters.NumberOfPagesAfterFiltration = CountNumberOfPagesAfterFiltration(int.Parse(pageCount), query, filters.NumberOfGamesFromPreviousSource);
                filters.NumberOfGamesFromPreviousSource = query.Count();
                int numberToTake = numberOfGamesPerPage - filters.NumberOfDisplayedGamesFromPreviousSource;

                query = query.Skip(numberToSkip).Take(numberToTake);
                filters.NumberOfDisplayedGamesFromPreviousSource = query.Count();

                return query;
        }
    }

    private static int CalculateNumberOfEntriesToSkip(GameFiltersDto filters, int numberOfGamesPerPage)
    {
        int numberToSkip;
        if (filters.NumberOfGamesFromPreviousSource == 0)
        {
            numberToSkip = numberOfGamesPerPage * (filters.Page - 1);
        }
        else
        {
            numberToSkip = (numberOfGamesPerPage * (filters.Page - 1 - (filters.NumberOfGamesFromPreviousSource / numberOfGamesPerPage))) - (filters.NumberOfGamesFromPreviousSource % numberOfGamesPerPage);
        }

        return numberToSkip;
    }

    private static int CountNumberOfPagesAfterFiltration(int numberOfGamesPerPage, IQueryable<Game> query, int numberOfGamesFromPreviousSource)
    {
        return (int)Math.Ceiling((double)(query.Count() + numberOfGamesFromPreviousSource) / numberOfGamesPerPage);
    }
}
