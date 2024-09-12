namespace NotificationService.Models;

public static class NotificationMethods
{
    public static IReadOnlyList<NotificationMethod> Methods { get; } =
    [
        new NotificationMethod() { Name = "SMS" },
        new NotificationMethod() { Name = "Push" },
        new NotificationMethod() { Name = "Email" },
    ];
}
