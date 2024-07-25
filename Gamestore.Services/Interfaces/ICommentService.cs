using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.BLL.Interfaces;

public interface ICommentService
{
    Task BanCustomerFromCommentingAsync(BanDto banDetails, UserManager<AppUser> userManager);

    List<string> GetBanDurations();
}
