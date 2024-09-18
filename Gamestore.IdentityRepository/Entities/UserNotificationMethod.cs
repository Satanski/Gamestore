using System.ComponentModel.DataAnnotations.Schema;
using Gamestore.IdentityRepository.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.IdentityRepository.Entities;

[Table("UserNotificationMethods")]
[PrimaryKey(nameof(UserId), nameof(NotificationType))]
public class UserNotificationMethod
{
    public string UserId { get; set; }

    public string NotificationType { get; set; }

    public AppUser User { get; set; }
}
