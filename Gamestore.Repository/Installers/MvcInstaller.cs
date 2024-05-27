using Gamestore.DAL.Interfaces;
using Gamestore.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Gamestore.DAL.Installers;

public class MvcInstaller : IInstaller
{
    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IPlatformRepository, PlatformRepository>();
    }
}
