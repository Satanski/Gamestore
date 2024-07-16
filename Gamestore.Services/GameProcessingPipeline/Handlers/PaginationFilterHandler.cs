using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.BLL.Filtering.Handlers;

public class PaginationFilterHandler : GameProcessingPipelineHandlerBase
{
    private readonly string _allGames = PaginationOptionsDto.PaginationOptions[4];

    public override async Task<IQueryable<Product>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Product> query)
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
                filters.NumberOfPagesAfterFiltration = await CountNumberOfPagesAfterFiltration(int.Parse(pageCount), query);
                CheckIfPageNumberDoesntExceedLastPage(filters);

                var numberOfGamesPerPage = int.Parse(pageCount);
                int numberToSkip = CalculateNumberOfEntriesToSkip(filters, numberOfGamesPerPage);
                filters.NumberOfPagesAfterFiltration = CountNumberOfPagesAfterFiltration(int.Parse(pageCount), query, filters);
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

    private static int CountNumberOfPagesAfterFiltration(int numberOfGamesPerPage, IQueryable<Product> query, GameFiltersDto filters)
    {
        var noOfGames = query.Count() + filters.NumberOfGamesFromPreviousSource;
        return (int)Math.Ceiling((double)noOfGames / numberOfGamesPerPage);
    }
}
