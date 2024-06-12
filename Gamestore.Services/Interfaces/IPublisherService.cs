using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Interfaces;

public interface IPublisherService
{
    Task AddPublisherAsync(PublisherAddDto publisherModel);

    Task DeletPublisherByIdAsync(Guid publisherId);

    Task<IEnumerable<PublisherModel>> GetAllPublishersAsync();

    Task<IEnumerable<GameModel>> GetGamesByPublisherIdAsync(Guid publisherId);

    Task<IEnumerable<GameModel>> GetGamesByPublisherNameAsync(string publisherName);

    Task UpdatePublisherAsync(PublisherUpdateDto publisherModel);

    Task<PublisherModel> GetPublisherByIdAsync(Guid publisherId);

    Task<PublisherModel> GetPublisherByCompanyNameAsync(string companyName);
}
