using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PublishDateHandler : FilterHandlerBase, IPublishDateHandler
{
    private const string LastWeek = "Last week";
    private const string LastMonth = "Last month";
    private const string LastYear = "Last year";
    private const string TwoYears = "2 years";
    private const string ThreeYears = "3 years";

    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters)
    {
        var publishingDate = filters.DatePublishing;
        var now = DateOnly.FromDateTime(DateTime.Now);

        filteredGames = publishingDate switch
        {
            LastWeek => filteredGames.Where(x => x.PublishDate >= now.AddDays(-7)).ToList(),
            LastMonth => filteredGames.Where(x => x.PublishDate >= now.AddMonths(-1)).ToList(),
            LastYear => filteredGames.Where(x => x.PublishDate >= now.AddYears(-1)).ToList(),
            TwoYears => filteredGames.Where(x => x.PublishDate >= now.AddYears(-2)).ToList(),
            ThreeYears => filteredGames.Where(x => x.PublishDate >= now.AddYears(-3)).ToList(),
            null => [.. filteredGames],
            _ => throw new GamestoreException("Wrong publising date filter"),
        };
        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }
}
