using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gamestore.DAL.DIRegistrations;

public static class DAlServices
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GamestoreContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IPlatformRepository, PlatformRepository>();
        services.AddScoped<IGameGenreRepository, GameGenreRepository>();
        services.AddScoped<IGamePlatformRepository, GamePlatformRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderGameRepository, OrderGameRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
