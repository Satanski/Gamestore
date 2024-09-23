using System.Diagnostics;
using NotificationService.Models;

namespace NotificationService.NotificationStrategies;

public class SmsNotificationStrategy : INotificationStrategy
{
    public Task ExecuteAsync(NotificationMessage message)
    {
        Debug.WriteLine($"Sending sms to {message.To}: {message.Subject}");
        return Task.CompletedTask;
    }
}
