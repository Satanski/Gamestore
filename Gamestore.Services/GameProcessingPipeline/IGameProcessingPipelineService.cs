using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.Filtering;

public interface IGameProcessingPipelineService
{
    Task<IQueryable<Game>> ProcessGamesAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Game> query);
}
