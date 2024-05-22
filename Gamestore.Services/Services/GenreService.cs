using Gamestore.Repository.Interfaces;
using Gamestore.Services.Helpers;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Validation;

namespace Gamestore.Services.Services;

public class GenreService(IUnitOfWork unitOfWork) : IGenreService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task AddGenreAsync(GenreModel genreModel)
    {
        ValidationHelpers.ValidateGenreModel(genreModel);
        var genre = MappingHelpers.CreateGenre(genreModel);

        var task = Task.Run(() => _unitOfWork.GenreRepository.AddGenreAsync(genre));

        return task;
    }

    public Task DeleteGenreAsync(Guid id)
    {
        var task = Task.Run(() => _unitOfWork.GenreRepository.DeleteGenreAsync(id));

        return task;
    }

    public Task<IEnumerable<GenreModel>> GetAllGenresAsync()
    {
        var task = Task.Run(() => _unitOfWork.GenreRepository.GetAllGenresAsync())
            .ContinueWith(x =>
            {
                var genres = x.Result.ToList();
                List<GenreModel> genreModels = [];

                if (genres.Count == 0)
                {
                    throw new GamestoreException("No genres found");
                }

                foreach (var genre in genres)
                {
                    genreModels.Add(MappingHelpers.CreateGenreModel(genre));
                }

                return genreModels.AsEnumerable();
            });

        return task;
    }

    public Task<IEnumerable<GameModel>> GetGamesByGenreAsync(Guid id)
    {
        var task = Task.Run(() => _unitOfWork.GenreRepository.GetGamesByGenreAsync(id))
            .ContinueWith(x =>
       {
           var games = x.Result.ToList();

           List<GameModel> gameModels = [];

           if (games.Count == 0)
           {
               throw new GamestoreException("No games found");
           }

           foreach (var game in games)
           {
               gameModels.Add(MappingHelpers.CreateGameModel(game));
           }

           return gameModels.AsEnumerable();
       });

        return task;
    }

    public Task<GenreModel> GetGenreByIdAsync(Guid id)
    {
        var task = Task.Run(() => _unitOfWork.GenreRepository.GetGenreByIdAsync(id))
            .ContinueWith(x =>
            {
                var genre = x.Result;

                return genre == null ? throw new GamestoreException($"No genre found with given id: {id}") : MappingHelpers.CreateGenreModel(genre);
            });

        return task;
    }

    public Task<IEnumerable<GenreModel>> GetGenresByParentGenreAsync(Guid id)
    {
        var task = Task.Run(() => _unitOfWork.GenreRepository.GetGenresByParentGenreAsync(id))
            .ContinueWith(x =>
            {
                var genres = x.Result.ToList();
                List<GenreModel> genreModels = [];

                if (genres.Count == 0)
                {
                    throw new GamestoreException("No genres found");
                }

                foreach (var genre in genres)
                {
                    genreModels.Add(MappingHelpers.CreateGenreModel(genre));
                }

                return genreModels.AsEnumerable();
            });

        return task;
    }

    public Task UpdateGenreAsync(DetailedGenreModel genreModel)
    {
        ValidationHelpers.ValidateDetailedGenreModel(genreModel);
        var genre = MappingHelpers.CreateDetailedGenre(genreModel);

        var task = Task.Run(() => _unitOfWork.GenreRepository.UpdateGenreAsync(genre));

        return task;
    }
}
