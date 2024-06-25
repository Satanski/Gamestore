using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.BanHandler;

public class BanHandlerBase : IBanHandler
{
    private IBanHandler _nextHandler;

    public void SetNext(IBanHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public virtual void Handle(BanDto banDetails, CustomerStub customerStub)
    {
        _nextHandler?.Handle(banDetails, customerStub);
    }
}
