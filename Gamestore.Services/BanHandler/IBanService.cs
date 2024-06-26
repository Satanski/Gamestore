using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.BanHandler;

public interface IBanService
{
    void BanCustomerFromCommenting(BanDto banDetails, CustomerStub customerStub);
}
