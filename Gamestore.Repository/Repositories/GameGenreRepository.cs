﻿using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class GameGenreRepository(GamestoreContext context) : RepositoryBase<GameGenres>(context), IGameGenreRepository
{
    private readonly GamestoreContext _context = context;

    public Task<List<GameGenres>> GetByGameIdAsync(Guid id)
    {
        return _context.GameGenres.Where(x => x.GameId == id).ToListAsync();
    }

    public Task<List<GameGenres>> GetAllAsync()
    {
        return _context.GameGenres.ToListAsync();
    }
}
