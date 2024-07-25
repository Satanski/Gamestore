using Gamestore.IdentityRepository.Entities;
using Gamestore.IdentityRepository.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.IdentityRepository.Repositories;

public class RoleClaimRepository(IdentityDbContext context) : IRoleClaimRepository
{
    public async Task AddAsync(RoleClaim roleClaim)
    {
        await context.RoleClaims.AddAsync(roleClaim);
    }

    public IQueryable<IdentityRoleClaim<string>> GetClaimsByRoleIdAsync(Guid roleId)
    {
        return context.RoleClaims.Where(x => x.RoleId == roleId.ToString());
    }

    public void DeleteClaimsByRoleId(string roleId)
    {
        context.RoleClaims.RemoveRange(context.RoleClaims.Where(x => x.RoleId == roleId));
    }
}
