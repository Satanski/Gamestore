using Gamestore.BLL.Models.Payment;
using Gamestore.WebApi.Stubs;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Interfaces;

public interface IPaymentStrategy
{
    Task<IActionResult> ExecuteAsync(PaymentModelDto payment, CustomerStub customerStub);
}
