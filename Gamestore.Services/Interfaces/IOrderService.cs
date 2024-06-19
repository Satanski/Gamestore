using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.Interfaces;

public interface IOrderService
{
    Task DeleteOrderByIdAsync(Guid orderId);

    Task<IEnumerable<OrderModelDto>> GetAllOrdersAsync();

    Task<IEnumerable<OrderDetailsDto>> GetCartByCustomerIdAsync(Guid customerId);

    Task<OrderModelDto> GetOrderByIdAsync(Guid orderId);

    Task<List<OrderDetailsDto>> GetOrderDetailsByOrderIdAsync(Guid orderId);

    Task<byte[]> CreateInvoicePdf(PaymentModelDto payment, CustomerStub customer);

    Task PayWithVisaAsync(PaymentModelDto payment, CustomerStub customer);

    Task PayWithIboxAsync(PaymentModelDto payment, CustomerStub customer);

    Task RemoveGameFromCartAsync(Guid customerId, string gameKey, int quantity);

    PaymentMethodsDto GetPaymentMethods();
}
