using AutoMapper;
using Gamestore.BLL.Filtering;
using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Services;

internal static class MongoDbHelperService
{
    internal static async Task FilterProductsFromMongoDBAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, GameFiltersDto gameFilters, FilteredGamesDto filteredGameDtos, IGameProcessingPipelineService gameProcessingPipelineService)
    {
        var products = await GetProductsFromMongoDBThatDoesntExistInSQLServerAsync(unitOfWork, mongoUnitOfWork, automapper);

        var filterdProducts = (await gameProcessingPipelineService.ProcessGamesAsync(unitOfWork, mongoUnitOfWork, gameFilters, products.AsQueryable())).ToList();
        if (filterdProducts.Count != 0)
        {
            filteredGameDtos.Games.AddRange(automapper.Map<List<GameModelDto>>(filterdProducts));
        }
    }

    internal static void SetTotalNumberOfPagesAfterFiltering(GameFiltersDto gameFilters, FilteredGamesDto filteredGameDtos)
    {
        filteredGameDtos.TotalPages = gameFilters.NumberOfPagesAfterFiltration;
    }

    internal static void CheckIfCurrentPageDoesntExceedTotalNumberOfPages(GameFiltersDto gameFilters, FilteredGamesDto filteredGameDtos)
    {
        if (gameFilters.Page <= gameFilters.NumberOfPagesAfterFiltration)
        {
            filteredGameDtos.CurrentPage = gameFilters.Page;
        }
        else
        {
            filteredGameDtos.CurrentPage = gameFilters.NumberOfPagesAfterFiltration;
        }
    }

    private static async Task<List<Game>> GetProductsFromMongoDBThatDoesntExistInSQLServerAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper)
    {
        var productsFromMongoDB = automapper.Map<List<Game>>(await mongoUnitOfWork.ProductRepository.GetAllAsync());
        return productsFromMongoDB.Except(await unitOfWork.GameRepository.GetAllAsync()).ToList();
    }
}
