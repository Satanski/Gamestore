using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.BanHandler;

public class BanService : IBanService
{
    private readonly OneHourBanHandler _handlerChain;

    public BanService()
    {
        var oneHourBanHandler = new OneHourBanHandler();
        var oneDayBanHandler = new OneDayBanHandler();
        var oneWeekBanHandler = new OneWeekBanHandler();
        var oneMonthBanHandler = new OneMonthBanHandler();
        var permanentBanHandler = new PermanentBanHandler();

        oneHourBanHandler.SetNext(oneDayBanHandler);
        oneDayBanHandler.SetNext(oneWeekBanHandler);
        oneWeekBanHandler.SetNext(oneMonthBanHandler);
        oneMonthBanHandler.SetNext(permanentBanHandler);

        _handlerChain = oneHourBanHandler;
    }

    public void BanCustomerFromCommenting(BanDto banDetails, CustomerStub customerStub)
    {
        _handlerChain.Handle(banDetails, customerStub);
    }
}
