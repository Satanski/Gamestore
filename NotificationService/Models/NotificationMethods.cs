namespace NotificationService.Models;

public static class NotificationMethods
{
    public static IReadOnlyList<NotificationMethod> Methods { get; } =
    [
        new NotificationMethod() { Name = "Sms" },
        new NotificationMethod() { Name = "Push" },
        new NotificationMethod() { Name = "Email" },
    ];
}
