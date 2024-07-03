using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class SortingHandler : GameProcessingPipelineHandlerBase
{
    private readonly string _mostPoplar = SortingOptionsDto.SortingOptions[0];
    private readonly string _mostCommented = SortingOptionsDto.SortingOptions[1];
    private readonly string _priceAsc = SortingOptionsDto.SortingOptions[2];
    private readonly string _priceDesc = SortingOptionsDto.SortingOptions[3];
    private readonly string _new = SortingOptionsDto.SortingOptions[4];

    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters)
    {
        string sortOption = filters.Sort;

        switch (sortOption)
        {
            case var filter when filter == _mostPoplar:
                filteredGames = [.. filteredGames.OrderByDescending(x => x.NumberOfViews)];
                break;
            case var filter when filter == _mostCommented:
                filteredGames = [.. filteredGames.OrderByDescending(x => x.Comments.Count)];
                break;
            case var filter when filter == _priceAsc:
                filteredGames = [.. filteredGames.OrderBy(x => x.Price)];
                break;
            case var filter when filter == _priceDesc:
                filteredGames = [.. filteredGames.OrderByDescending(x => x.Price)];
                break;
            case var filter when filter == _new:
                filteredGames = [.. filteredGames.OrderByDescending(x => x.PublishDate)];
                break;
            case null:
                break;
            default:
                throw new GamestoreException("Wrong sorting option");
        }

        filteredGames = await base.HandleAsync(unitOfWork, filteredGames, filters);

        return filteredGames;
    }
}
