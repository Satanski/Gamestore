using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public interface IGameProcessingPipelineService
{
    Task<List<Game>> ProcessGamesAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters);
}
