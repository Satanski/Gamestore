using Gamestore.BLL.Identity.Extensions;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.WebApi.Strategies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class OrdersController([FromServices] IOrderService orderService, [FromServices] PaymentContext paymentContext) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    // GET: orders
    [HttpGet]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> GetOrdersAsync()
    {
        var orders = await _orderService.GetAllOrdersAsync();

        return Ok(orders);
    }

    // GET: orders/GUID
    [HttpGet("{id}")]
    [Authorize(Roles = "User, Manager")]
    public async Task<IActionResult> GetOrderByIdAsync(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);

        return order == null ? NotFound() : Ok(order);
    }

    // GET: orders
    [HttpGet("history")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> GetOrdersHistoryAsync([FromQuery] string? start, [FromQuery] string? end)
    {
        var orders = await _orderService.GetOrdersHistoryAsync(start, end);

        return Ok(orders);
    }

    // GET: orders/GUID/details
    [HttpGet("{id}/details")]
    [Authorize(Roles = "User, Manager")]
    public async Task<IActionResult> GetOrderDetailsByOrderIdAsync(Guid id)
    {
        var order = await _orderService.GetOrderDetailsByOrderIdAsync(id);

        return order == null ? NotFound() : Ok(order);
    }

    // GET: orders/cart
    [HttpGet("cart")]
    [Authorize(Roles = "User, Manager")]
    public async Task<IActionResult> GetCartAsync()
    {
        var userId = new Guid(User.GetJwtSubjectId());
        var orders = await _orderService.GetCartByCustomerIdAsync(userId);

        return Ok(orders);
    }

    // POST: orders
    [HttpPost("payment")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> PayAsync([FromBody] PaymentModelDto payment)
    {
        var id = User.GetJwtSubjectId();
        var userName = User.GetJwtSubject();
        var customer = new CustomerDto() { Id = new Guid(id!), Name = userName };

        return await paymentContext.ExecuteStrategyAsync(payment.Method, payment, customer);
    }

    // GET: orders/cart
    [HttpGet("payment-methods")]
    [Authorize(Roles = "User, Manager")]
    public IActionResult GetPaymentMethods()
    {
        var paymentMethods = _orderService.GetPaymentMethods();

        return Ok(paymentMethods);
    }

    // DELETE: orders
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeleteOrderByIdAsync(Guid id)
    {
        await _orderService.DeleteOrderByIdAsync(id);

        return Ok();
    }

    // DELETE: orders
    [HttpDelete("cart/{key}")]
    [Authorize(Roles = "User, Manager")]
    public async Task<IActionResult> DeleteOrderByIdAsync(string key)
    {
        var userId = new Guid(User.GetJwtSubjectId());

        await _orderService.RemoveGameFromCartAsync(userId, key, 1);

        return Ok();
    }
}
