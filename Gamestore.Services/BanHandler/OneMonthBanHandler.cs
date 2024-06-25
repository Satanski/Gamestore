using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.BanHandler;

public class OneMonthBanHandler : BanHandlerBase
{
    private const string OneMonthBanKeyWord = "1 month";

    public override void Handle(BanDto banDetails, CustomerStub customerStub)
    {
        if (banDetails.Duration == OneMonthBanKeyWord)
        {
            CustomerStub.BannedTill = DateTime.Now.AddMonths(1);
        }
        else
        {
            base.Handle(banDetails, customerStub);
        }
    }
}
