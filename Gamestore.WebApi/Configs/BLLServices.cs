using Gamestore.Services.Interfaces;
using Gamestore.Services.Services;

namespace Gamestore.WebApi.Configs;

internal static class BllServices
{
    internal static void Congigure(IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IPlatformService, PlatformService>();
        services.AddScoped<IGenreService, GenreService>();
    }
}
