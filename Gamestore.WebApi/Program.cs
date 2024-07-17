﻿using Gamestore.BLL.DiRegistrations;
using Gamestore.DAL.DIRegistrations;
using Gamestore.MongoRepository.DIRegistrations;
using Gamestore.WebApi.Identity;
using Gamestore.WebApi.Interfaces;
using Gamestore.WebApi.Middlewares;
using Gamestore.WebApi.Strategies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDatabase")));
        builder.Services.AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<IdentityDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;
        });

        var connectionString = builder.Configuration.GetConnectionString("GamestoreDatabase");
        if (connectionString == null)
        {
#pragma warning disable S112 // General or reserved exceptions should never be thrown
            throw new NullReferenceException(nameof(connectionString));
#pragma warning restore S112 // General or reserved exceptions should never be thrown
        }

        builder.Services.AddMemoryCache();

        DAlServices.Configure(builder.Services, connectionString);
        MongoRepositoryServices.Configure(builder.Services, builder.Configuration.GetSection("MongoDB"));
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}
