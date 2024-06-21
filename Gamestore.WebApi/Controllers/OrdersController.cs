using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models.Payment;
using Gamestore.WebApi.Strategies;
using Gamestore.WebApi.Stubs;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class OrdersController([FromServices] IOrderService orderService, [FromServices] PaymentContext paymentContext) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    // GET: orders
    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync()
    {
        var orders = await _orderService.GetAllOrdersAsync();

        return Ok(orders);
    }

    // GET: orders/GUID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByIdAsync(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);

        return order == null ? NotFound() : Ok(order);
    }

    // GET: orders/GUID/details
    [HttpGet("{id}/details")]
    public async Task<IActionResult> GetOrderDetailsByOrderIdAsync(Guid id)
    {
        var order = await _orderService.GetOrderDetailsByOrderIdAsync(id);

        return order == null ? NotFound() : Ok(order);
    }

    // GET: orders/cart
    [HttpGet("cart")]
    public async Task<IActionResult> GetCartAsync()
    {
        var customerStub = new CustomerStub();
        var orders = await _orderService.GetCartByCustomerIdAsync(customerStub.Id);

        return Ok(orders);
    }

    // POST: orders
    [HttpPost("payment")]
    public async Task<IActionResult> PayAsync([FromBody] PaymentModelDto payment)
    {
        var customerStub = new CustomerStub();

        var result = await paymentContext.ExecuteStrategyAsync(payment.Method, payment, customerStub);

        return Ok(result);
    }

    // GET: orders/cart
    [HttpGet("payment-methods")]
    public IActionResult GetPaymentMethods()
    {
        var paymentMethods = _orderService.GetPaymentMethods();

        return Ok(paymentMethods);
    }

    // DELETE: orders
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderByIdAsync(Guid id)
    {
        await _orderService.DeleteOrderByIdAsync(id);

        return Ok();
    }

    // DELETE: orders
    [HttpDelete("cart/{key}")]
    public async Task<IActionResult> DeleteOrderByIdAsync(string key)
    {
        var customerStub = new CustomerStub();
        await _orderService.RemoveGameFromCartAsync(customerStub.Id, key, 1);

        return Ok();
    }
}
