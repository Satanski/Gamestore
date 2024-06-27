﻿using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.Filtering;

public interface IFilterService
{
    Task<List<Game>> FilterGames(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters);
}
