using Microsoft.AspNetCore.Identity;

namespace Gamestore.IdentityRepository.Interfaces;

public interface IRoleClaimRepository
{
    Task AddAsync(RoleClaim roleClaim);

    void DeleteClaimsByRoleId(string roleId);

    IQueryable<IdentityRoleClaim<string>> GetClaimsByRoleIdAsync(Guid roleId);
}
