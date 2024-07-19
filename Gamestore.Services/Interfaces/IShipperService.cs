using Gamestore.BLL.Models;

namespace Gamestore.BLL.Interfaces;

public interface IShipperService
{
    Task<List<ShipperModelDto>> GetAllShippersAsync();
}
