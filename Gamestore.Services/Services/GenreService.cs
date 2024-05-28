using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;

namespace Gamestore.Services.Services;

public class GenreService(IUnitOfWork unitOfWork, IMapper automapper) : IGenreService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _automapper = automapper;
    private readonly GenreModelValidator _genreModelValidator = new(unitOfWork);
    private readonly GenreModelDtoValidator _genreModelDtoValidator = new(unitOfWork);

    public async Task AddGenreAsync(GenreModel genreModel)
    {
        var result = await _genreModelValidator.ValidateAsync(genreModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var genre = _automapper.Map<Genre>(genreModel);

        await _unitOfWork.GenreRepository.AddAsync(genre);

        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteGenreAsync(Guid genreId)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(genreId);
        if (genre != null)
        {
            _unitOfWork.GenreRepository.Delete(genre);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new GamestoreException($"No genre found with given id: {genreId}");
        }
    }

    public async Task<IEnumerable<GenreModel>> GetAllGenresAsync()
    {
        var genres = await _unitOfWork.GenreRepository.GetAllAsync();
        List<GenreModel> genreModels = [];

        foreach (var genre in genres)
        {
            genreModels.Add(_automapper.Map<GenreModel>(genre));
        }

        return genreModels.AsEnumerable();
    }

    public async Task<IEnumerable<GameModel>> GetGamesByGenreAsync(Guid genreId)
    {
        var games = await _unitOfWork.GenreRepository.GetGamesByGenreAsync(genreId);

        List<GameModel> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(_automapper.Map<GameModel>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task<GenreModel> GetGenreByIdAsync(Guid genreId)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(genreId);

        return genre == null ? throw new GamestoreException($"No genre found with given id: {genreId}") : _automapper.Map<GenreModel>(genre);
    }

    public async Task<IEnumerable<GenreModel>> GetGenresByParentGenreAsync(Guid genreId)
    {
        var genres = await _unitOfWork.GenreRepository.GetGenresByParentGenreAsync(genreId);
        List<GenreModel> genreModels = [];

        foreach (var genre in genres)
        {
            genreModels.Add(_automapper.Map<GenreModel>(genre));
        }

        return genreModels.AsEnumerable();
    }

    public async Task UpdateGenreAsync(GenreModelDto genreModel)
    {
        var result = await _genreModelDtoValidator.ValidateAsync(genreModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var genre = _automapper.Map<Genre>(genreModel);

        await _unitOfWork.GenreRepository.UpdateAsync(genre);

        await _unitOfWork.SaveAsync();
    }
}
