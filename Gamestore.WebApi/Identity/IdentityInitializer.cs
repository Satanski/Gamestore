using Gamestore.BLL.Identity.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.WebApi.Identity;

public static class IdentityInitializer
{
    public static async Task Initialize(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        string[] roleNames = ["Guest", "User", "Moderator", "Manager", "Admin"];

        await CreateRoles(roleManager, roleNames);
        await CreateUserClaims(roleManager, roleNames);
        await CreateAdminClaims(roleManager, roleNames);
        await CreateManagerClaims(roleManager, roleNames);
        await CreateModeratorClaims(roleManager, roleNames);
        await CreateUsers(userManager, roleNames);
    }

    private static async Task CreateModeratorClaims(RoleManager<AppRole> roleManager, string[] roleNames)
    {
        var userRole = roleManager.Roles.FirstOrDefault(x => x.Name == roleNames[2]);
        var claims = await roleManager.GetClaimsAsync(userRole!);
        var claimKey = Permissions.PermissionDeleteComment;
        var claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);

        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }

        claimKey = Permissions.PermissionBanUsers;
        claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);
        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }

        claimKey = Permissions.PermissionModerateComments;
        claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);
        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }
    }

    private static async Task CreateManagerClaims(RoleManager<AppRole> roleManager, string[] roleNames)
    {
        var userRole = roleManager.Roles.FirstOrDefault(x => x.Name == roleNames[3]);
        var claims = await roleManager.GetClaimsAsync(userRole!);
        var claimKey = Permissions.PermissionManageEntities;
        var claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);

        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }

        claimKey = Permissions.PermissionEditOrders;
        claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);
        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }

        claimKey = Permissions.PermissionOrderHistory;
        claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);
        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }

        claimKey = Permissions.PermissionOrderStatus;
        claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);
        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }
    }

    private static async Task CreateAdminClaims(RoleManager<AppRole> roleManager, string[] roleNames)
    {
        var userRole = roleManager.Roles.FirstOrDefault(x => x.Name == roleNames[4]);
        var claims = await roleManager.GetClaimsAsync(userRole!);
        var claimKey = Permissions.PermissionManageUsers;
        var claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);

        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }

        claimKey = Permissions.PermissionDeletedGames;
        claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);
        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }

        claimKey = Permissions.PermissionManageRoles;
        claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);
        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }
    }

    private static async Task CreateUserClaims(RoleManager<AppRole> roleManager, string[] roleNames)
    {
        var userRole = roleManager.Roles.FirstOrDefault(x => x.Name == roleNames[1]);
        var claims = await roleManager.GetClaimsAsync(userRole!);
        var claimKey = Permissions.PermissionDeleteComment;
        var claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);

        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }

        claimKey = Permissions.PermissionBuyGame;
        claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);
        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }

        claimKey = Permissions.PermissionOrderStatus;
        claimValue = Permissions.PermissionList.GetValueOrDefault(claimKey);
        if (!claims.Any(x => x.Type == claimKey))
        {
            await roleManager.AddClaimAsync(userRole!, new System.Security.Claims.Claim(claimKey, claimValue!));
        }
    }

    private static async Task CreateUsers(UserManager<AppUser> userManager, string[] roleNames)
    {
        var normalUser = new AppUser
        {
            UserName = "Pawel",
            Email = "pawel@wp.pl",
        };

        string userPassword = "1";

        var user = await userManager.FindByNameAsync(normalUser.UserName);
        if (user == null)
        {
            var createNormalUser = await userManager.CreateAsync(normalUser, userPassword);
            if (createNormalUser.Succeeded)
            {
                await userManager.AddToRoleAsync(normalUser, roleNames[1]);
            }
        }

        var normalUser2 = new AppUser
        {
            UserName = "Robert",
            Email = "robert@wp.pl",
        };

        var user2 = await userManager.FindByNameAsync(normalUser2.UserName);
        if (user2 == null)
        {
            var createUser = await userManager.CreateAsync(normalUser2, userPassword);
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(normalUser2, roleNames[1]);
            }
        }

        var adminlUser = new AppUser
        {
            UserName = "Admin",
            Email = "admin@wp.pl",
        };

        var user3 = await userManager.FindByNameAsync(adminlUser.UserName);
        if (user3 == null)
        {
            var createUser = await userManager.CreateAsync(adminlUser, userPassword);
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(adminlUser, roleNames[4]);
            }
        }

        var managerUser = new AppUser
        {
            UserName = "Manager",
            Email = "manager@wp.pl",
        };

        var user4 = await userManager.FindByNameAsync(managerUser.UserName);
        if (user4 == null)
        {
            var createUser = await userManager.CreateAsync(managerUser, userPassword);
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(managerUser, roleNames[3]);
            }
        }

        var moderatorUser = new AppUser
        {
            UserName = "Moderator",
            Email = "moderator@wp.pl",
        };

        var user5 = await userManager.FindByNameAsync(moderatorUser.UserName);
        if (user5 == null)
        {
            var createUser = await userManager.CreateAsync(moderatorUser, userPassword);
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(moderatorUser, roleNames[2]);
            }
        }
    }

    private static async Task CreateRoles(RoleManager<AppRole> roleManager, string[] roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleNames[0]);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new AppRole { Name = roleNames[0] });
        }

        roleExist = await roleManager.RoleExistsAsync(roleNames[1]);
        if (!roleExist)
        {
            var parentRole = await roleManager.FindByNameAsync(roleNames[0]);
            await roleManager.CreateAsync(new AppRole { Name = roleNames[1], ParentRole = parentRole, ParentRoleId = parentRole.Id });
        }

        roleExist = await roleManager.RoleExistsAsync(roleNames[2]);
        if (!roleExist)
        {
            var parentRole = await roleManager.FindByNameAsync(roleNames[1]);
            await roleManager.CreateAsync(new AppRole { Name = roleNames[2], ParentRole = parentRole, ParentRoleId = parentRole.Id });
        }

        roleExist = await roleManager.RoleExistsAsync(roleNames[3]);
        if (!roleExist)
        {
            var parentRole = await roleManager.FindByNameAsync(roleNames[2]);
            await roleManager.CreateAsync(new AppRole { Name = roleNames[3], ParentRole = parentRole, ParentRoleId = parentRole.Id });
        }

        roleExist = await roleManager.RoleExistsAsync(roleNames[4]);
        if (!roleExist)
        {
            var parentRole = await roleManager.FindByNameAsync(roleNames[3]);
            await roleManager.CreateAsync(new AppRole { Name = roleNames[4], ParentRole = parentRole, ParentRoleId = parentRole.Id });
        }
    }
}
