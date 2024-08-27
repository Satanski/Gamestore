using System.Text;
using Azure.Identity;
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
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionAddComment)!, policy => policy.RequireClaim(Permissions.PermissionAddComment))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionDeleteComment)!, policy => policy.RequireClaim(Permissions.PermissionDeleteComment))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionBuyGame)!, policy => policy.RequireClaim(Permissions.PermissionBuyGame))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionManageUsers)!, policy => policy.RequireClaim(Permissions.PermissionManageUsers))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionManageRoles)!, policy => policy.RequireClaim(Permissions.PermissionManageRoles))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionDeletedGames)!, policy => policy.RequireClaim(Permissions.PermissionDeletedGames))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionManageEntities)!, policy => policy.RequireClaim(Permissions.PermissionManageEntities))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionEditOrders)!, policy => policy.RequireClaim(Permissions.PermissionEditOrders))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionOrderHistory)!, policy => policy.RequireClaim(Permissions.PermissionOrderHistory))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionOrderStatus)!, policy => policy.RequireClaim(Permissions.PermissionOrderStatus))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionBanUsers)!, policy => policy.RequireClaim(Permissions.PermissionBanUsers))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(Permissions.PermissionModerateComments)!, policy => policy.RequireClaim(Permissions.PermissionModerateComments))
            .AddPolicy(Permissions.PermissionManageEntitiesOrDeletedGames, policy => policy.RequireAssertion(context => context.User.HasClaim(claim => claim.Type == Permissions.PermissionManageEntities) || context.User.HasClaim(claim => claim.Type == Permissions.PermissionDeletedGames)));

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

        var keyVaultName = builder.Configuration["Azure:KeyVaultName"];
        if (!string.IsNullOrEmpty(keyVaultName))
        {
            var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
            builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
        }

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

        app.UseResponseCaching();

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
