﻿using Gamestore.BLL.Filtering.Models;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;

namespace Gamestore.BLL.BanHandler;

public interface IGameProcessingPipelineHandler
{
    void SetNext(IGameProcessingPipelineHandler nextHandler);

    Task<IQueryable<Game>> HandleAsync(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, GameFiltersDto filters, IQueryable<Game> query);
}
