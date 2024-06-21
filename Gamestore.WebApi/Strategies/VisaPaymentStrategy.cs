using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models.Payment;
using Gamestore.WebApi.Interfaces;
using Gamestore.WebApi.Stubs;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Strategies;

public class VisaPaymentStrategy(IOrderService orderService) : IPaymentStrategy
{
    public async Task<IActionResult> ExecuteAsync(PaymentModelDto payment, CustomerStub customerStub)
    {
        await orderService.PayWithVisaAsync(payment, customerStub);
        return new OkResult();
    }
}
