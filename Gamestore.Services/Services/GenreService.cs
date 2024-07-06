using System.Collections.Generic;
using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Models;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.Extensions.Logging;

namespace Gamestore.Services.Services;

public class GenreService(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, ILogger<GenreService> logger) : IGenreService
{
    private readonly GenreDtoWrapperAddValidator _genreDtoWrapperAddValidator = new(unitOfWork);
    private readonly GenreDtoWrapperUpdateValidator _genreDtoWrapperUpdateValidator = new(unitOfWork);

    public async Task DeleteGenreAsync(Guid genreId)
    {
        logger.LogInformation("Deleting genre {genreId}", genreId);

        var childGenres = await unitOfWork.GenreRepository.GetGenresByParentGenreAsync(genreId);
        if (childGenres.Count != 0)
        {
            throw new GamestoreException($"You can't delete genre when it has child genres {genreId}");
        }

        var genre = await unitOfWork.GenreRepository.GetByIdAsync(genreId);
        if (genre != null)
        {
            unitOfWork.GenreRepository.Delete(genre);
            await unitOfWork.SaveAsync();
        }
        else
        {
            throw new GamestoreException($"No genre found with given id: {genreId}");
        }
    }

    public async Task<IEnumerable<GenreModelDto>> GetAllGenresAsync()
    {
        logger.LogInformation("Getting all genres");

        var genreModels = await GetGenresFromSQLServer(unitOfWork, automapper);
        genreModels.AddRange(await GetCategoriesFromMongoDB(mongoUnitOfWork, automapper));

        return genreModels.AsEnumerable();
    }

    public async Task<IEnumerable<GameModelDto>> GetGamesByGenreAsync(Guid genreId)
    {
        logger.LogInformation("Getting games by genre: {genreId}", genreId);

        var games = await GetGamesByGenreIdFromSQLServer(unitOfWork, automapper, genreId);
        if (games.Count == 0)
        {
            games = await GetGamesByGenreIdFromMongoDB(mongoUnitOfWork, automapper, genreId);
        }

        return games;
    }

    public async Task<GenreModelDto> GetGenreByIdAsync(Guid genreId)
    {
        logger.LogInformation("Getting genre by id: {genreId}", genreId);

        var genre = await GetGenreFromSQLServerById(unitOfWork, genreId);
        genre ??= await GetGenreFromMongoDB(mongoUnitOfWork, automapper, genreId);

        return genre == null ? throw new GamestoreException($"No genre found with given id: {genreId}") : automapper.Map<GenreModelDto>(genre);
    }

    public async Task<IEnumerable<GenreModelDto>> GetGenresByParentGenreAsync(Guid genreId)
    {
        logger.LogInformation("Getting genres by parent genre id: {genreId}", genreId);
        var genres = await unitOfWork.GenreRepository.GetGenresByParentGenreAsync(genreId);
        List<GenreModelDto> genreModels = automapper.Map<List<GenreModelDto>>(genres);

        return genreModels.AsEnumerable();
    }

    public async Task AddGenreAsync(GenreDtoWrapper genreModel)
    {
        logger.LogInformation("Adding genre {@genreModel}", genreModel);

        await _genreDtoWrapperAddValidator.ValidateGenreForAdding(genreModel);

        var genre = automapper.Map<Genre>(genreModel.Genre);

        await unitOfWork.GenreRepository.AddAsync(genre);

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateGenreAsync(GenreDtoWrapper genreModel)
    {
        logger.LogInformation("Updating genre {@genreModel}", genreModel);

        await _genreDtoWrapperUpdateValidator.ValidateGenreForUpdating(genreModel);

        var genre = automapper.Map<Genre>(genreModel.Genre);

        await unitOfWork.GenreRepository.UpdateAsync(genre);

        await unitOfWork.SaveAsync();
    }

    private static int ConvertFirstFourCharactersOfGuidToId(Guid genreId)
    {
        return int.Parse(genreId.ToString()[..4]);
    }

    private static async Task<Genre?> GetGenreFromSQLServerById(IUnitOfWork unitOfWork, Guid genreId)
    {
        return await unitOfWork.GenreRepository.GetByIdAsync(genreId);
    }

    private static async Task<Genre?> GetGenreFromMongoDB(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, Guid genreId)
    {
        int id = ConvertFirstFourCharactersOfGuidToId(genreId);
        var category = await mongoUnitOfWork.CategoryRepository.GetCategoryById(id);
        var genre = automapper.Map<Genre>(category);

        return genre;
    }

    private static async Task<List<GameModelDto>> GetGamesByGenreIdFromSQLServer(IUnitOfWork unitOfWork, IMapper automapper, Guid genreId)
    {
        List<GameModelDto> gameModels = [];
        var games = await unitOfWork.GenreRepository.GetGamesByGenreAsync(genreId);
        if (games is not null)
        {
            gameModels = automapper.Map<List<GameModelDto>>(games);
        }

        return gameModels;
    }

    private static async Task<List<GameModelDto>> GetGamesByGenreIdFromMongoDB(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, Guid genreId)
    {
        List<GameModelDto> games = [];
        int id = ConvertFirstFourCharactersOfGuidToId(genreId);
        var category = await mongoUnitOfWork.CategoryRepository.GetCategoryById(id);
        var products = await mongoUnitOfWork.ProductRepository.GetProductsByCategoryIdAsync(category.CategoryId);

        if (products is not null)
        {
            games = automapper.Map<List<GameModelDto>>(games);
        }

        return games;
    }

    private static async Task<List<GenreModelDto>> GetGenresFromSQLServer(IUnitOfWork unitOfWork, IMapper automapper)
    {
        var genres = await unitOfWork.GenreRepository.GetAllAsync();
        var genreModels = automapper.Map<List<GenreModelDto>>(genres);

        return genreModels;
    }

    private static async Task<List<GenreModelDto>> GetCategoriesFromMongoDB(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper)
    {
        var categories = await mongoUnitOfWork.CategoryRepository.GetAllAsync();
        var genres = automapper.Map<List<GenreModelDto>>(categories);

        return genres;
    }
}
