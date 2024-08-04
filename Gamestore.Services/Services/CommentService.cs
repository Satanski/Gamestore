using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.BLL.Services;

public class CommentService(IBanService banService) : ICommentService
{
    public async Task BanCustomerFromCommentingAsync(BanDto banDetails, UserManager<AppUser> userManager)
    {
        await banService.BanCustomerFromCommentingAsync(banDetails, userManager);
    }

    public List<string> GetBanDurations()
    {
        return BanDurationsDto.Durations;
    }
}
