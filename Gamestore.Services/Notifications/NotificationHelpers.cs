using Gamestore.DAL.Entities;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NotificationService;
using NotificationService.Models;

namespace Gamestore.BLL.Notifications;

internal static class NotificationHelpers
{
    public static IEnumerable<string> GetNotificationMethods()
    {
        return NotificationMethods.Methods.Select(x => x.Name);
    }

    public static IEnumerable<string> GetUserNotificationMethods(AppUser user)
    {
        var userNotificationMethods = user.NotificationMethods;

        return userNotificationMethods?.Select(x => x.NotificationType) ?? [];
    }

    internal static async Task NotifyUserOrderStatusChanged(Order? order, string status, INotificationService notificationService, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user != null)
        {
            var usr = await userManager.GetUserAsync(user);
            if (usr != null)
            {
                var methods = GetUserNotificationMethods(usr);
                var message = new NotificationMessage() { To = usr.Email!, Subject = $"Order no. {order.Id} received new status", Body = $"New order status: {status}", NotificationMethods = methods.ToList() };
                await notificationService.NotifyUser(message);
            }
        }
    }
}
