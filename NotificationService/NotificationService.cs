using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using NotificationService.Models;

namespace NotificationService;

public class NotificationService(IConfiguration configuration) : INotificationService
{
    private readonly string _connectionString = configuration["ServiceBusConnectionstring"]!;
    private readonly string _queueName = configuration["GamestoreNotificationQueueName"]!;

    public async Task NotifyUser(NotificationMessage notificationMessage)
    {
        await using var client = new ServiceBusClient(_connectionString);
        ServiceBusSender sender = client.CreateSender(_queueName);

        string messageBody = JsonSerializer.Serialize(notificationMessage);
        ServiceBusMessage sbMessage = new ServiceBusMessage(messageBody);

        await sender.SendMessageAsync(sbMessage);
    }
}
