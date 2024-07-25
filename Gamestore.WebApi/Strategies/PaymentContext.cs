using System.Globalization;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Strategies;

public class PaymentContext(IEnumerable<IPaymentStrategy> strategies)
{
    private readonly Dictionary<string, IPaymentStrategy> _strategies = strategies.ToDictionary(s => s.GetType().Name.Replace("PaymentStrategy", string.Empty), s => s);

    public async Task<IActionResult> ExecuteStrategyAsync(string method, PaymentModelDto payment, CustomerDto customer)
    {
        method = MakeMethodNameFromRequestClassNameFriendly(method);

        if (_strategies.TryGetValue(method, out var strategy))
        {
            return await strategy.ExecuteAsync(payment, customer);
        }

        return new BadRequestResult();
    }

    private static string MakeMethodNameFromRequestClassNameFriendly(string method)
    {
        var index = method.IndexOf(' ');
        if (index >= 0)
        {
            method = method.Remove(index);
        }

        method = method.ToLower(CultureInfo.InvariantCulture);
        method = char.ToUpper(method[0]) + method[1..];
        return method;
    }
}
