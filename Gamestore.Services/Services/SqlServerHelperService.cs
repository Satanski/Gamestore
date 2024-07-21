using AutoMapper;
using Gamestore.BLL.Filtering;
using Gamestore.BLL.Filtering.Models;
using Gamestore.BLL.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Services;

internal static class SqlServerHelperService
{
    internal static async Task FilterGamesFromSQLServerAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, GameFiltersDto gameFilters, FilteredGamesDto filteredGameDtos, IGameProcessingPipelineService gameProcessingPipelineService, bool canSeeDeletedGames)
    {
        IQueryable<Game> games;
        if (canSeeDeletedGames)
        {
            games = unitOfWork.GameRepository.GetGamesWithDeletedAsQueryable();
        }
        else
        {
            games = unitOfWork.GameRepository.GetGamesAsQueryable();
        }

        var gamesFromSQLServer = (await gameProcessingPipelineService.ProcessGamesAsync(unitOfWork, mongoUnitOfWork, gameFilters, games)).ToList();
        if (gamesFromSQLServer.Count != 0)
        {
            filteredGameDtos.Games.AddRange(automapper.Map<List<GameModelDto>>(gamesFromSQLServer));
        }
    }

    internal static async Task UpdateExistingOrderAsync(IUnitOfWork unitOfWork, IMapper automapper, int quantity, GameModelDto game, int unitInStock, Order? exisitngOrder)
    {
        if (game.Id is not null)
        {
            OrderGame existingOrderGame = await unitOfWork.OrderGameRepository.GetByOrderIdAndProductIdAsync(exisitngOrder.Id, (Guid)game.Id);

            if (existingOrderGame != null)
            {
                await UpdateExistingOrderGameAsync(unitOfWork, quantity, unitInStock, existingOrderGame);
            }
            else
            {
                await CreateNewOrderGameAsync(unitOfWork, automapper, quantity, game, unitInStock, exisitngOrder);
            }
        }
    }

    internal static async Task CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync(IUnitOfWork unitOfWork, IMapper automapper, GameModelDto game, Game? gameInSQLServer)
    {
        if (gameInSQLServer is null)
        {
            var gameToAdd = automapper.Map<Game>(game);

            await CreateGenreInSQLServerIfDoesntExistAsync(unitOfWork, game, gameToAdd);
            await CreatePublisherInSQLServerIfDoesntExistAsync(unitOfWork, game, gameToAdd);
            await unitOfWork.GameRepository.AddAsync(gameToAdd);
            await unitOfWork.SaveAsync();
        }
    }

    internal static async Task<List<GenreModelDto>> GetGenresFromSQLServerByGameKeyAsync(IUnitOfWork unitOfWork, IMapper automapper, string gameKey)
    {
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);

        List<GenreModelDto> genreModels = [];
        if (game is not null)
        {
            var genres = await unitOfWork.GameRepository.GetGenresByGameAsync(game.Id);

            foreach (var genre in genres)
            {
                genreModels.Add(automapper.Map<GenreModelDto>(genre));
            }

            return genreModels;
        }

        return null;
    }

    internal static async Task<PublisherModelDto> GetPublisherFromSQLServerByGameKeyAsync(IUnitOfWork unitOfWork, IMapper automapper, string gameKey)
    {
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
        if (game is not null)
        {
            var publisher = await unitOfWork.GameRepository.GetPublisherByGameAsync(game.Id);

            return automapper.Map<PublisherModelDto>(publisher);
        }

        return null;
    }

    internal static async Task<GameModelDto> GetGameFromSQLServerByKeyAsync(IUnitOfWork unitOfWork, IMapper automapper, string key)
    {
        var game = await unitOfWork.GameRepository.GetGameByKeyAsync(key);
        await IncreaseGameViewCounterAsync(unitOfWork, game);
        return automapper.Map<GameModelDto>(game);
    }

    internal static async Task<Game?> GetGameFromSQLServerByIdAsync(IUnitOfWork unitOfWork, Guid gameId)
    {
        return await unitOfWork.GameRepository.GetByIdAsync(gameId);
    }

    private static async Task IncreaseGameViewCounterAsync(IUnitOfWork unitOfWork, Game? game)
    {
        if (game is not null)
        {
            game.NumberOfViews++;
            await unitOfWork.SaveAsync();
        }
    }

    private static async Task CreateGenreInSQLServerIfDoesntExistAsync(IUnitOfWork unitOfWork, GameModelDto game, Game gameToAdd)
    {
        var firstGenre = game.Genres[0];
        if (firstGenre != null && firstGenre.Id != null && game.Id != null)
        {
            var existingGenre = await unitOfWork.GenreRepository.GetByIdAsync((Guid)firstGenre.Id);
            if (existingGenre is null)
            {
                await unitOfWork.GenreRepository.AddAsync(new() { Id = (Guid)firstGenre.Id, Name = firstGenre.Name });
            }

            gameToAdd.ProductCategories =
            [
                 new GameGenres { GameId = game.Id.Value, GenreId = (Guid)firstGenre.Id }
            ];
        }
    }

    private static async Task CreatePublisherInSQLServerIfDoesntExistAsync(IUnitOfWork unitOfWork, GameModelDto game, Game? gameInSQLServer)
    {
        if (game.Publisher is not null && game.Publisher.Id is not null)
        {
            var publisherInSQLServer = await unitOfWork.PublisherRepository.GetByIdAsync((Guid)game.Publisher.Id);

            if (publisherInSQLServer is null)
            {
                await unitOfWork.PublisherRepository.AddAsync(new Publisher { Id = (Guid)game.Publisher.Id, CompanyName = game.Publisher.CompanyName });
                await unitOfWork.SaveAsync();
                await AttachPublisherFromSQLServerAsync(unitOfWork, game, gameInSQLServer);
            }
            else
            {
                var publisherId = game.Publisher.Id;
                if (publisherId is not null)
                {
                    var pub = await unitOfWork.PublisherRepository.GetByIdAsync((Guid)publisherId);
                    if (pub is not null)
                    {
                        await AttachPublisherFromSQLServerAsync(unitOfWork, game, gameInSQLServer);
                    }
                }
            }
        }
    }

    private static async Task AttachPublisherFromSQLServerAsync(IUnitOfWork unitOfWork, GameModelDto game, Game? gameInSQLServer)
    {
        var publisherId = game.Publisher.Id;
        if (publisherId is not null)
        {
            var pub = await unitOfWork.PublisherRepository.GetByIdAsync((Guid)publisherId);
            if (pub is not null)
            {
                gameInSQLServer.Publisher = pub;
            }
        }
    }

    private static async Task UpdateExistingOrderGameAsync(IUnitOfWork unitOfWork, int quantity, int unitInStock, OrderGame existingOrderGame)
    {
        var expectedTotalQuantity = quantity + existingOrderGame.Quantity;
        expectedTotalQuantity = expectedTotalQuantity < unitInStock ? expectedTotalQuantity : unitInStock;
        existingOrderGame.Quantity = expectedTotalQuantity;
        await unitOfWork.OrderGameRepository.UpdateAsync(existingOrderGame);
        await unitOfWork.SaveAsync();
    }

    private static async Task CreateNewOrderGameAsync(IUnitOfWork unitOfWork, IMapper automapper, int quantity, GameModelDto game, int unitInStock, Order? exisitngOrder)
    {
        if (game.Id is not null)
        {
            var expectedTotalQuantity = quantity < unitInStock ? quantity : unitInStock;

            var gameInSQLServer = await unitOfWork.GameRepository.GetByIdAsync((Guid)game.Id);
            if (gameInSQLServer is null)
            {
                await CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync(unitOfWork, automapper, game, gameInSQLServer);
            }

            OrderGame newOrderGame = new OrderGame()
            {
                OrderId = exisitngOrder.Id,
                GameId = game.Id ?? Guid.Empty,
                Price = game.Price,
                Discount = game.Discontinued,
                Quantity = expectedTotalQuantity,
            };

            await unitOfWork.OrderGameRepository.AddAsync(newOrderGame);
            await unitOfWork.SaveAsync();
        }
    }
}
