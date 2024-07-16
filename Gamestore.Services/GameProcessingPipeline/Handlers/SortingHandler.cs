using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering;

public class SortingHandler : GameProcessingPipelineHandlerBase
{
    private readonly string _mostPoplar = SortingOptionsDto.SortingOptions[0];
    private readonly string _mostCommented = SortingOptionsDto.SortingOptions[1];
    private readonly string _priceAsc = SortingOptionsDto.SortingOptions[2];
    private readonly string _priceDesc = SortingOptionsDto.SortingOptions[3];
    private readonly string _new = SortingOptionsDto.SortingOptions[4];

    public override async Task<IQueryable<Product>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Product> query)
    {
        string sortOption = filters.Sort;

        switch (sortOption)
        {
            case var filter when filter == _mostPoplar:
                query = query.OrderByDescending(x => x.NumberOfViews);
                break;
            case var filter when filter == _mostCommented:
                query = query.OrderByDescending(x => x.Comments.Count);
                break;
            case var filter when filter == _priceAsc:
                query = query.OrderBy(x => x.Price);
                break;
            case var filter when filter == _priceDesc:
                query = query.OrderByDescending(x => x.Price);
                break;
            case var filter when filter == _new:
                query = query.OrderByDescending(x => x.PublishDate);
                break;
            case null:
                break;
            default:
                throw new GamestoreException("Wrong sorting option");
        }

        query = await base.HandleAsync(unitOfWork, mongoUnitOfWork, filters, query);

        return query;
    }
}
