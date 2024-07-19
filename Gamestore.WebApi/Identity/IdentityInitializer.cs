using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.WebApi.Identity;

public static class IdentityInitializer
{
    public static async Task Initialize(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        IdentityResult roleResult;

        string[] roleNames = ["User"];
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new AppRole { Name = roleName });
            }
        }

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
                await userManager.AddToRoleAsync(normalUser, "User");
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
                await userManager.AddToRoleAsync(normalUser2, "User");
            }
        }
    }
}
