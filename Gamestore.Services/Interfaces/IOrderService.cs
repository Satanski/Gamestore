using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;

namespace Gamestore.BLL.Interfaces;

public interface IOrderService
{
    Task DeleteOrderByIdAsync(Guid orderId);

    Task<List<OrderModelDto>> GetAllOrdersAsync();

    Task<List<OrderDetailsDto>> GetCartByCustomerIdAsync(Guid customerId);

    Task<OrderModelDto> GetOrderByIdAsync(Guid orderId);

    Task<List<OrderDetailsDto>> GetOrderDetailsByOrderIdAsync(Guid orderId);

    Task<byte[]> CreateInvoicePdf(PaymentModelDto payment, CustomerDto customer);

    Task PayWithVisaAsync(PaymentModelDto payment, CustomerDto customer);

    Task PayWithIboxAsync(PaymentModelDto payment, CustomerDto customer);

    Task RemoveGameFromCartAsync(Guid customerId, string gameKey, int quantity);

    PaymentMethodsDto GetPaymentMethods();

    Task<List<OrderModelDto>> GetOrdersHistoryAsync(string? startDate, string? endDate);
}
