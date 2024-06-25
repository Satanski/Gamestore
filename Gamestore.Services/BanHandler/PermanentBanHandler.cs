using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.BanHandler;

public class PermanentBanHandler : BanHandlerBase
{
    private const string PermanentBanKeyWord = "permanent";

    public override void Handle(BanDto banDetails, CustomerStub customerStub)
    {
        if (banDetails.Duration == PermanentBanKeyWord)
        {
            CustomerStub.BannedTill = DateTime.Now.AddYears(999);
        }
        else
        {
            base.Handle(banDetails, customerStub);
        }
    }
}
