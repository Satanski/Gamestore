using System.Globalization;
using NotificationService.Models;

namespace NotificationService.NotificationStrategies;

public class NotificationContext(IEnumerable<INotificationStrategy> strategies)
{
    private readonly Dictionary<string, INotificationStrategy> _strategies = strategies.ToDictionary(s => s.GetType().Name.Replace("NotificationStrategy", string.Empty), s => s);

    public Task ExecuteStrategyAsync(string method, NotificationMessage message)
    {
        method = MakeMethodNameClassNameFriendly(method);

        if (_strategies.TryGetValue(method, out var strategy))
        {
            return strategy.ExecuteAsync(message);
        }

        return Task.CompletedTask;
    }

    private static string MakeMethodNameClassNameFriendly(string method)
    {
        method = method.ToLower(CultureInfo.InvariantCulture);
        method = char.ToUpper(method[0]) + method[1..];
        return method;
    }
}
