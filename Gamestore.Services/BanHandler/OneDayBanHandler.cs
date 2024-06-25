using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.BanHandler;

public class OneDayBanHandler : BanHandlerBase
{
    private const string OneDayBanKeyWord = "1 day";

    public override void Handle(BanDto banDetails, CustomerStub customerStub)
    {
        if (banDetails.Duration == OneDayBanKeyWord)
        {
            CustomerStub.BannedTill = DateTime.Now.AddDays(1);
        }
        else
        {
            base.Handle(banDetails, customerStub);
        }
    }
}
