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
    private const string PermissionAddComment = "permissions.add.comment";
    private const string PermissionDeleteComment = "permissions.delete.comment";
    private const string PermissionBuyGame = "permissions.buy.game";
    private const string PermissionManageUsers = "permissions.manage.users";
    private const string PermissionManageRoles = "permissions.manage.roles";
    private const string PermissionDeletedGames = "permissions.deleted.games";
    private const string PermissionManageEntities = "permissions.manage.entities";
    private const string PermissionEditOrders = "permissions.edit.orders";
    private const string PermissionOrderHistory = "permissions.order.history";
    private const string PermissionOrderStatus = "permissions.order.status";
    private const string PermissionBanUsers = "permissions.ban.users";
    private const string PermissionModerateComments = "permissions.moderate.comments";
    private const string PermissionManageEntitiesOrDeletedGames = "ManageEntitiesOrDeletedGames";

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
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionAddComment)!, policy => policy.RequireClaim(PermissionAddComment))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionDeleteComment)!, policy => policy.RequireClaim(PermissionDeleteComment))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionBuyGame)!, policy => policy.RequireClaim(PermissionBuyGame))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionManageUsers)!, policy => policy.RequireClaim(PermissionManageUsers))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionManageRoles)!, policy => policy.RequireClaim(PermissionManageRoles))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionDeletedGames)!, policy => policy.RequireClaim(PermissionDeletedGames))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionManageEntities)!, policy => policy.RequireClaim(PermissionManageEntities))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionEditOrders)!, policy => policy.RequireClaim(PermissionEditOrders))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionOrderHistory)!, policy => policy.RequireClaim(PermissionOrderHistory))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionOrderStatus)!, policy => policy.RequireClaim(PermissionOrderStatus))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionBanUsers)!, policy => policy.RequireClaim(PermissionBanUsers))
            .AddPolicy(Permissions.PermissionList.GetValueOrDefault(PermissionModerateComments)!, policy => policy.RequireClaim(PermissionModerateComments))
            .AddPolicy(PermissionManageEntitiesOrDeletedGames, policy => policy.RequireAssertion(context => context.User.HasClaim(claim => claim.Type == PermissionManageEntities) || context.User.HasClaim(claim => claim.Type == PermissionDeletedGames)));

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
