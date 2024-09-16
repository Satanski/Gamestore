using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using NotificationService.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NotificationService.NotificationStrategies;

public class EmailNotificationStrategy(IConfiguration configuration) : INotificationStrategy
{
    public async Task ExecuteAsync(NotificationMessage message)
    {
        Debug.WriteLine($"Sending email to {message.To}: {message.Subject}");

        var apiKey = configuration["SendGridApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("pawel.stafinski@gmail.com", "Pawel S");
        var subject = message.Subject;
        var to = new EmailAddress(message.To);
        var plainTextContent = message.Body;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, string.Empty);

        var response = await client.SendEmailAsync(msg);
        Debug.WriteLine(response.StatusCode);
    }
}
