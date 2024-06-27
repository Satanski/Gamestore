using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering.Handlers;

public interface IPriceFilterHandler
{
    Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters);
}
