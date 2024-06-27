﻿using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.BLL.BanHandler;

public interface IFilterHandler
{
    void SetNext(IFilterHandler nextHandler);

    Task<List<Game>> HandleAsync(IUnitOfWork unitOfWork, List<Game> filteredGames, GameFilters filters);
}
