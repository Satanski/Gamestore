using AutoMapper;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Services;
using Microsoft.Extensions.Logging;

namespace Gamestore.BLL.Services;

public class ShipperService(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, ILogger<GameService> logger) : IShipperService
{
    public async Task<List<ShipperModelDto>> GetAllShippersAsync()
    {
        logger.LogInformation("Getting all shippers");
        var shippers = await mongoUnitOfWork.ShipperRepository.GetAllAsync();
        return automapper.Map<List<ShipperModelDto>>(shippers);
    }
}
