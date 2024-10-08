﻿using Gamestore.IdentityRepository.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.IdentityRepository.Identity;

public class AppUser : IdentityUser
{
    public DateTime BannedTill { get; set; }

    public ICollection<IdentityUserRole<string>> UserRoles { get; set; }

    public ICollection<UserNotificationMethod> NotificationMethods { get; set; }
}
