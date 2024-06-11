using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGenreService
{
    Task AddGenreAsync(GenreAddDto genreModel);

    Task<IEnumerable<GameModelDto>> GetGamesByGenreAsync(Guid genreId);

    Task<GenreModel> GetGenreByIdAsync(Guid genreId);

    Task<IEnumerable<GenreModel>> GetAllGenresAsync();

    Task UpdateGenreAsync(GenreUpdateDto genreModel);

    Task DeleteGenreAsync(Guid genreId);

    Task<IEnumerable<GenreModelDto>> GetGenresByParentGenreAsync(Guid genreId);
}
