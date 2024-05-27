using Gamestore.BLL.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gamestore.BLL.Installers;

public class MvcInstaller : IInstaller
{
    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IPlatformService, PlatformService>();
        services.AddScoped<IGenreService, GenreService>();
    }
}
