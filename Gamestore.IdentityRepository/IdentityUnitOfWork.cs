using Gamestore.IdentityRepository.Entities;
using Gamestore.IdentityRepository.Interfaces;

namespace Gamestore.IdentityRepository;

public class IdentityUnitOfWork(IdentityDbContext context, IRoleClaimRepository roleClaimRepository, IRoleRepository roleRepository) : IIdentityUnitOfWork
{
    private readonly IdentityDbContext _context = context;

    public IRoleClaimRepository RoleClaimRepository => roleClaimRepository;

    public IRoleRepository RoleRepository => roleRepository;

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
