using Gamestore.IdentityRepository.Entities;
using Gamestore.IdentityRepository.Interfaces;
using Gamestore.IdentityRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gamestore.IdentityRepository.DIRegistrations;

public static class IdentityRepositoryServices
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
        services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();
    }
}
