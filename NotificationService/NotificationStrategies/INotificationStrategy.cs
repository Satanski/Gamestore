using NotificationService.Models;

namespace NotificationService.NotificationStrategies;

public interface INotificationStrategy
{
    Task ExecuteAsync(NotificationMessage message);
}
