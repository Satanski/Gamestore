using System.Globalization;
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
using Gamestore.DAL.Enums;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.WebApi.Stubs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Gamestore.BLL.Services;

public class OrderService(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, ILogger<OrderService> logger, IOptions<PaymentServiceConfiguration> paymentServiceConfiguration) : IOrderService
{
    private readonly VisaPaymentValidator _visaPaymentValidator = new();

    public async Task<OrderModelDto> GetOrderByIdAsync(Guid orderId)
    {
        logger.LogInformation("Getting order by id: {orderId}", orderId);
        var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId);

        if (order is null)
        {
            int id = GuidHelpers.GuidToInt(orderId);
            var o = await mongoUnitOfWork.OrderRepository.GetByIdAsync(id);
            order = automapper.Map<Order>(o);
        }

        return order == null ? throw new GamestoreException($"No order found with given id: {orderId}") : automapper.Map<OrderModelDto>(order);
    }

    public async Task<List<OrderModelDto>> GetAllOrdersAsync()
    {
        logger.LogInformation("Getting all orders");
        var orders = await unitOfWork.OrderRepository.GetAllAsync();

        List<OrderModelDto> orderModels = [];
        AddSQLServerOrdersToDtoList(automapper, orders, orderModels);

        return orderModels;
    }

    public async Task<List<OrderModelDto>> GetOrdersHistoryAsync(string? startDate, string? endDate)
    {
        logger.LogInformation("Getting orders history");
        List<OrderModelDto> orderModels = [];

        ParseDateRangeToDateTimeFormat(ref startDate, ref endDate, out var startD, out var endD);

        var orders = await unitOfWork.OrderRepository.GetOrdersByDateRangeAsync(startD, endD);
        AddSQLServerOrdersToDtoList(automapper, orders, orderModels);

        var ordersFromMongo = await mongoUnitOfWork.OrderRepository.GetAllAsync();
        AddMongoOrdersToDtoList(automapper, ordersFromMongo, startD, endD, orderModels);

        return orderModels;
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
        List<OrderDetailsDto> orederDetails = [];

        var order = await unitOfWork.OrderRepository.GetWithDetailsByIdAsync(orderId);
        if (order is not null)
        {
            AddSQLServerOrderDetailsToDtoList(automapper, order, orederDetails);
            return orederDetails;
        }

        int id = GuidHelpers.GuidToInt(orderId);
        order = automapper.Map<Order>(await mongoUnitOfWork.OrderRepository.GetByIdAsync(id));
        if (order is not null)
        {
            await AddMongoDBOrderDetailsToDtoList(mongoUnitOfWork, automapper, order, orederDetails);
            return orederDetails;
        }

        return orederDetails;
    }

    public async Task<List<OrderDetailsDto>> GetCartByCustomerIdAsync(Guid customerId)
    {
        logger.LogInformation("Getting cart");
        var order = await unitOfWork.OrderRepository.GetOrderByCustomerIdAsync(customerId);

        List<OrderDetailsDto> orederDetails = [];
        AddSQLServerOrderDetailsToDtoList(automapper, order, orederDetails);

        return orederDetails;
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
        await ProcessOrderAfterPayment(unitOfWork, customer);
    }

    public async Task PayWithVisaAsync(PaymentModelDto payment, CustomerStub customer)
    {
        logger.LogInformation("Executing payment by Visa {@payment.Model}", payment.Model);
        await _visaPaymentValidator.ValidateVisaPayment(payment);

        var visaPaymentModel = await CreateVisaPaymentModel(unitOfWork, automapper, payment, customer);
        string serviceUrl = paymentServiceConfiguration.Value.VisaServiceUrl;

        await MakePaymentServiceRequest(visaPaymentModel, serviceUrl);
        await ProcessOrderAfterPayment(unitOfWork, customer);
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

    private static async Task ProcessOrderAfterPayment(IUnitOfWork unitOfWork, CustomerStub customer)
    {
        var order = await unitOfWork.OrderRepository.GetByCustomerIdAsync(customer.Id);
        if (order != null)
        {
            await SetOrderStatusToPaidInSQLServer(unitOfWork, order);
            await UpdateProductQuanityInSQLServer(unitOfWork, order);
            await unitOfWork.SaveAsync();
        }
    }

    private static async Task SetOrderStatusToPaidInSQLServer(IUnitOfWork unitOfWork, Order? order)
    {
        order.Status = OrderStatus.Paid;
        await unitOfWork.OrderRepository.UpdateAsync(order);
    }

    private static async Task UpdateProductQuanityInSQLServer(IUnitOfWork unitOfWork, Order? order)
    {
        var gameOrders = await unitOfWork.OrderGameRepository.GetByOrderIdAsync(order.Id);
        foreach (var gameOrder in gameOrders)
        {
            var product = await unitOfWork.GameRepository.GetByIdAsync(gameOrder.ProductId);
            if (product != null)
            {
                product.UnitInStock -= gameOrder.Quantity;
            }
        }
    }

    private static void AddSQLServerOrderDetailsToDtoList(IMapper automapper, Order? order, List<OrderDetailsDto> orderDetails)
    {
        if (order.OrderGames.Count > 0)
        {
            orderDetails.AddRange(automapper.Map<List<OrderDetailsDto>>(order.OrderGames));
        }
    }

    private static async Task AddMongoDBOrderDetailsToDtoList(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, Order order, List<OrderDetailsDto> orderDetails)
    {
        int id = GuidHelpers.GuidToInt(order.Id);
        var mongoOrderDetails = await mongoUnitOfWork.OrderDetailRepository.GetByOrderIdAsync(id);

        order.OrderGames = [];
        foreach (var od in mongoOrderDetails)
        {
            order.OrderGames.Add(new OrderGame() { OrderId = order.Id, ProductId = GuidHelpers.IntToGuid(od.ProductId), Price = od.UnitPrice, Quantity = od.Quantity, Discount = od.Discount });
        }

        orderDetails.AddRange(automapper.Map<List<OrderDetailsDto>>(order.OrderGames));
    }

    private static void AddSQLServerOrdersToDtoList(IMapper automapper, List<Order> orders, List<OrderModelDto> orderModels)
    {
        orderModels.AddRange(automapper.Map<List<OrderModelDto>>(orders));
    }

    private static void AddMongoOrdersToDtoList(IMapper automapper, List<MongoOrder> ordersFromMongo, DateTime startD, DateTime endD, List<OrderModelDto> orderModels)
    {
        var mongoOrdersWitheDateTime = automapper.Map<List<MongoOrderModel>>(ordersFromMongo);
        var mongoOrdersByDateRange = mongoOrdersWitheDateTime.Where(x => x.Date >= startD && x.Date <= endD);
        var filteredMOngoOrders = automapper.Map<List<OrderModelDto>>(mongoOrdersByDateRange);

        orderModels.AddRange(filteredMOngoOrders);
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

    private static void ParseDateRangeToDateTimeFormat(ref string? startDate, ref string? endDate, out DateTime startD, out DateTime endD)
    {
        string format = "MMM dd yyyy";
        startD = DateTime.Now.AddYears(-1000);
        endD = DateTime.Now;
        try
        {
            if (!string.IsNullOrEmpty(startDate))
            {
                startDate = startDate.Substring(4, 11);
                startD = DateTime.ParseExact(startDate, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

                if (!string.IsNullOrEmpty(endDate))
                {
                    endDate = endDate.Substring(4, 11);
                    endD = DateTime.ParseExact(endDate, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                }
            }
        }
        catch (FormatException)
        {
            throw new GamestoreException("Date range is in wrong format or corrupted");
        }
    }
}
