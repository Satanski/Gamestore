using System.Globalization;
using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Identity.Models;
using Gamestore.BLL.Interfaces;
using Gamestore.IdentityRepository;
using Gamestore.IdentityRepository.Identity;
using Gamestore.IdentityRepository.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.BLL.Services;

public class RoleService(IIdentityUnitOfWork unitOfWork, IMapper automapper) : IRoleService
{
    public List<string> GetAllPermissions()
    {
        return [.. Permissions.PermissionList.Values];
    }

    public List<RoleModel> GetAllRoles(RoleManager<AppRole> roleManager)
    {
        var roles = roleManager.Roles.ToList();

        return automapper.Map<List<RoleModel>>(roles);
    }

    public async Task<RoleModel> GetRoleByIdAsync(RoleManager<AppRole> roleManager, Guid roleId)
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());

        return role == null ? throw new GamestoreException($"Role with ID {roleId} does not exist.") : automapper.Map<RoleModel>(role);
    }

    public async Task<List<string>> GetPermissionsByRoleIdAsync(RoleManager<AppRole> roleManager, Guid roleId)
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role == null)
        {
            throw new GamestoreException($"Role with ID {roleId} does not exist.");
        }

        var permissions = unitOfWork.RoleClaimRepository.GetClaimsByRoleIdAsync(roleId).ToList();
        var permissionList = CreatePermissionList(permissions);

        return permissionList;
    }

    public async Task<IdentityResult> AddRoleAsync(RoleManager<AppRole> roleManager, RoleModelDto role)
    {
        if (await roleManager.RoleExistsAsync(role.Role.Name))
        {
            return IdentityResult.Failed(new IdentityError { Description = $"Role {role.Role.Name} already exists." });
        }

        var identityRole = new AppRole() { Name = role.Role.Name };
        var result = await roleManager.CreateAsync(identityRole);
        await AddRoleClaimsToRepositoryAsync(unitOfWork, role.Permissions!, identityRole);
        await unitOfWork.SaveAsync();

        return result;
    }

    public async Task<IdentityResult> UpdateRoleAsync(RoleManager<AppRole> roleManager, RoleModelDto roleDto)
    {
        var identityRole = await roleManager.FindByIdAsync(roleDto.Role.Id.ToString()!);
        if (identityRole == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = $"Role {roleDto.Role.Name} Doesn't exist." });
        }

        identityRole.Name = roleDto.Role.Name;
        identityRole.NormalizedName = roleDto.Role.Name.ToUpper(CultureInfo.InvariantCulture);
        unitOfWork.RoleRepository.Update(identityRole);
        await UpdateRoleCialmsInrepositoryAsync(unitOfWork, roleDto, identityRole);
        await unitOfWork.SaveAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteRoleByIdAsync(RoleManager<AppRole> roleManager, Guid roleId)
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = $"Role with ID {roleId} does not exist." });
        }

        var result = await roleManager.DeleteAsync(role);

        return result;
    }

    private static async Task AddRoleClaimsToRepositoryAsync(IIdentityUnitOfWork unitOfWork, List<string> permissions, AppRole identityRole)
    {
        foreach (var item in permissions)
        {
            var roleClaim = new RoleClaim()
            {
                RoleId = identityRole.Id,
                ClaimType = item,
                ClaimValue = item,
            };

            await unitOfWork.RoleClaimRepository.AddAsync(roleClaim);
        }
    }

    private static async Task UpdateRoleCialmsInrepositoryAsync(IIdentityUnitOfWork unitOfWork, RoleModelDto roleDto, AppRole? identityRole)
    {
        unitOfWork.RoleClaimRepository.DeleteClaimsByRoleId(identityRole.Id);
        await AddRoleClaimsToRepositoryAsync(unitOfWork, roleDto.Permissions!, identityRole);
    }

    private static List<string> CreatePermissionList(List<IdentityRoleClaim<string>> permissions)
    {
        List<string> permissionList = [];
        foreach (var permission in permissions)
        {
            permissionList.Add(permission.ClaimValue!);
        }

        return permissionList;
    }
}
