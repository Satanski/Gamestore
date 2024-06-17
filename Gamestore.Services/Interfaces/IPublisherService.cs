using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Interfaces;

public interface IPublisherService
{
    Task AddPublisherAsync(PublisherDtoWrapper publisherModel);

    Task DeletPublisherByIdAsync(Guid publisherId);

    Task<IEnumerable<PublisherModelDto>> GetAllPublishersAsync();

    Task<IEnumerable<GameModelDto>> GetGamesByPublisherIdAsync(Guid publisherId);

    Task<IEnumerable<GameModelDto>> GetGamesByPublisherNameAsync(string publisherName);

    Task UpdatePublisherAsync(PublisherDtoWrapper publisherModel);

    Task<PublisherModelDto> GetPublisherByIdAsync(Guid publisherId);

    Task<PublisherModelDto> GetPublisherByCompanyNameAsync(string companyName);
}
