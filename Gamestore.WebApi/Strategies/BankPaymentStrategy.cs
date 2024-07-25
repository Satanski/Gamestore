using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Strategies;

public class BankPaymentStrategy(IOrderService orderService) : IPaymentStrategy
{
    public async Task<IActionResult> ExecuteAsync(PaymentModelDto payment, CustomerDto customer)
    {
        var invoicePdf = await orderService.CreateInvoicePdf(payment, customer);
        return new FileContentResult(invoicePdf, "application/pdf") { FileDownloadName = "Invoice.pdf" };
    }
}
