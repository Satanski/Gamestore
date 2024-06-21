using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models.Payment;
using Gamestore.WebApi.Interfaces;
using Gamestore.WebApi.Stubs;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Strategies;

public class BankPaymentStrategy(IOrderService orderService) : IPaymentStrategy
{
    public async Task<IActionResult> ExecuteAsync(PaymentModelDto payment, CustomerStub customerStub)
    {
        var invoicePdf = await orderService.CreateInvoicePdf(payment, customerStub);
        return new FileContentResult(invoicePdf, "application/pdf") { FileDownloadName = "Invoice.pdf" };
    }
}
