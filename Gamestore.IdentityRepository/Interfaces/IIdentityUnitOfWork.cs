namespace Gamestore.IdentityRepository.Interfaces;

public interface IIdentityUnitOfWork
{
    IRoleClaimRepository RoleClaimRepository { get; }

    IRoleRepository RoleRepository { get; }

    Task SaveAsync();
}
