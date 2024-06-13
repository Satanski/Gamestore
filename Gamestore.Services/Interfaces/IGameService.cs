﻿using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.Services.Interfaces;

public interface IGameService
{
    Task AddGameAsync(GameDtoWrapper gameModel);

    Task DeleteGameByIdAsync(Guid gameId);

    Task DeleteGameByKeyAsync(string gameKey);

    Task<IEnumerable<GameModelDto>> GetAllGamesAsync();

    Task<GameModelDto> GetGameByIdAsync(Guid gameId);

    Task<GameModelDto> GetGameByKeyAsync(string key);

    Task<IEnumerable<GenreModelDto>> GetGenresByGameKeyAsync(string gameKey);

    Task<IEnumerable<PlatformModelDto>> GetPlatformsByGameKeyAsync(string gameKey);

    Task<PublisherDto> GetPublisherByGameKeyAsync(string gameKey);

    Task UpdateGameAsync(GameDtoWrapper gameModel);
}
