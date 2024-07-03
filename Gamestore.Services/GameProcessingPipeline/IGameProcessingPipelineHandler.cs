using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.BanHandler;

public interface IGameProcessingPipelineHandler
{
    void SetNext(IGameProcessingPipelineHandler nextHandler);

    Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFiltersDto filters);
}
