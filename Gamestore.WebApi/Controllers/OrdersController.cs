using Gamestore.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class OrdersController([FromServices] IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    // GET: orders
    [HttpGet]
    public async Task<IActionResult> GetGenresAsync()
    {
        var orders = await _orderService.GetAllOrdersAsync();

        return orders.Any() ? Ok(orders) : NotFound();
    }

    // GET: orders/GUID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByIdAsync(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);

        return order == null ? NotFound() : Ok(order);
    }

    // DELETE: orders
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderByIdAsync(Guid id)
    {
        await _orderService.DeleteOrderByIdAsync(id);

        return Ok();
    }
}
