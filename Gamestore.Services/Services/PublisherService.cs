using AutoMapper;
using Gamestore.BLL.Exceptions;
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
    private readonly PublisherModelValidator _publisherModelValidator = new(unitOfWork);
    private readonly PublisherModelDtoValidator _publisherModelDtoValidator = new(unitOfWork);

    public async Task AddPublisherAsync(PublisherModelDto publisherModel)
    {
        logger.LogInformation("Adding publisher {@publisherModel}", publisherModel);

        var result = await _publisherModelDtoValidator.ValidateAsync(publisherModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var publisher = automapper.Map<Publisher>(publisherModel);
        await unitOfWork.PublisherRepository.AddAsync(publisher);
        await unitOfWork.SaveAsync();
    }

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

    public async Task<PublisherModelDto> GetPublisherByIdAsync(Guid publisherId)
    {
        logger.LogInformation("Getting publisher by Id: {publisherId}", publisherId);
        var publisher = await unitOfWork.PublisherRepository.GetByIdAsync(publisherId);

        return publisher == null ? throw new GamestoreException($"No publisher found with given id: {publisherId}") : automapper.Map<PublisherModelDto>(publisher);
    }

    public async Task<PublisherModelDto> GetPublisherByCompanyNameAsync(string companyName)
    {
        logger.LogInformation("Getting publisher by CompanyName: {companyName}", companyName);
        var publisher = await unitOfWork.PublisherRepository.GetByCompanyNameAsync(companyName);

        return publisher == null ? throw new GamestoreException($"No publisher found with given company name: {companyName}") : automapper.Map<PublisherModelDto>(publisher);
    }

    public async Task<IEnumerable<PublisherModelDto>> GetAllPublishersAsync()
    {
        logger.LogInformation("Getting all publishers");
        var publishers = await unitOfWork.PublisherRepository.GetAllAsync();
        List<PublisherModelDto> publisherModels = [];

        foreach (var publisher in publishers)
        {
            publisherModels.Add(automapper.Map<PublisherModelDto>(publisher));
        }

        return publisherModels.AsEnumerable();
    }

    public async Task<IEnumerable<GameModel>> GetGamesByPublisherIdAsync(Guid publisherId)
    {
        logger.LogInformation("Getting games by publisher: {publisherId}", publisherId);
        var games = await unitOfWork.PublisherRepository.GetGamesByPublisherAsync(publisherId);

        List<GameModel> gameModels = [];

        foreach (var game in games)
        {
            gameModels.Add(automapper.Map<GameModel>(game));
        }

        return gameModels.AsEnumerable();
    }

    public async Task UpdatePublisherAsync(PublisherModel publisherModel)
    {
        logger.LogInformation("Updating publisher {@publisherModel}", publisherModel);
        var result = await _publisherModelValidator.ValidateAsync(publisherModel);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors[0].ToString());
        }

        var publisher = automapper.Map<Publisher>(publisherModel);

        await unitOfWork.PublisherRepository.UpdateAsync(publisher);

        await unitOfWork.SaveAsync();
    }
}
