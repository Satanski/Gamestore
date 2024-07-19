using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Interfaces;

public interface IPaymentStrategy
{
    Task<IActionResult> ExecuteAsync(PaymentModelDto payment, CustomerDto customer);
}
