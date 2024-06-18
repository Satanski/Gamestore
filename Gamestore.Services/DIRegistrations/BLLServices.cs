using AutoMapper;
using Gamestore.BLL.Helpers;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Services;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gamestore.BLL.DiRegistrations;

public static class BllServices
{
    public static void Congigure(IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IPlatformService, PlatformService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IOrderService, OrderService>();

        var autoMapperConfiguration = new MapperConfiguration(m => m.AddProfile(new MappingProfile()));
        var autoMapper = autoMapperConfiguration.CreateMapper();
        services.AddSingleton(autoMapper);
    }
}
