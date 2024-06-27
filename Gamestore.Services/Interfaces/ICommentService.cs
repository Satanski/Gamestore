using Gamestore.BLL.Models;

namespace Gamestore.BLL.Interfaces;

public interface ICommentService
{
    void BanCustomerFromCommenting(BanDto banDetails);

    List<string> GetBanDurations();
}
