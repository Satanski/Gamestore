using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public class PublishDateHandler : GameProcessingPipelineHandlerBase
{
    private readonly string _lastWeek = PublishDateOptionsDto.PublishDateOptions[0];
    private readonly string _lastMonth = PublishDateOptionsDto.PublishDateOptions[1];
    private readonly string _lastYear = PublishDateOptionsDto.PublishDateOptions[2];
    private readonly string _twoYears = PublishDateOptionsDto.PublishDateOptions[3];
    private readonly string _threeYears = PublishDateOptionsDto.PublishDateOptions[4];

    public override async Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Game> query)
    {
        var publishingDate = filters.DatePublishing;
        var now = DateOnly.FromDateTime(DateTime.Now);

        switch (publishingDate)
        {
            case var filter when filter == _lastWeek:
                query = query.Where(x => x.PublishDate >= now.AddDays(-7));
                break;

            case var filter when filter == _lastMonth:
                query = query.Where(x => x.PublishDate >= now.AddMonths(-1));
                break;

            case var filter when filter == _lastYear:
                query = query.Where(x => x.PublishDate >= now.AddYears(-1));
                break;

            case var filter when filter == _twoYears:
                query = query.Where(x => x.PublishDate >= now.AddYears(-2));
                break;

            case var filter when filter == _threeYears:
                query = query.Where(x => x.PublishDate >= now.AddYears(-3));
                break;

            case null:
                break;

            default:
                throw new GamestoreException("Wrong publishing date filter");
        }

        query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);

        return query;
    }
}
