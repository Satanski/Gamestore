using System.Text;
using System.Text.Json;
using AutoMapper;
using Gamestore.BLL.Configurations;
using Gamestore.BLL.Documents;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.WebApi.Stubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Gamestore.BLL.Services;

public class OrderService(IUnitOfWork unitOfWork, IMapper automapper, ILogger<OrderService> logger, IOptions<PaymentServiceConfiguration> paymentServiceConfiguration) : IOrderService
{
    private readonly VisaPaymentValidator _visaPaymentValidator = new();

    public async Task<OrderModelDto> GetOrderByIdAsync(Guid orderId)
    {
        logger.LogInformation("Getting order by id: {orderId}", orderId);
        var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId);

        return order == null ? throw new GamestoreException($"No order found with given id: {orderId}") : automapper.Map<OrderModelDto>(order);
    }

    public async Task<IEnumerable<OrderModelDto>> GetAllOrdersAsync()
    {
        logger.LogInformation("Getting all orders");
        var orders = await unitOfWork.OrderRepository.GetAllAsync();

        List<OrderModelDto> orderModels = [];
        AddOrderModelsToDtoList(automapper, orders, orderModels);

        return orderModels.AsEnumerable();
    }

    public async Task DeleteOrderByIdAsync(Guid orderId)
    {
        logger.LogInformation("Deleting order by id: {orderId}", orderId);

        var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId);
        if (order != null)
        {
            await DeleteOrder(unitOfWork, order);
        }
        else
        {
            throw new GamestoreException($"No order found with given id: {orderId}");
        }
    }

    public async Task<List<OrderDetailsDto>> GetOrderDetailsByOrderIdAsync(Guid orderId)
    {
        logger.LogInformation("Getting order details by id: {orderId}", orderId);
        var order = await unitOfWork.OrderRepository.GetWithDetailsByIdAsync(orderId);

        List<OrderDetailsDto> orederDetails = [];
        AddOrderDetailsToDtoList(automapper, order, orederDetails);

        return orederDetails;
    }

    public async Task<IEnumerable<OrderDetailsDto>> GetCartByCustomerIdAsync(Guid customerId)
    {
        logger.LogInformation("Getting cart");
        var order = await unitOfWork.OrderRepository.GetOrderByCustomerIdAsync(customerId);

        List<OrderDetailsDto> orederDetails = [];
        AddOrderDetailsToDtoList(automapper, order, orederDetails);

        return orederDetails.AsEnumerable();
    }

    public async Task RemoveGameFromCartAsync(Guid customerId, string gameKey, int quantity)
    {
        logger.LogInformation("Removing game from cart: {@gameKey}", gameKey);

        var exisitngOrder = await unitOfWork.OrderRepository.GetByCustomerIdAsync(customerId) ?? throw new GamestoreException($"No order found with given customerId: {customerId}");
        var orderGame = exisitngOrder.OrderGames.Find(x => x.Product.Key == gameKey);

        if (orderGame != null)
        {
            var expectedQuantity = orderGame.Quantity - quantity;
            if (expectedQuantity <= 0)
            {
                unitOfWork.OrderGameRepository.Delete(orderGame);
            }
            else
            {
                orderGame.Quantity = expectedQuantity;
                await unitOfWork.OrderGameRepository.UpdateAsync(orderGame);
            }

            await unitOfWork.SaveAsync();

            if (exisitngOrder.OrderGames.Count == 0)
            {
                unitOfWork.OrderRepository.Delete(exisitngOrder);
                await unitOfWork.SaveAsync();
            }
        }
        else
        {
            throw new GamestoreException($"No order found for given game key: {gameKey}");
        }
    }

    public async Task<byte[]> CreateInvoicePdf(PaymentModelDto payment, CustomerStub customer)
    {
        logger.LogInformation("Creating invoice {@payment}", payment);

        var order = await unitOfWork.OrderRepository.GetByCustomerIdAsync(customer.Id) ?? throw new GamestoreException("There is no order for given customer");
        var sum = await CalculateAmountToPay(unitOfWork, customer);

        QuestPDF.Settings.License = LicenseType.Community;
        var document = new Invoice(order, (double)sum);
        byte[] pdfBytes = document.GeneratePdf();

        return pdfBytes;
    }

    public async Task PayWithIboxAsync(PaymentModelDto payment, CustomerStub customer)
    {
        logger.LogInformation("Executing payment by IBox {@payment}", payment);

        var order = await unitOfWork.OrderRepository.GetByCustomerIdAsync(customer.Id) ?? throw new GamestoreException("There is no order for given customer");
        var iboxPaymentModel = await CreateIboxPaymentModel(unitOfWork, customer, order);

        string serviceUrl = paymentServiceConfiguration.Value.IboxServiceUrl;
        await MakePaymentServiceRequest(iboxPaymentModel, serviceUrl);
        await CloseOrderByCustomer(unitOfWork, customer);
    }

    public async Task PayWithVisaAsync(PaymentModelDto payment, CustomerStub customer)
    {
        logger.LogInformation("Executing payment by Visa {@payment.Model}", payment.Model);
        await _visaPaymentValidator.ValidateVisaPayment(payment);

        var visaPaymentModel = await CreateVisaPaymentModel(unitOfWork, automapper, payment, customer);
        string serviceUrl = paymentServiceConfiguration.Value.VisaServiceUrl;

        await MakePaymentServiceRequest(visaPaymentModel, serviceUrl);
        await CloseOrderByCustomer(unitOfWork, customer);
    }

    public PaymentMethodsDto GetPaymentMethods()
    {
        var paymentMethods = new PaymentMethodsDto
        {
            PaymentMethods =
            [
                new()
                {
                    Title = "Bank",
                    Description = "Bank transfer",
                    ImageUrl = "https://cdn1.vectorstock.com/i/1000x1000/17/80/bank-transfer-icon-vector-5791780.jpg",
                },
                new()
                {
                    Title = "Visa",
                    Description = "Visa payment",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/512/4697/4697352.png",
                },
                new()
                {
                    Title = "IBox terminal",
                    Description = "Ibox payment",
                    ImageUrl = "https://logowik.com/content/uploads/images/ibox9043.logowik.com.webp",
                },
            ],
        };

        return paymentMethods;
    }

    private static void DeleteOrderGames(IUnitOfWork unitOfWork, Order? order)
    {
        foreach (var item in order.OrderGames)
        {
            unitOfWork.OrderGameRepository.Delete(item);
        }
    }

    private static async Task DeleteOrder(IUnitOfWork unitOfWork, Order order)
    {
        DeleteOrderGames(unitOfWork, order);
        unitOfWork.OrderRepository.Delete(order);
        await unitOfWork.SaveAsync();
    }

    private static async Task CloseOrderByCustomer(IUnitOfWork unitOfWork, CustomerStub customer)
    {
        var order = await unitOfWork.OrderRepository.GetByCustomerIdAsync(customer.Id);
        if (order != null)
        {
            DeleteOrderGames(unitOfWork, order);
            unitOfWork.OrderRepository.Delete(order);
            await unitOfWork.SaveAsync();
        }
    }

    private static void AddOrderDetailsToDtoList(IMapper automapper, Order? order, List<OrderDetailsDto> orederDetails)
    {
        if (order != null && order.OrderGames.Count > 0)
        {
            foreach (var orderGame in order.OrderGames)
            {
                orederDetails.Add(automapper.Map<OrderDetailsDto>(orderGame));
            }
        }
    }

    private static void AddOrderModelsToDtoList(IMapper automapper, List<Order> orders, List<OrderModelDto> orderModels)
    {
        foreach (var order in orders)
        {
            orderModels.Add(automapper.Map<OrderModelDto>(order));
        }
    }

    private static async Task<double?> CalculateAmountToPay(IUnitOfWork unitOfWork, CustomerStub customer)
    {
        var order = await unitOfWork.OrderRepository.GetByCustomerIdAsync(customer.Id);
        var orderGames = await unitOfWork.OrderGameRepository.GetByOrderIdAsync(order.Id);

        double? sum = 0;
        foreach (var orderGame in orderGames)
        {
            if (orderGame.Discount is not null and not 0)
            {
                sum += (orderGame.Price - (orderGame.Price * ((double)orderGame.Discount / 100))) * orderGame.Quantity;
            }
            else
            {
                sum += orderGame.Price;
            }
        }

        return sum;
    }

    private static async Task MakePaymentServiceRequest(object paymentModel, string serviceUrl)
    {
        var json = JsonSerializer.Serialize(paymentModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpClient client = new HttpClient();

        var response = await client.PostAsync(serviceUrl, content);
        response.EnsureSuccessStatusCode();
    }

    private static async Task<VisaMicroservicePaymentModel> CreateVisaPaymentModel(IUnitOfWork unitOfWork, IMapper automapper, PaymentModelDto payment, CustomerStub customer)
    {
        var visaPaymentModel = automapper.Map<VisaMicroservicePaymentModel>(payment);

        var sum = await CalculateAmountToPay(unitOfWork, customer);
        visaPaymentModel.TransactionAmount = (double)sum;
        return visaPaymentModel;
    }

    private static async Task<IboxPaymentModel> CreateIboxPaymentModel(IUnitOfWork unitOfWork, CustomerStub customer, Order order)
    {
        var iboxPaymentModel = new IboxPaymentModel()
        {
            InvoiceNumber = order.Id,
            AccountNumber = customer.Id,
        };

        var sum = await CalculateAmountToPay(unitOfWork, customer);
        iboxPaymentModel.TransactionAmount = (decimal?)sum;
        return iboxPaymentModel;
    }
}
