using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

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

    public void BanCustomerFromCommenting(BanDto banDetails, CustomerStub customerStub)
    {
        if (_banDuration.TryGetValue(banDetails.Duration, out int duration))
        {
            CustomerStub.BannedTill = DateTime.Now.AddHours(duration);
        }
        else
        {
            throw new GamestoreException("Wrong ban duration description");
        }
    }
}
