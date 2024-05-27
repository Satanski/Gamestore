using Gamestore.DAL;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.WebApi.Configs;

internal static class DllServices
{
    internal static void Congigure(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GamestoreContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IPlatformRepository, PlatformRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
