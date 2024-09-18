using System.Diagnostics;
using NotificationService.Models;

namespace NotificationService.NotificationStrategies;

public class PushNotificationStrategy : INotificationStrategy
{
    public Task ExecuteAsync(NotificationMessage message)
    {
        Debug.WriteLine($"Pushing to {message.To}: {message.Subject}");
        return Task.CompletedTask;
    }
}
