using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.WebApi.Stubs;

namespace Gamestore.BLL.Services;

public class CommentService(IBanService banService) : ICommentService
{
    public void BanCustomerFromCommenting(BanDto banDetails)
    {
        var customerStub = new CustomerStub();
        banService.BanCustomerFromCommenting(banDetails, customerStub);
    }

    public List<string> GetBanDurations()
    {
        return BanDurationsDto.Durations;
    }
}
