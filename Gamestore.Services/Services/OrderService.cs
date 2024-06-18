using System.Text;
using System.Text.Json;
using AutoMapper;
using Gamestore.BLL.Documents;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Gamestore.BLL.Services;

public class OrderService(IUnitOfWork unitOfWork, IMapper automapper, ILogger<OrderService> logger, IConfiguration config) : IOrderService
{
    private readonly IConfiguration _config = config;
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
            DeleteOrderGames(unitOfWork, order);
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

    public async Task<byte[]> CreateInvoicePdf(PaymentModelDto payment)
    {
        logger.LogInformation("Creating invoice {@payment}", payment);

        var t = await Task.Run(() =>
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var document = new Invoice(payment);
            byte[] pdfBytes = document.GeneratePdf();

            return pdfBytes;
        });

        return t;
    }

    public async Task PayWithIboxAsync(PaymentModelDto payment)
    {
        logger.LogInformation("Executing payment by IBox {@payment}", payment);
        var iboxPaymentModel = automapper.Map<IboxPaymentModel>(payment);

        var json = JsonSerializer.Serialize(iboxPaymentModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using HttpClient client = new HttpClient();
        var paymentServiceUrl = GetPeymentServiceUrl();

        var response = await client.PostAsync($"{paymentServiceUrl}/ibox", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task PayWithVisaAsync(PaymentModelDto payment)
    {
        logger.LogInformation("Executing payment by Visa {@payment.Model}", payment.Model);
        await _visaPaymentValidator.ValidateVisaPayment(payment);

        var microservicePaymentModel = automapper.Map<VisaMicroservicePaymentModel>(payment);

        var json = JsonSerializer.Serialize(microservicePaymentModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using HttpClient client = new HttpClient();
        var paymentServiceUrl = GetPeymentServiceUrl();

        var response = await client.PostAsync($"{paymentServiceUrl}/visa", content);
        response.EnsureSuccessStatusCode();
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

    private string GetPeymentServiceUrl()
    {
        return _config.GetSection("PaymentServiceUrl").Value;
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
        unitOfWork.OrderRepository.Delete(order);
        await unitOfWork.SaveAsync();
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
}
