using Gamestore.BLL.Models;

namespace Gamestore.BLL.Interfaces;

public interface IOrderService
{
    Task DeleteOrderByIdAsync(Guid orderId);

    Task<IEnumerable<OrderModelDto>> GetAllOrdersAsync();

    Task<OrderModelDto> GetOrderByIdAsync(Guid orderId);
}
