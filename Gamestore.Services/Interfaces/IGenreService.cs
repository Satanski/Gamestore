﻿using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGenreService
{
    Task AddGenreAsync(GenreDtoWrapper genreModel);

    Task<IEnumerable<GameModelDto>> GetGamesByGenreAsync(Guid genreId);

    Task<GenreModelDto> GetGenreByIdAsync(Guid genreId);

    Task<IEnumerable<GenreModelDto>> GetAllGenresAsync();

    Task UpdateGenreAsync(GenreDtoWrapper genreModel);

    Task DeleteGenreAsync(Guid genreId);

    Task<IEnumerable<GenreModelDto>> GetGenresByParentGenreAsync(Guid genreId);
}
