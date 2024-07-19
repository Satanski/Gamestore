using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Gamestore.BLL.Interfaces;

public interface IUserService
{
    List<CustomerDto> GetAllUsers(UserManager<AppUser> userManager);

    Task<string> LoginAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration, LoginModelDto login);
}
