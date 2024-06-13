using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Services;
using Microsoft.Extensions.Logging;

namespace Gamestore.BLL.Services;

public class PublisherService(IUnitOfWork unitOfWork, IMapper automapper, ILogger<GameService> logger) : IPublisherService
{
    private readonly PublisherDtoWrapperValidator _publisherDtoWrapperValidator = new(unitOfWork);

    public async Task DeletPublisherByIdAsync(Guid publisherId)
    {
        logger.LogInformation("Deleting publisher {publisherId}", publisherId);
        var publisher = await unitOfWork.PublisherRepository.GetByIdAsync(publisherId);
        if (publisher != null)
        {
            unitOfWork.PublisherRepository.Delete(publisher);
            await unitOfWork.SaveAsync();
        }
        else
        {
            throw new GamestoreException($"No publisher found with given id: {publisherId}");
        }
    }

    public async Task<PublisherDto> GetPublisherByIdAsync(Guid publisherId)
    {
        logger.LogInformation("Getting publisher by Id: {publisherId}", publisherId);
        var publisher = await unitOfWork.PublisherRepository.GetByIdAsync(publisherId);

        return publisher == null ? throw new GamestoreException($"No publisher found with given id: {publisherId}") : automapper.Map<PublisherDto>(publisher);
    }

    public async Task<PublisherDto> GetPublisherByCompanyNameAsync(string companyName)
    {
        logger.LogInformation("Getting publisher by CompanyName: {companyName}", companyName);
        var publisher = await unitOfWork.PublisherRepository.GetByCompanyNameAsync(companyName);

        return publisher == null ? throw new GamestoreException($"No publisher found with given company name: {companyName}") : automapper.Map<PublisherDto>(publisher);
    }

    public async Task<IEnumerable<PublisherDto>> GetAllPublishersAsync()
    {
        logger.LogInformation("Getting all publishers");
        var publishers = await unitOfWork.PublisherRepository.GetAllAsync();
        List<PublisherDto> publisherModels = [];

        foreach (var publisher in publishers)
        {
            publisherModels.Add(automapper.Map<PublisherDto>(publisher));
        }

        return publisherModels.AsEnumerable();
    }

    public async Task<IEnumerable<GameModelDto>> GetGamesByPublisherIdAsync(Guid publisherId)
    {
        logger.LogInformation("Getting games by publisher: {publisherId}", publisherId);
        var games = await unitOfWork.PublisherRepository.GetGamesByPublisherIdAsync(publisherId);

        List<GameModelDto> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(automapper.Map<GameModelDto>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task<IEnumerable<GameModelDto>> GetGamesByPublisherNameAsync(string publisherName)
    {
        logger.LogInformation("Getting games by publisher: {publisherName}", publisherName);
        var games = await unitOfWork.PublisherRepository.GetGamesByPublisherNameAsync(publisherName);

        List<GameModelDto> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(automapper.Map<GameModelDto>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task AddPublisherAsync(PublisherDtoWrapper publisherModel)
    {
        logger.LogInformation("Adding publisher {@publisherModel}", publisherModel);

        await _publisherDtoWrapperValidator.ValidatePublisher(publisherModel);

        var publisher = automapper.Map<Publisher>(publisherModel.Publisher);
        await unitOfWork.PublisherRepository.AddAsync(publisher);
        await unitOfWork.SaveAsync();
    }

    public async Task UpdatePublisherAsync(PublisherDtoWrapper publisherModel)
    {
        logger.LogInformation("Updating publisher {@publisherModel}", publisherModel);

        await _publisherDtoWrapperValidator.ValidatePublisher(publisherModel);

        var publisher = automapper.Map<Publisher>(publisherModel.Publisher);

        await unitOfWork.PublisherRepository.UpdateAsync(publisher);

        await unitOfWork.SaveAsync();
    }
}
