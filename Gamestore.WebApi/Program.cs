using Gamestore.Repository;
using Gamestore.Repository.Entities;
using Gamestore.Repository.Interfaces;
using Gamestore.Services.Interfaces;
using Gamestore.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("GamestoreDatabase");

        // Add services to the container.
        if (connectionString != null)
        {
            builder.Services.AddDbContext<GamestoreContext>(options => options.UseSqlServer(connectionString));
        }

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IGameService, GameService>();
        builder.Services.AddScoped<IPlatformService, PlatformService>();
        builder.Services.AddScoped<IGenreService, GenreService>();

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.Use(async (context, next) =>
        {
            var dbContext = context.RequestServices.GetRequiredService<GamestoreContext>();

            int numberOfGames = dbContext.Games.Count();

            context.Response.Headers.Append("x-total-number-of-games", $"{numberOfGames}");
            await next.Invoke();
        });

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
