using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models.Payment;
using Gamestore.WebApi.Stubs;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class OrdersController([FromServices] IOrderService orderService) : ControllerBase
{
    private const string VisaPaymentMethodName = "Visa";
    private const string IboxPaymentMethodName = "IBox terminal";
    private const string BankPaymentMethodName = "Bank";
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
        var orders = await _orderService.GetCartByCustomerIdAsync(CustomerStub.Id);

        return Ok(orders);
    }

    // POST: orders
    [HttpPost("payment")]
    public async Task<IActionResult> PayAsync([FromBody] PaymentModelDto payment)
    {
        switch (payment.Method)
        {
            case VisaPaymentMethodName:
                await _orderService.PayWithVisaAsync(payment);
                return Ok();

            case IboxPaymentMethodName:
                await _orderService.PayWithIboxAsync(payment);
                return Ok();

            case BankPaymentMethodName:
                var invoicePdf = await _orderService.CreateInvoicePdf(payment);
                return File(invoicePdf, "application/pdf", "Invoice.pdf");

            default:
                return BadRequest();
        }
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
        await _orderService.RemoveGameFromCartAsync(CustomerStub.Id, key, 1);

        return Ok();
    }
}
