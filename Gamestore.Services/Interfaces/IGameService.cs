﻿using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGameService
{
    Task AddGameAsync(GameAddDto gameModel);

    Task DeleteGameByIdAsync(Guid gameId);

    Task<IEnumerable<GameModelDto>> GetAllGamesAsync();

    Task<GameModelDto> GetGameByIdAsync(Guid gameId);

    Task<GameModel> GetGameByKeyAsync(string key);

    Task<IEnumerable<GenreModelDto>> GetGenresByGameIdAsync(Guid gameId);

    Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameIdAsync(Guid gameId);

    Task<PublisherModelDto> GetPublisherByGameIdAsync(Guid gameId);

    Task UpdateGameAsync(GameUpdateDto gameModel);
}
