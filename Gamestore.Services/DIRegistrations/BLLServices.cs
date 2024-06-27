using AutoMapper;
using Gamestore.BLL.BanHandler;
using Gamestore.BLL.Configurations;
using Gamestore.BLL.Filtering;
using Gamestore.BLL.Filtering.Handlers;
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
        services.AddScoped<PaymentServiceConfiguration>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IBanService, BanService>();
        services.AddScoped<IGenreFilterHandler, GenreFilterHandler>();
        services.AddScoped<INameFilterHandler, NameFilterHandler>();
        services.AddScoped<IPaginationFilterHandler, PaginationFilterHandler>();
        services.AddScoped<IPlatformFilterHandler, PlatformFilterHandler>();
        services.AddScoped<IPriceFilterHandler, PriceFilterHandler>();
        services.AddScoped<IPublishDateHandler, PublishDateHandler>();
        services.AddScoped<IPublisherFilterHandler, PublisherFilterHandler>();
        services.AddScoped<ISortingHandler, SortingHandler>();
        services.AddScoped<IFilterServiceBuilder, FilterServiceBuilder>();
        services.AddScoped<IFilterServiceDirector, FilterServiceDirector>();

        var autoMapperConfiguration = new MapperConfiguration(m => m.AddProfile(new MappingProfile()));
        var autoMapper = autoMapperConfiguration.CreateMapper();
        services.AddSingleton(autoMapper);
    }
}
