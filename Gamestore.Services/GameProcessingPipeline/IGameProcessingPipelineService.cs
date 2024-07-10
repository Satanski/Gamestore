using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public interface IGameProcessingPipelineService
{
    Task<IQueryable<Game>> ProcessGamesAsync(IUnitOfWork unitOfWork, GameFiltersDto filters, IQueryable<Game> query);
}
