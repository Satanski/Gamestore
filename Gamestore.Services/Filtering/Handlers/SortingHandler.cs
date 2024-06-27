using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public class SortingHandler : FilterHandlerBase, ISortingHandler
{
    private const string MostPoplar = "Most popular";
    private const string MostCommented = "Most commented";
    private const string PriceAsc = "Price ASC";
    private const string PriceDesc = "Price DESC";
    private const string New = "New";

    public override async Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters)
    {
        string sortOption = filters.Sort;

        switch (sortOption)
        {
            case MostPoplar:
                filteredGames = [.. filteredGames.OrderByDescending(x => x.NumberOfViews)];
                break;
            case MostCommented:
                filteredGames = [.. filteredGames.OrderByDescending(x => x.Comments.Count)];
                break;
            case PriceAsc:
                filteredGames = [.. filteredGames.OrderBy(x => x.Price)];
                break;
            case PriceDesc:
                filteredGames = [.. filteredGames.OrderByDescending(x => x.Price)];
                break;
            case New:
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
