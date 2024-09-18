using Microsoft.Extensions.Hosting;

namespace NotificationService;

public class NotificationBackgroundService(NotificationReceiver notificationReceiver) : BackgroundService
{
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await notificationReceiver.StopReceivingNotifications();
        await base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await notificationReceiver.ReceiveNotificationAsync();
    }
}
