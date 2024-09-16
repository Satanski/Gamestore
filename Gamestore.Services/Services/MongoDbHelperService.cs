using AutoMapper;
using Gamestore.BLL.Filtering;
using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Helpers;
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

    internal static async Task<List<GenreModelDto>> GetGenresFromMongoDBByGameKeyAsync(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, string gameKey)
    {
        var product = await mongoUnitOfWork.ProductRepository.GetByNameAsync(gameKey);
        var category = await mongoUnitOfWork.CategoryRepository.GetById(product.CategoryID);

        return [automapper.Map<GenreModelDto>(category)];
    }

    internal static async Task<PublisherModelDto> GetPublisherFromMongoDBByGameKeyAsync(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, string gameKey)
    {
        var product = await mongoUnitOfWork.ProductRepository.GetByNameAsync(gameKey);
        var supplier = await mongoUnitOfWork.SupplierRepository.GetByIdAsync(product.SupplierID);

        return automapper.Map<PublisherModelDto>(supplier);
    }

    internal static async Task<GameModelDto> GetGameWithDetailsFromMongoDBByKeyAsync(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, string key)
    {
        var product = await mongoUnitOfWork.ProductRepository.GetByNameAsync(key);
        var gameFromProduct = automapper.Map<GameModelDto>(product);
        await SetGameDetailsForMongoDBGameAsync(mongoUnitOfWork, gameFromProduct);

        return gameFromProduct;
    }

    internal static async Task<GameModelDto> GetGameWithDetailsFromMongoDBByIdAsync(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, int id)
    {
        var product = await mongoUnitOfWork.ProductRepository.GetByIdAsync(id);
        var gameFromProduct = automapper.Map<GameModelDto>(product);
        await SetGameDetailsForMongoDBGameAsync(mongoUnitOfWork, gameFromProduct);

        return gameFromProduct;
    }

    internal static async Task<Game?> GetGameFromMongoDBByIdAsync(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, Guid gameId)
    {
        int id = GuidHelpers.GuidToInt(gameId);
        var product = await mongoUnitOfWork.ProductRepository.GetByIdAsync(id);
        var game = automapper.Map<Game>(product);
        return game;
    }

    private static async Task SetGameDetailsForMongoDBGameAsync(IMongoUnitOfWork mongoUnitOfWork, GameModelDto gameFromProduct)
    {
        if (gameFromProduct is not null && gameFromProduct.Publisher.Id is not null && gameFromProduct.Publisher.Id != Guid.Empty)
        {
            gameFromProduct.Publisher.CompanyName = (await mongoUnitOfWork.SupplierRepository.GetByIdAsync(GuidHelpers.GuidToInt((Guid)gameFromProduct.Publisher.Id!))).CompanyName;
        }

        if (gameFromProduct is not null && gameFromProduct.Genres[0] is not null && gameFromProduct.Genres[0].Id != Guid.Empty)
        {
            gameFromProduct.Genres[0].Name = (await mongoUnitOfWork.CategoryRepository.GetById(GuidHelpers.GuidToInt((Guid)gameFromProduct.Genres[0].Id!))).CategoryName;
        }
    }

    private static async Task<List<Game>> GetProductsFromMongoDBThatDoesntExistInSQLServerAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper)
    {
        var productsFromMongoDB = automapper.Map<List<Game>>(await mongoUnitOfWork.ProductRepository.GetAllAsync());
        return productsFromMongoDB.Except(await unitOfWork.GameRepository.GetAllAsync()).ToList();
    }
}
