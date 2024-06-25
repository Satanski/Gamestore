using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.BanHandler;

public class OneWeekBanHandler : BanHandlerBase
{
    private const string OneWeekBanKeyword = "1 week";

    public override void Handle(BanDto banDetails, CustomerStub customerStub)
    {
        if (banDetails.Duration == OneWeekBanKeyword)
        {
            CustomerStub.BannedTill = DateTime.Now.AddDays(7);
        }
        else
        {
            base.Handle(banDetails, customerStub);
        }
    }
}
