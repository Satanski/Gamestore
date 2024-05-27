using Gamestore.DAL;
using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Gamestore.DAL.Repositories;
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

        if (connectionString != null)
        {
            builder.Services.AddDbContext<GamestoreContext>(options => options.UseSqlServer(connectionString));
        }

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IGameService, GameService>();
        builder.Services.AddScoped<IPlatformService, PlatformService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IGenreRepository, GenreRepository>();
        builder.Services.AddScoped<IGameRepository, GameRepository>();
        builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

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

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
