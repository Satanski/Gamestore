using Gamestore.DAL;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.WebApi.Configs;

internal static class DAlServices
{
    internal static void Congigure(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GamestoreContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IPlatformRepository, PlatformRepository>();
        services.AddScoped<IGameGenreRepository, GameGenreRepository>();
        services.AddScoped<IGamePlatformRepository, GamePlatformRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
