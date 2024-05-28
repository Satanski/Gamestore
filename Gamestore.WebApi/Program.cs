using Gamestore.BLL.DiRegistrations;
using Gamestore.DAL.DIRegistrations;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("GamestoreDatabase");
        if (connectionString == null)
        {
#pragma warning disable S112 // General or reserved exceptions should never be thrown
            throw new NullReferenceException(nameof(connectionString));
#pragma warning restore S112 // General or reserved exceptions should never be thrown
        }

        DAlServices.Congigure(builder.Services, connectionString);
        BllServices.Congigure(builder.Services);

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
        app.MapControllers();

        app.Run();
    }
}
