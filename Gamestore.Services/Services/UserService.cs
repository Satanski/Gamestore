using AutoMapper;
using Gamestore.BLL.Identity.JWT;
using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Gamestore.BLL.Services;

public class UserService(IMapper automapper) : IUserService
{
    public List<CustomerDto> GetAllUsers(UserManager<AppUser> userManager)
    {
        var users = userManager.Users.ToList();

        return automapper.Map<List<CustomerDto>>(users);
    }

    public async Task<string> LoginAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration, LoginModelDto login)
    {
        var user = await userManager.FindByNameAsync(login.Model.Login);
        if (user == null || !await userManager.CheckPasswordAsync(user, login.Model.Password))
        {
            return null;
        }

        var generatedToken = await JwtHelpers.GenerateJwtToken(userManager, roleManager, configuration, user);
        string token = $"Bearer {generatedToken}";

        return token;
    }
}
