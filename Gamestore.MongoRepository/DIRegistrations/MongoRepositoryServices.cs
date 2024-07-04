using Gamestore.MongoRepository.Interfaces;
using Gamestore.MongoRepository.MongoDB;
using Gamestore.MongoRepository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Gamestore.MongoRepository.DIRegistrations;

public static class MongoRepositoryServices
{
    public static void Configure(IServiceCollection services, IConfigurationSection connection)
    {
        services.Configure<MongoDBSettings>(connection);
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });
        services.AddScoped(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();
    }
}
