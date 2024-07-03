using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PublishDateHandler : GameProcessingPipelineHandlerBase
{
    private readonly string _lastWeek = PublishDateOptionsDto.PublishDateOptions[0];
    private readonly string _lastMonth = PublishDateOptionsDto.PublishDateOptions[1];
    private readonly string _lastYear = PublishDateOptionsDto.PublishDateOptions[2];
    private readonly string _twoYears = PublishDateOptionsDto.PublishDateOptions[3];
    private readonly string _threeYears = PublishDateOptionsDto.PublishDateOptions[4];

    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters)
    {
        var publishingDate = filters.DatePublishing;
        var now = DateOnly.FromDateTime(DateTime.Now);

        switch (publishingDate)
        {
            case var filter when filter == _lastWeek:
                filteredGames = filteredGames.Where(x => x.PublishDate >= now.AddDays(-7)).ToList();
                break;

            case var filter when filter == _lastMonth:
                filteredGames = filteredGames.Where(x => x.PublishDate >= now.AddMonths(-1)).ToList();
                break;

            case var filter when filter == _lastYear:
                filteredGames = filteredGames.Where(x => x.PublishDate >= now.AddYears(-1)).ToList();
                break;

            case var filter when filter == _twoYears:
                filteredGames = filteredGames.Where(x => x.PublishDate >= now.AddYears(-2)).ToList();
                break;

            case var filter when filter == _threeYears:
                filteredGames = filteredGames.Where(x => x.PublishDate >= now.AddYears(-3)).ToList();
                break;

            case null:
                break;

            default:
                throw new GamestoreException("Wrong publishing date filter");
        }

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }
}
