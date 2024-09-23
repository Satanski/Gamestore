using NotificationService.Models;

namespace NotificationService;

public interface INotificationService
{
    Task NotifyUser(NotificationMessage notificationMessage);
}
