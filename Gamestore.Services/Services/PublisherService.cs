using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.BLL.Validation;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.MongoRepository.Interfaces;
using Gamestore.Services.Models;
using Gamestore.Services.Services;
using Microsoft.Extensions.Logging;

namespace Gamestore.BLL.Services;

public class PublisherService(IUnitOfWork unitOfWork, IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, ILogger<GameService> logger) : IPublisherService
{
    private readonly PublisherDtoWrapperValidator _publisherDtoWrapperValidator = new(unitOfWork);

    public async Task DeletPublisherByIdAsync(Guid publisherId)
    {
        logger.LogInformation("Deleting publisher {publisherId}", publisherId);
        var publisher = await unitOfWork.PublisherRepository.GetByOrderIdAsync(publisherId);
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
        var publisher = await unitOfWork.PublisherRepository.GetByOrderIdAsync(publisherId);

        return publisher == null ? throw new GamestoreException($"No publisher found with given id: {publisherId}") : automapper.Map<PublisherModelDto>(publisher);
    }

    public async Task<PublisherModelDto> GetPublisherByCompanyNameAsync(string companyName)
    {
        logger.LogInformation("Getting publisher by CompanyName: {companyName}", companyName);

        var publisher = await GetPublisherFromSQLServerByCompanyName(unitOfWork, companyName);
        publisher ??= await GetPublisherFromMongoDB(mongoUnitOfWork, automapper, companyName);

        return publisher == null ? throw new GamestoreException($"No publisher found with given company name: {companyName}") : automapper.Map<PublisherModelDto>(publisher);
    }

    public async Task<IEnumerable<PublisherModelDto>> GetAllPublishersAsync()
    {
        logger.LogInformation("Getting all publishers");

        var publisherModels = await GetPublishersFromSQLServer(unitOfWork, automapper);
        publisherModels.AddRange(await GetPublishersFromMongoDB(mongoUnitOfWork, automapper));

        return publisherModels.AsEnumerable();
    }

    public async Task<IEnumerable<GameModelDto>> GetGamesByPublisherIdAsync(Guid publisherId)
    {
        logger.LogInformation("Getting games by publisher: {publisherId}", publisherId);
        var games = await unitOfWork.PublisherRepository.GetGamesByPublisherIdAsync(publisherId);
        var gameModels = automapper.Map<List<GameModelDto>>(games);

        return gameModels.AsEnumerable();
    }

    public async Task<IEnumerable<GameModelDto>> GetGamesByPublisherNameAsync(string publisherName)
    {
        logger.LogInformation("Getting games by publisher: {publisherName}", publisherName);

        var gameModels = await GetGamesByPublisherNameFromSQLServer(unitOfWork, automapper, publisherName);
        if (gameModels.Count == 0)
        {
            gameModels = await GetGamesByPublisherNameFromMongoDB(mongoUnitOfWork, automapper, publisherName);
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

    private static async Task<Publisher?> GetPublisherFromMongoDB(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, string companyName)
    {
        var supplier = await mongoUnitOfWork.SupplierRepository.GetByNameAsync(companyName);
        var publisher = automapper.Map<Publisher>(supplier);
        return publisher;
    }

    private static async Task<Publisher?> GetPublisherFromSQLServerByCompanyName(IUnitOfWork unitOfWork, string companyName)
    {
        return await unitOfWork.PublisherRepository.GetByCompanyNameAsync(companyName);
    }

    private static async Task<List<GameModelDto>> GetGamesByPublisherNameFromSQLServer(IUnitOfWork unitOfWork, IMapper automapper, string publisherName)
    {
        var games = await unitOfWork.PublisherRepository.GetGamesByPublisherNameAsync(publisherName);
        var gameModels = automapper.Map<List<GameModelDto>>(games);

        return gameModels;
    }

    private static async Task<List<GameModelDto>> GetGamesByPublisherNameFromMongoDB(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper, string publisherName)
    {
        var supplier = await mongoUnitOfWork.SupplierRepository.GetByNameAsync(publisherName);
        var games = await mongoUnitOfWork.ProductRepository.GetBySupplierIdAsync(supplier.SupplierID);
        var gameModels = automapper.Map<List<GameModelDto>>(games);

        return gameModels;
    }

    private static async Task<List<PublisherModelDto>> GetPublishersFromSQLServer(IUnitOfWork unitOfWork, IMapper automapper)
    {
        var publishers = await unitOfWork.PublisherRepository.GetAllAsync();
        var publisherModels = automapper.Map<List<PublisherModelDto>>(publishers);

        return publisherModels;
    }

    private static async Task<List<PublisherModelDto>> GetPublishersFromMongoDB(IMongoUnitOfWork mongoUnitOfWork, IMapper automapper)
    {
        var suppliers = await mongoUnitOfWork.SupplierRepository.GetAllAsync();
        var publishers = automapper.Map<List<PublisherModelDto>>(suppliers);

        return publishers;
    }
}
