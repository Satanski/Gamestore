using Gamestore.BLL.DiRegistrations;
using Gamestore.DAL.DIRegistrations;
using Gamestore.WebApi.Interfaces;
using Gamestore.WebApi.Middlewares;
using Gamestore.WebApi.Strategies;
using Serilog;
using Serilog.Events;

namespace Gamestore.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Debug()
            .CreateBootstrapLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services));

        var connectionString = builder.Configuration.GetConnectionString("GamestoreDatabase");
        if (connectionString == null)
        {
#pragma warning disable S112 // General or reserved exceptions should never be thrown
            throw new NullReferenceException(nameof(connectionString));
#pragma warning restore S112 // General or reserved exceptions should never be thrown
        }

        builder.Services.AddMemoryCache();

        DAlServices.Configure(builder.Services, connectionString);
        BllServices.Congigure(builder.Services);

        builder.Services.AddControllers().AddNewtonsoftJson();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IPaymentStrategy, VisaPaymentStrategy>();
        builder.Services.AddScoped<IPaymentStrategy, IboxPaymentStrategy>();
        builder.Services.AddScoped<IPaymentStrategy, BankPaymentStrategy>();
        builder.Services.AddScoped<PaymentContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSerilogRequestLogging();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseMiddleware<GameCounterMiddleware>();

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}
