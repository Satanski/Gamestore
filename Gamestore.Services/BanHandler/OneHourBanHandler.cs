using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.BanHandler;

public class OneHourBanHandler : BanHandlerBase
{
    private const string OneHourBanKeyWord = "1 hour";

    public override void Handle(BanDto banDetails, CustomerStub customerStub)
    {
        if (banDetails.Duration == OneHourBanKeyWord)
        {
            CustomerStub.BannedTill = DateTime.Now.AddHours(1);
        }
        else
        {
            base.Handle(banDetails, customerStub);
        }
    }
}
