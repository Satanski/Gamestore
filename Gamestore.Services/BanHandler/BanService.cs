using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.BLL.BanHandler;

public class BanService : IBanService
{
    private const string OneHourBanKeyWord = "1 hour";
    private const string OndeDayBanKeyWord = "1 day";
    private const string OneWeekBanKeyword = "1 week";
    private const string OneMonthBanKeyword = "1 month";
    private const string PermanentBanKeyWord = "permanent";
    private readonly Dictionary<string, int> _banDuration = new()
    {
        { OneHourBanKeyWord, 1 },
        { OndeDayBanKeyWord, 24 },
        { OneWeekBanKeyword, 168 },
        { OneMonthBanKeyword, 744 },
        { PermanentBanKeyWord, 9999999 },
    };

    public async Task BanCustomerFromCommentingAsync(BanDto banDetails, UserManager<AppUser> userManager)
    {
        var u = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == banDetails.User);
        if (u is not null && _banDuration.TryGetValue(banDetails.Duration, out int duration))
        {
            u.BannedTill = DateTime.Now.AddHours(duration);
            await userManager.UpdateAsync(u);
        }
        else
        {
            throw new GamestoreException("Wrong ban duration description");
        }
    }
}
