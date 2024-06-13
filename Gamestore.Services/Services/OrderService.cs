using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Interfaces;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace Gamestore.BLL.Services;

public class OrderService(IUnitOfWork unitOfWork, IMapper automapper, ILogger<OrderService> logger) : IOrderService
{
    public async Task<OrderModelDto> GetOrderByIdAsync(Guid orderId)
    {
        logger.LogInformation("Getting order by id: {orderId}", orderId);
        var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId);

        return order == null ? throw new GamestoreException($"No order found with given id: {orderId}") : automapper.Map<OrderModelDto>(order);
    }

    public async Task DeleteOrderByIdAsync(Guid orderId)
    {
        logger.LogInformation("Deleting order by id: {orderId}", orderId);

        var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId);
        if (order != null)
        {
            unitOfWork.OrderRepository.Delete(order);
            await unitOfWork.SaveAsync();
        }
        else
        {
            throw new GamestoreException($"No order found with given id: {orderId}");
        }
    }
}
