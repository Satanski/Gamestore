using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Microsoft.Extensions.Logging;

namespace Gamestore.Services.Services;

public class GenreService(IUnitOfWork unitOfWork, IMapper automapper, ILogger<GenreService> logger) : IGenreService
{
    private readonly GenreModelValidator _genreModelValidator = new(unitOfWork);
    private readonly GenreModelDtoValidator _genreModelDtoValidator = new(unitOfWork);

    public async Task AddGenreAsync(GenreModel genreModel)
    {
        logger.LogInformation("Adding genre {@genreModel}", genreModel);
        var result = await _genreModelValidator.ValidateAsync(genreModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var genre = automapper.Map<Genre>(genreModel);

        await unitOfWork.GenreRepository.AddAsync(genre);

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteGenreAsync(Guid genreId)
    {
        logger.LogInformation("Deleting genre {genreId}", genreId);
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

    public async Task<IEnumerable<GenreModel>> GetAllGenresAsync()
    {
        logger.LogInformation("Getting all genres");
        var genres = await unitOfWork.GenreRepository.GetAllAsync();
        List<GenreModel> genreModels = [];

        foreach (var genre in genres)
        {
            genreModels.Add(automapper.Map<GenreModel>(genre));
        }

        return genreModels.AsEnumerable();
    }

    public async Task<IEnumerable<GameModel>> GetGamesByGenreAsync(Guid genreId)
    {
        logger.LogInformation("Getting games by genre: {genreId}", genreId);
        var games = await unitOfWork.GenreRepository.GetGamesByGenreAsync(genreId);

        List<GameModel> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(automapper.Map<GameModel>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task<GenreModel> GetGenreByIdAsync(Guid genreId)
    {
        logger.LogInformation("Getting genre by id: {genreId}", genreId);
        var genre = await unitOfWork.GenreRepository.GetByIdAsync(genreId);

        return genre == null ? throw new GamestoreException($"No genre found with given id: {genreId}") : automapper.Map<GenreModel>(genre);
    }

    public async Task<IEnumerable<GenreModel>> GetGenresByParentGenreAsync(Guid genreId)
    {
        logger.LogInformation("Getting genres by parent genre id: {genreId}", genreId);
        var genres = await unitOfWork.GenreRepository.GetGenresByParentGenreAsync(genreId);
        List<GenreModel> genreModels = [];

        foreach (var genre in genres)
        {
            genreModels.Add(automapper.Map<GenreModel>(genre));
        }

        return genreModels.AsEnumerable();
    }

    public async Task UpdateGenreAsync(GenreModelDto genreModel)
    {
        logger.LogInformation("Updating genre {@genreModel}", genreModel);
        var result = await _genreModelDtoValidator.ValidateAsync(genreModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var genre = automapper.Map<Genre>(genreModel);

        await unitOfWork.GenreRepository.UpdateAsync(genre);

        await unitOfWork.SaveAsync();
    }
}
