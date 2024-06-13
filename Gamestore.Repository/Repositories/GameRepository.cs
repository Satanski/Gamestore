﻿using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Gamestore.DAL.Repositories;

public class GameRepository(GamestoreContext context) : RepositoryBase<Game>(context), IGameRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<Genre>> GetGenresByGameAsync(Guid id)
    {
        return _context.GameGenres.Where(x => x.GameId == id).Include(x => x.Genre).Select(x => x.Genre).ToListAsync();
    }

    public Task<List<Platform>> GetPlatformsByGameAsync(Guid id)
    {
        return _context.GamePlatforms.Where(x => x.GameId == id).Include(x => x.Platform).Select(x => x.Platform).ToListAsync();
    }

    public Task<Publisher?> GetPublisherByGameAsync(Guid gameId)
    {
        return _context.Games.Include(x => x.Publisher).Where(x => x.Id == gameId).Select(x => x.Publisher).FirstOrDefaultAsync();
    }

    public Task<Game?> GetGameByKeyAsync(string key)
    {
        var query = GameIncludes();
        return query.Where(x => x.Key == key).AsSplitQuery().FirstOrDefaultAsync();
    }

    public Task<Game?> GetByIdAsync(Guid id)
    {
        var query = GameIncludes();
        return query.Where(x => x.Id == id).AsSplitQuery().FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Game entity)
    {
        var g = await _context.Games.Include(x => x.GameGenres).Include(x => x.GamePlatforms).Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }

    public Task<List<Game>> GetAllAsync()
    {
        var query = GameIncludes();
        return query.AsSplitQuery().ToListAsync();
    }

    private IIncludableQueryable<Game, Publisher> GameIncludes()
    {
        return _context.Games
            .Include(x => x.GameGenres).ThenInclude(x => x.Genre)
            .Include(x => x.GamePlatforms).ThenInclude(x => x.Platform)
            .Include(x => x.Publisher);
    }
}
