using Gamestore.IdentityRepository.Entities;
using Gamestore.IdentityRepository.Identity;
using Gamestore.IdentityRepository.Interfaces;

namespace Gamestore.IdentityRepository.Repositories;

public class RoleRepository(IdentityDbContext context) : IRoleRepository
{
    public void Update(AppRole role)
    {
        context.Update(role);
    }
}
