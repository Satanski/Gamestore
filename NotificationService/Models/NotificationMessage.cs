namespace NotificationService.Models;

public record NotificationMessage
{
    public string To { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public string PhoneNumber { get; set; }

    public List<string> NotificationMethods { get; set; }
}
