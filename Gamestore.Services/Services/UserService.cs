﻿using System.Globalization;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Gamestore.BLL.Exceptions;
using Gamestore.BLL.Identity.JWT;
using Gamestore.BLL.Identity.Models;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Notifications;
using Gamestore.BLL.Notifications;
using Gamestore.IdentityRepository.Entities;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gamestore.BLL.Services;

public class UserService(IMapper automapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration) : IUserService
{
    private const string EmailStub = "default@default.com";

    public List<CustomerDto> GetAllUsers()
    {
        var users = userManager.Users.ToList();

        return automapper.Map<List<CustomerDto>>(users);
    }

    public async Task<CustomerDto> GetUserByIdAsync(string userId)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

        return automapper.Map<CustomerDto>(user);
    }

    public async Task<List<UserRoleDto>> GetUserRolesByUserId(string userId)
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

    public async Task<string> LoginAsync(LoginModelDto login)
    {
        string token;

        if (login.Model.InternalAuth)
        {
            token = await LoginInternalAsync(login);
        }
        else
        {
            token = await LoginExternalAsync(login);
        }

        return token;
    }

    public async Task<string> LoginInternalAsync(LoginModelDto login)
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

    public async Task<string> LoginExternalAsync(LoginModelDto login)
    {
        var externalLoginDto = new ExternalLoginDto() { Email = login.Model.Login, Password = login.Model.Password };
        var json = JsonSerializer.Serialize(externalLoginDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpClient client = new HttpClient();
        var serviceUrl = configuration["ExternalAuthServiceUrl"];
        var response = await client.PostAsync(serviceUrl, content);

        response.EnsureSuccessStatusCode();

        var generatedToken = await JwtHelpers.GenerateJwtTokenForExternalAuth(roleManager, configuration, login.Model.Login);
        string token = $"Bearer {generatedToken}";

        return token;
    }

    public async Task<IdentityResult> UpdateUserAsync(UserDto user)
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

    public async Task DeleteUserAsync(string userId)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
        {
            throw new GamestoreException("User not found");
        }

        await userManager.DeleteAsync(user);
    }

    public async Task AddUserAsync(UserDto user)
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

    public IEnumerable<string> GetNotificationMethods()
    {
        return NotificationHelpers.GetNotificationMethods();
    }

    public IEnumerable<string> GetUserNotificationMethods(AppUser user)
    {
        return NotificationHelpers.GetUserNotificationMethods(user);
    }

    public async Task SetUserNotificationMethodsAsync(NotificationsDto notificaltionList, AppUser user)
    {
        user.NotificationMethods.Clear();

        foreach (var notification in notificaltionList.Notifications)
        {
            user.NotificationMethods.Add(new UserNotificationMethod() { UserId = user.Id, NotificationType = notification });
        }

        await userManager.UpdateAsync(user);
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
