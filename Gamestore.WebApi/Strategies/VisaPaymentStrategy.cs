using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Strategies;

public class VisaPaymentStrategy(IOrderService orderService) : IPaymentStrategy
{
    public async Task<IActionResult> ExecuteAsync(PaymentModelDto payment, CustomerDto customer)
    {
        await orderService.PayWithVisaAsync(payment, customer);
        return new OkResult();
    }
}
