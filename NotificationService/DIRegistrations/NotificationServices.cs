using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.NotificationStrategies;

namespace NotificationService.DIRegistrations;

public static class NotificationServices
{
    public static void Configure(IServiceCollection services)
    {
        services.AddTransient<INotificationService, NotificationService>();
        services.AddSingleton<NotificationContext>();
        services.AddSingleton<INotificationStrategy, EmailNotificationStrategy>();
        services.AddSingleton<INotificationStrategy, SmsNotificationStrategy>();
        services.AddSingleton<INotificationStrategy, PushNotificationStrategy>();

        services.AddSingleton(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            string connectionString = configuration["ServiceBusConnectionstring"];
            string queueName = configuration["GamestoreNotificationQueueName"];
            var context = (NotificationContext)provider.GetRequiredService(typeof(NotificationContext));
            return new NotificationReceiver(connectionString!, queueName!, context);
        });

        services.AddHostedService<NotificationBackgroundService>();
    }
}
