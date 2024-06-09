using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Interfaces;

public interface IPublisherService
{
    Task AddPublisherAsync(PublisherModelDto publisherModel);

    Task DeletPublisherByIdAsync(Guid publisherId);

    Task<IEnumerable<PublisherModelDto>> GetAllPublishersAsync();

    Task<IEnumerable<GameModel>> GetGamesByPublisherIdAsync(Guid publisherId);

    Task UpdatePublisherAsync(PublisherModel publisherModel);

    Task<PublisherModelDto> GetPublisherByIdAsync(Guid publisherId);

    Task<PublisherModelDto> GetPublisherByCompanyNameAsync(string companyName);
}
