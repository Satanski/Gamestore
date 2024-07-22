using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.BLL.BanHandler;

public interface IBanService
{
    Task BanCustomerFromCommentingAsync(BanDto banDetails, UserManager<AppUser> userManager);
}
