using System.Globalization;
using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Identity.JWT;
using Gamestore.BLL.Identity.Models;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gamestore.BLL.Services;

public class UserService(IMapper automapper) : IUserService
{
    private const string EmailStub = "default@default.com";

    public List<CustomerDto> GetAllUsers(UserManager<AppUser> userManager)
    {
        var users = userManager.Users.ToList();

        return automapper.Map<List<CustomerDto>>(users);
    }

    public async Task<CustomerDto> GetUserByIdAsync(UserManager<AppUser> userManager, string userId)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

        return automapper.Map<CustomerDto>(user);
    }

    public async Task<List<UserRoleDto>> GetUserRolesByUserId(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, string userId)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
        {
            throw new GamestoreException("User not found.");
        }

        List<UserRoleDto> userRoles = [];
        await FindRolesByUserAsync(userManager, roleManager, user, userRoles);

        return userRoles;
    }

    public async Task<string> LoginAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration, LoginModelDto login)
    {
        var user = await userManager.FindByNameAsync(login.Model.Login);
        if (user == null || !await userManager.CheckPasswordAsync(user, login.Model.Password))
        {
            throw new GamestoreException("User not found.");
        }

        var generatedToken = await JwtHelpers.GenerateJwtToken(userManager, roleManager, configuration, user);
        string token = $"Bearer {generatedToken}";

        return token;
    }

    public async Task<IdentityResult> UpdateUserAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, UserDto user)
    {
        var userToUpdate = await userManager.FindByIdAsync(user.User.Id);
        if (userToUpdate == null)
        {
            throw new GamestoreException("User not found.");
        }

        UpdateUserDetails(user, userToUpdate);
        await UpdateUserPasswordAsync(userManager, user, userToUpdate);
        await UpdateUserRolesAsync(userManager, roleManager, user, userToUpdate);

        return await userManager.UpdateAsync(userToUpdate);
    }

    public async Task DeleteUserAsync(UserManager<AppUser> userManager, string userId)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
        {
            throw new GamestoreException("User not found");
        }

        await userManager.DeleteAsync(user);
    }

    public async Task AddUserAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, UserDto user)
    {
        var appUser = new AppUser
        {
            UserName = user.User.Name,
            NormalizedUserName = user.User.Name.ToUpper(CultureInfo.InvariantCulture),
            Email = EmailStub,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
        };

        await userManager.CreateAsync(appUser);
        await AddUserToSelectedRolesAsync(userManager, roleManager, user, appUser);
        await AddUserPasswordAsync(userManager, user, appUser);
    }

    private static async Task UpdateUserRolesAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, UserDto user, AppUser userToUpdate)
    {
        await RemoveUserFromAllRolesAsync(userManager, userToUpdate);
        await AddUserToSelectedRolesAsync(userManager, roleManager, user, userToUpdate);
    }

    private static async Task AddUserToSelectedRolesAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, UserDto user, AppUser userToUpdate)
    {
        foreach (var roleId in user.Roles)
        {
            var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);

            if (role is not null)
            {
                await userManager.AddToRoleAsync(userToUpdate, role.Name!);
            }
        }
    }

    private static async Task RemoveUserFromAllRolesAsync(UserManager<AppUser> userManager, AppUser userToUpdate)
    {
        var roles = await userManager.GetRolesAsync(userToUpdate);
        if (roles != null || roles!.Any())
        {
            var result = await userManager.RemoveFromRolesAsync(userToUpdate, roles!);
            if (!result.Succeeded)
            {
                throw new GamestoreException("RemoveFromRolesAsync failed");
            }
        }
    }

    private static void UpdateUserDetails(UserDto user, AppUser? userToUpdate)
    {
        userToUpdate.UserName = user.User.Name;
        userToUpdate.NormalizedUserName = user.User.Name.ToUpper(CultureInfo.InvariantCulture);
    }

    private static async Task UpdateUserPasswordAsync(UserManager<AppUser> userManager, UserDto user, AppUser userToUpdate)
    {
        await RemoveUserPasswordAsync(userManager, userToUpdate);
        await AddUserPasswordAsync(userManager, user, userToUpdate);
    }

    private static async Task AddUserPasswordAsync(UserManager<AppUser> userManager, UserDto user, AppUser userToUpdate)
    {
        var addPasswordResult = await userManager.AddPasswordAsync(userToUpdate, user.Password);
        if (!addPasswordResult.Succeeded)
        {
            throw new GamestoreException("AddPasswordAsync faild");
        }
    }

    private static async Task RemoveUserPasswordAsync(UserManager<AppUser> userManager, AppUser userToUpdate)
    {
        var removePasswordResult = await userManager.RemovePasswordAsync(userToUpdate);
        if (!removePasswordResult.Succeeded)
        {
            throw new GamestoreException("RemovePasswordAsync faild");
        }
    }

    private static async Task FindRolesByUserAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppUser user, List<UserRoleDto> userRoles)
    {
        var roles = await userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            var name = role;
            var appRole = await roleManager.FindByNameAsync(role);
            var id = await roleManager.GetRoleIdAsync(appRole!);

            userRoles.Add(new UserRoleDto { Id = id, Name = name });
        }
    }
}
