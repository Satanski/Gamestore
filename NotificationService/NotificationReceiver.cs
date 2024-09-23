using System.Diagnostics;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using NotificationService.Models;
using NotificationService.NotificationStrategies;

namespace NotificationService;

public class NotificationReceiver(string connectionString, string queueName, NotificationContext notificationContext) : IAsyncDisposable
{
    private ServiceBusClient _client;
    private ServiceBusProcessor _processor;
    private bool _isDisposed;

    public async Task ReceiveNotificationAsync()
    {
        _client = new ServiceBusClient(connectionString);
        _processor = _client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;

        await _processor.StartProcessingAsync();
    }

    public async Task StopReceivingNotifications()
    {
        await _processor.StopProcessingAsync();
        _processor.ProcessMessageAsync -= MessageHandler;
        _processor.ProcessErrorAsync -= ErrorHandler;
        await _processor.DisposeAsync();
        await _client.DisposeAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (!_isDisposed)
        {
            if (_client != null)
            {
                await _client.DisposeAsync();
            }

            if (_processor != null)
            {
                _processor.ProcessMessageAsync -= MessageHandler;
                _processor.ProcessErrorAsync -= ErrorHandler;
                await _processor.DisposeAsync();
            }

            _isDisposed = true;
        }
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string notification = args.Message.Body.ToString();
        var message = JsonSerializer.Deserialize<NotificationMessage>(notification);

        foreach (var method in message.NotificationMethods)
        {
            await notificationContext.ExecuteStrategyAsync(method, message!);
        }

        await args.CompleteMessageAsync(args.Message);
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Debug.WriteLine($"{args.Exception.Message}");
        return Task.CompletedTask;
    }
}
