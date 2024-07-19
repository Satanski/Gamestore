using AutoMapper;
using Gamestore.BLL.Filtering;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Services;

internal static class SqlServerHelperService
{
    internal static async Task FilterGamesFromSQLServerAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, GameFiltersDto gameFilters, FilteredGamesDto filteredGameDtos, IGameProcessingPipelineService gameProcessingPipelineService)
    {
        var games = unitOfWork.GameRepository.GetGamesAsQueryable();
        var gamesFromSQLServer = (await gameProcessingPipelineService.ProcessGamesAsync(unitOfWork, mongoUnitOfWork, gameFilters, games)).ToList();
        if (gamesFromSQLServer.Count != 0)
        {
            filteredGameDtos.Games.AddRange(automapper.Map<List<GameModelDto>>(gamesFromSQLServer));
        }
    }
}
