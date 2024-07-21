using System.Text;
using Gamestore.BLL.DiRegistrations;
using Gamestore.BLL.Identity.Models;
using Gamestore.DAL.DIRegistrations;
using Gamestore.IdentityRepository.DIRegistrations;
using Gamestore.IdentityRepository.Entities;
using Gamestore.IdentityRepository.Identity;
using Gamestore.MongoRepository.DIRegistrations;
using Gamestore.WebApi.Identity;
using Gamestore.WebApi.Interfaces;
using Gamestore.WebApi.Middlewares;
using Gamestore.WebApi.Strategies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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

        builder.Services.AddIdentity<AppUser, AppRole>(options => options.User.RequireUniqueEmail = false)
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = false;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(50);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                };
            });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.add.comment")!, policy => policy.RequireClaim("permissions.add.comment"))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.delete.comment")!, policy => policy.RequireClaim("permissions.delete.comment"))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.buy.game")!, policy => policy.RequireClaim("permissions.buy.game"!))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.manage.users")!, policy => policy.RequireClaim("permissions.manage.users"))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.manage.roles")!, policy => policy.RequireClaim("permissions.manage.roles"))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.deleted.games")!, policy => policy.RequireClaim("permissions.deleted.games"))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.manage.entities")!, policy => policy.RequireClaim("permissions.manage.entities"))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.edit.orders")!, policy => policy.RequireClaim("permissions.edit.orders"))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.order.history")!, policy => policy.RequireClaim("permissions.order.history"))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.order.status")!, policy => policy.RequireClaim("permissions.order.status"))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.ban.users")!, policy => policy.RequireClaim("permissions.ban.users"))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault("permissions.moderate.comments")!, policy => policy.RequireClaim("permissions.moderate.comments"))
            .AddPolicy("ManageEntitiesOrDeletedGames", policy => policy.RequireAssertion(context => context.User.HasClaim(claim => claim.Type == "permissions.manage.entities") || context.User.HasClaim(claim => claim.Type == "permissions.deleted.games")));

        builder.Services.AddMemoryCache();

        DAlServices.Configure(builder.Services, builder.Configuration.GetConnectionString("GamestoreDatabase")!);
        IdentityRepositoryServices.Configure(builder.Services, builder.Configuration.GetConnectionString("IdentityDatabase")!);
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

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
            IdentityInitializer.Initialize(userManager, roleManager).Wait();
        }

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}
