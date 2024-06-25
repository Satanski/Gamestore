using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.BanHandler;

public interface IBanHandler
{
    void SetNext(IBanHandler nextHandler);

    void Handle(BanDto banDetails, CustomerStub customerStub);
}
